using System.Linq.Expressions;


namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; } //Where clause
        List<Expression<Func<T, object>>> Includes { get; } //object is most generic thing in c#
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        int Take { get; } //take how many products to show in page
        int Skip { get; } // how many to skip e.g if i have 10 results and say skip 5 i go straight to second page
        bool IsPagingEnabled { get; }
    }
}