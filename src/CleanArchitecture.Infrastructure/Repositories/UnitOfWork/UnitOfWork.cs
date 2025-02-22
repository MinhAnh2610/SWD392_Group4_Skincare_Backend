using CleanArchitecture.Domain.RepositoryContracts;
using CleanArchitecture.Domain.RepositoryContracts.UnitOfWork;

namespace CleanArchitecture.Infrastructure.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
  private readonly ApplicationDbContext _context;
  
  public IBatchRepository Batches { get; }
  public IBlogRepository Blogs { get; }
  public IBlogTagRepository BlogTags { get; }
  public IBrandRepository Brands { get; }
  public ICartItemRepository CartItems { get; }
  public ICartRepository Carts { get; }
  public ICategoryRepository Categories { get; }
  public ICompanyInformationRepository CompanyInformation { get; }
  public ICosmeticImageRepository CosmeticImages { get; }
  public ICosmeticRepository Cosmetics { get; }
  public ICosmeticSubCategoryRepository CosmeticSubCategories { get; }
  public ICosmeticTypeRepository CosmeticTypes { get; } 
  public ICouponRepository Coupons { get; }
  public IFAQRepository FAQs { get; }
  public IFeedbackRepository Feedbacks { get; }
  public IOrderItemRepository OrderItems { get; }
  public IOrderRepository Orders { get; }
  public IPaymentRepository Payments { get; }
  public IPolicyRepository Policies { get; }
  public IQuestionOptionRepository QuestionOptions { get; }
  public IQuestionRepository Questions { get; }
  public IQuestionTypeRepository QuestionTypes { get; }
  public IQuizRepository Quizs { get; }
  public IQuizResultRepository QuizResults { get; }
  public IQuizAnswerRepository QuizAnswers { get; }
  public IRefundRepository Refunds { get; }
  public IRefundItemRepository RefundItems { get; }
  public IRoutineRepository Routines { get; }
  public IRoutineStepRepository RoutineSteps { get; }
  public ISkinTypeRepository SkinTypes { get; }
  public ISubCategoryRepository SubCategories { get; }
  public ITagRepository Tags { get; }
  public ITestimonialRepository Testimonials { get; }

  public UnitOfWork(ApplicationDbContext context)
  {
    _context = context;
    Batches = new BatchRepository(_context);
    Blogs = new BlogRepository(_context);
    BlogTags = new BlogTagRepository(_context);
    Brands = new BrandRepository(_context);
    CartItems = new CartItemRepository(_context);
    Carts = new CartRepository(_context);
    Categories = new CategoryRepository(_context);
    CompanyInformation = new CompanyInformationRepository(_context);
    CosmeticImages = new CosmeticImageRepository(_context);
    Cosmetics = new CosmeticRepository(_context);
    CosmeticSubCategories = new CosmeticSubCategoryRepository(_context);
    CosmeticTypes = new CosmeticTypeRepository(_context);
    Coupons = new CouponRepository(_context);
    FAQs = new FAQRepository(_context);
    Feedbacks = new FeedbackRepository(_context);
    OrderItems = new OrderItemRepository(_context);
    Orders = new OrderRepository(_context);
    Payments = new PaymentRepository(_context);
    Policies = new PolicyRepository(_context);
    QuestionOptions = new QuestionOptionRepository(_context);
    Questions = new QuestionRepository(_context);
    QuestionTypes = new QuestionTypeRepository(_context);
    Quizs = new QuizRepository(_context);
    QuizResults = new QuizResultRepository(_context);
    QuizAnswers = new QuizAnswerRepository(_context);
    Refunds = new RefundRepository(_context);
    RefundItems = new RefundItemRepository(_context);
    Routines = new RoutineRepository(_context);
    RoutineSteps = new RoutineStepRepository(_context);
    SkinTypes = new SkinTypeRepository(_context);
    SubCategories = new SubCategoryRepository(_context);
    Tags = new TagRepository(_context);
    Testimonials = new TestimonialRepository(_context);
  }

  public async Task<int> CompleteAsync()
  {
    return await _context.SaveChangesAsync();
  }

  public void Dispose()
  {
    _context.Dispose();
  }
}
