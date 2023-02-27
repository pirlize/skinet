using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id); //We say async just to clarify
        Task<IReadOnlyList<Product>> GetProductsAsync(); //IReadOnly to be more specific and not want actions
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}