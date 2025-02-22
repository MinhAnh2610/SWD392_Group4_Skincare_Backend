using CleanArchitecture.Domain.RepositoryContracts.Base;
using Microsoft.EntityFrameworkCore.Metadata;

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
    // Get the entity type metadata
    var entityType = _context.Model.FindEntityType(typeof(T));

    // Start building the query
    var query = _context.Set<T>().AsQueryable();

    // Include all navigation properties (first-level relationships)
    foreach (var navigation in entityType.GetNavigations())
    {
      query = query.Include(navigation.Name);
    }

    // Execute the query with all includes
    return await query.ToListAsync();
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

  public virtual async Task<T> GetByIdAsync(Guid id)
  {
    // Get the entity type metadata
    var entityType = _context.Model.FindEntityType(typeof(T));

    // Start building the query
    var query = _context.Set<T>().AsQueryable();

    // Include all first-level navigation properties
    foreach (var navigation in entityType.GetNavigations())
    {
      query = query.Include(navigation.Name);
    }

    // Find the entity by ID with all includes
    var entity = await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);

    if (entity != null)
    {
      // Detach the entity to avoid tracking (optional)
      _context.Entry(entity).State = EntityState.Detached;
    }

    return entity!;
  }
  public virtual void Attach (T entity) 
  {
      if (entity == null)
        {
      throw new ArgumentNullException(nameof(entity));
       }
    _context.Attach(entity);
  }
  public virtual async Task<List<T>> GetAllAsyncWithDepth(int level)
  {
    if (level < 1) level = 1;

    var entityType = _context.Model.FindEntityType(typeof(T));
    var query = _context.Set<T>().AsQueryable();
    var includePaths = new List<string>();

    void CollectPaths(IEntityType currentEntityType, int currentDepth, string currentPath)
    {
      foreach (var navigation in currentEntityType.GetNavigations())
      {
        var path = string.IsNullOrEmpty(currentPath)
            ? navigation.Name
            : $"{currentPath}.{navigation.Name}";

        includePaths.Add(path);

        if (currentDepth < level)
        {
          CollectPaths(navigation.TargetEntityType, currentDepth + 1, path);
        }
      }
    }

    CollectPaths(entityType, 1, "");

    foreach (var path in includePaths)
    {
      query = query.Include(path);
    }

    return await query.ToListAsync();
  }
  public virtual async Task<T> GetByIdAsyncWithDepth(Guid id, int level)
  {
    if (level < 1) level = 1;

    var entityType = _context.Model.FindEntityType(typeof(T));
    var query = _context.Set<T>().AsQueryable();
    var includePaths = new List<string>();

    void CollectPaths(IEntityType currentEntityType, int currentDepth, string currentPath)
    {
      foreach (var navigation in currentEntityType.GetNavigations())
      {
        var path = string.IsNullOrEmpty(currentPath)
            ? navigation.Name
            : $"{currentPath}.{navigation.Name}";

        includePaths.Add(path);

        if (currentDepth < level)
        {
          CollectPaths(navigation.TargetEntityType, currentDepth + 1, path);
        }
      }
    }

    CollectPaths(entityType, 1, "");

    foreach (var path in includePaths)
    {
      query = query.Include(path);
    }

    var entity = await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);

    if (entity != null)
    {
      _context.Entry(entity).State = EntityState.Detached;
    }

    return entity!;
  }
}
