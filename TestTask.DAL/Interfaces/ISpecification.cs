using System.Linq.Expressions;
namespace TestTask.DAL.Interfaces;
public interface ISpecification<TEntity>
{
    Expression<Func<TEntity, bool>> Criteria { get; }
}
