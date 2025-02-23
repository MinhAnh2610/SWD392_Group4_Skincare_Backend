using CleanArchitecture.Application.DTOs.BlogDto;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.RepositoryContracts;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Security.Claims;

namespace CleanArchitecture.Application.Services;

public class BlogService : IBlogService
{
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IErrorFactory _errorFactory;
  private readonly IValidator<CreateBlogRequest> _createBlogValidator;
  public BlogService(IUnitOfWork unitOfWork, IErrorFactory errorFactory, IValidator<CreateBlogRequest> createBlogValidator, IHttpContextAccessor httpContextAccessor)
  {
    _unitOfWork = unitOfWork;
    _errorFactory = errorFactory;
    _createBlogValidator = createBlogValidator;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task<Result<List<BlogResponse>>> GetAllBlogsAsync()
  {
    var blogs = await _unitOfWork.Blogs.GetAllAsync();

    return Result<List<BlogResponse>>.Success(blogs.Select(b => new BlogResponse
    {
      Id = b.Id,
      StaffName = b.Staff.FirstName + " " + b.Staff.LastName,
      Title = b.Title,
      ShortenContent = b.Content.Length > 100
          ? b.Content.Substring(0, 100) + "..."
          : b.Content,
      BlogTags = b.BlogTags.ToList(),
    }).ToList(), StatusCodes.Status200OK);
  }

  /// <summary>
  /// Get blogs with conditions.  
  /// </summary>
  /// <param name="request">An object wrapped with conditions.</param>
  /// <returns>A paginated list of item filtered by conditions.</returns>
  public async Task<Result<PaginatedList<BlogResponse>>> GetBlogsAsync(GetProductRequest request)
  {
    var query = _unitOfWork.Blogs.GetQueryable();

    if (!string.IsNullOrEmpty(request.Title))
    {
      query = query.Where(b => b.Title.ToLower().Contains(request.Title.ToLower()));
    }

    if (!string.IsNullOrEmpty(request.Content))
    {
      query = query.Where(b => b.Content.ToLower().Contains(request.Content.ToLower()));
    }

    if (!string.IsNullOrEmpty(request.StaffUsername))
    {
      query = query.Where(b => b.Staff.UserName.ToLower().Contains(request.StaffUsername.ToLower()));
    }
      var blogResponseQuery = query.Select(b => new BlogResponse
      {
        Id = b.Id,
        StaffName = b.Staff.FirstName + " " + b.Staff.LastName,
        Title = b.Title,
        ShortenContent = b.Content.Length > 100
          ? b.Content.Substring(0, 100) + "..."
          : b.Content,
        BlogTags = b.BlogTags.ToList(),
      });
    
    var blogs = await PaginatedList<BlogResponse>.CreateAsync(blogResponseQuery, request.PageIndex, request.PageSize);
    
    return Result<PaginatedList<BlogResponse>>.Success(blogs, StatusCodes.Status200OK);
  }

  public async Task<Result<BlogResponse>> GetBlogByIdAsync(Guid blogId)
  {
    var blog = await _unitOfWork.Blogs.GetByIdAsync(blogId);
    if (blog is null)
    {
      var error = _errorFactory.CreateNotFoundError("Blog");
      return Result<BlogResponse>.Failure([error.err], error.statusCode);
    }

    return Result<BlogResponse>.Success(blog.Adapt<BlogResponse>(), StatusCodes.Status200OK);
  }

  public async Task<Result<BlogResponse>> CreateBlogAsync(CreateBlogRequest request)
  {
    var validationResult = await _createBlogValidator.ValidateAsync(request);
    if (!validationResult.IsValid)
    {
      var errors =  _errorFactory.CreateValidationError("Blog", validationResult);
      return Result<BlogResponse>.Failure(errors.errs, errors.statusCode);
    }
    var blog = request.Adapt<Blog>();
    var staff = _httpContextAccessor.HttpContext?.User;
    // Will refactor this later
    if (staff is null || !staff.Identity.IsAuthenticated)
    {
      // For development, this will just add the default staff
      blog.StaffId = Guid.Parse("103e55ba-607a-4d27-8119-b0c22969f02a");
    }
    else
    {
      blog.StaffId = Guid.Parse(staff.FindFirst(ClaimTypes.NameIdentifier).Value); 
    }
    
    // Use Create instead of CreateAsync to increase performance
    _unitOfWork.Blogs.Create(blog);
    var isSaved = await _unitOfWork.CompleteAsync();
    if (!isSaved)
    {
      var error = _errorFactory.CreateDatabaseError("Blog");
      return Result<BlogResponse>.Failure([error.err], error.statusCode);
    }
    
    return Result<BlogResponse>.Success(blog.Adapt<BlogResponse>(), StatusCodes.Status200OK);
  }

  public async Task<Result<BlogResponse>> UpdateBlogAsync(Guid id, UpdateBlogRequest request)
  {
    var blog = await _unitOfWork.Blogs.GetByIdAsync(id);
    if (blog is null)
    {
      var error = _errorFactory.CreateNotFoundError("Blog");
      return Result<BlogResponse>.Failure([error.err], error.statusCode);
    }
    
    blog.Title = string.IsNullOrEmpty(request.Title) ? blog.Title : request.Title;
    blog.Content = string.IsNullOrEmpty(request.Content) ? blog.Content : request.Content;
    
    var isSaved = await _unitOfWork.CompleteAsync();
    if (!isSaved)
    {
      var error = _errorFactory.CreateDatabaseError("Blog");
      return Result<BlogResponse>.Failure([error.err], error.statusCode);
    }
    
    return Result<BlogResponse>.Success(blog.Adapt<BlogResponse>(), StatusCodes.Status200OK);
  }

  public async Task<Result<BlogResponse>> DeleteBlogAsync(Guid id)
  {
    var blog = await _unitOfWork.Blogs.GetByIdAsync(id);
    if (blog is null)
    {
      var error = _errorFactory.CreateNotFoundError("Blog");
      return Result<BlogResponse>.Failure([error.err], error.statusCode);
    }

    _unitOfWork.Blogs.Remove(blog);
    var isSaved = await _unitOfWork.CompleteAsync();
    if (!isSaved)
    {
      var error = _errorFactory.CreateDatabaseError("Blog");
      return Result<BlogResponse>.Failure([error.err], error.statusCode);
    }
    
    return Result<BlogResponse>.Success(blog.Adapt<BlogResponse>(), StatusCodes.Status200OK);
  }

  private bool IsValidStaff(ClaimsPrincipal? claimsPrincipal)
  {
    // if (claimsPrincipal is null)
    // {
    //   return false;
    // }
    // if(!claimsPrincipal.Identity.IsAuthenticated && claimsPrincipal.Claims.Any())

    return true;
  }
}
