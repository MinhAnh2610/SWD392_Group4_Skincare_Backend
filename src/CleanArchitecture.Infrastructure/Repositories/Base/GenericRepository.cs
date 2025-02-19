using CleanArchitecture.Domain.RepositoryContracts.Base;

namespace CleanArchitecture.Infrastructure.Repositories.Base;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
  protected readonly ApplicationDbContext _context;

  public GenericRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public virtual List<T> GetAll()
  {
    return _context.Set<T>().ToList();
  }

  public virtual async Task<List<T>> GetAllAsync()
  {
    return await _context.Set<T>().ToListAsync();
  }

  public virtual void Create(T entity)
  {
    _context.Add(entity);
    _context.SaveChanges();
  }

  public virtual async Task<int> CreateAsync(T entity)
  {
    _context.Add(entity);
    return await _context.SaveChangesAsync();
  }

  public virtual void Update(T entity)
  {
    var tracker = _context.Attach(entity);
    tracker.State = EntityState.Modified;
    _context.SaveChanges();
  }

  public virtual async Task<int> UpdateAsync(T entity)
  {
    var tracker = _context.Attach(entity);
    tracker.State = EntityState.Modified;
    return await _context.SaveChangesAsync();
  }

  public virtual bool Remove(T entity)
  {
    _context.Remove(entity);
    _context.SaveChanges();
    return true;
  }

  public virtual async Task<bool> RemoveAsync(T entity)
  {
    _context.Remove(entity);
    await _context.SaveChangesAsync();
    return true;
  }

  public virtual T GetById(int id)
  {
    var entity = _context.Set<T>().Find(id);
    if (entity != null)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }
    return entity!;
  }

  public virtual async Task<T> GetByIdAsync(int id)
  {
    var entity = await _context.Set<T>().FindAsync(id);
    if (entity != null)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }
    return entity!;
  }

  public virtual T GetById(string code)
  {
    var entity = _context.Set<T>().Find(code);
    if (entity != null)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }
    return entity!;
  }

  public virtual async Task<T> GetByIdAsync(string code)
  {
    var entity = await _context.Set<T>().FindAsync(code);
    if (entity != null)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }
    return entity!;
  }

  public virtual T GetById(Guid code)
  {
    var entity = _context.Set<T>().Find(code);
    if (entity != null)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }
    return entity!;
  }

  public virtual async Task<T> GetByIdAsync(Guid code)
  {
    var entity = await _context.Set<T>().FindAsync(code);
    if (entity != null)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }
    return entity!;
  }
}
