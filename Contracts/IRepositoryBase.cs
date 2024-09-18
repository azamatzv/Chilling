using System.Linq.Expressions;

namespace Contracts;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll(bool tracking);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool tracking);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}