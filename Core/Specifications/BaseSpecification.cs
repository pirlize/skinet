using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }
        //initialize it to empty list to add items to list
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
//the point of all this is to get rid of the include statements in productsRepo
//(tolistAsync)Asynchronously creates a List<T> from an IQueryable<out T> by enumerating it asynchronously.
// in products Repo
//Where and include are the IQuerable and it doesnt send it to the db until it hits ToListAsync.
// var typeId = 1;
// var products = _context.Products.Where(x => x.ProductTypeId == typeId).Include(x => x.ProductType).ToListAsync();