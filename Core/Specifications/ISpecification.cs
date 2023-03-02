using System.Linq.Expressions;


namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; } //Where clause
        List<Expression<Func<T, object>>> Includes { get; } //object is most generic thing in c#
    }
}