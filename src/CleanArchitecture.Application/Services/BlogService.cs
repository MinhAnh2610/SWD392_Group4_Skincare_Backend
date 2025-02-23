using CleanArchitecture.Application.DTOs.BlogDto;
using CleanArchitecture.Application.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Services;

public class BlogService : IBlogService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IErrorFactory _errorFactory;
  private readonly IValidator<CreateBlogRequest> _createBlogValidator;
  public BlogService(IUnitOfWork unitOfWork, IErrorFactory errorFactory, IValidator<CreateBlogRequest> createBlogValidator)
  {
    _unitOfWork = unitOfWork;
    _errorFactory = errorFactory;
    _createBlogValidator = createBlogValidator;
  }

  public async Task<Result<List<BlogResponse>>> GetAllBlogsAsync()
  {
    var blogs = await _unitOfWork.Blogs.GetAllAsync();

    return Result<List<BlogResponse>>.Success(blogs.Select(b => new BlogResponse
    {
      Id = b.Id,
      StaffId = b.StaffId,
      Staff = b.Staff,
      Title = b.Title,
      Content = b.Content,
      BlogTags = b.BlogTags.ToList(),
    }).ToList(), StatusCodes.Status200OK);
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
}
