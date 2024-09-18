using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly RepositoryContext _context;
    protected RepositoryBase(RepositoryContext context)
    {
        this._context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public virtual void Create(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual void Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual void Update(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual IQueryable<T> FindAll(bool tracking) => !tracking ?
         _context.Set<T>().AsNoTracking() : _context.Set<T>();

    public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool tracking) => !tracking ?
        _context.Set<T>().Where(expression).AsNoTracking() : _context.Set<T>().Where(expression);
}