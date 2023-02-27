
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //when the api call comes, it instanciates a productsController class and when it reads the ctor, it sees that it needs a service
        //and since our service is registered in the program.cs class then it can be used and access all of its methods
        private readonly IProductRepository _repo; //this gives us access to our db context ProductRepository, Dependency Injec
        public ProductsController(IProductRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        //we are telling it we want this http to return a List of Products
        //and we make it async by adding async task, await, tolistAsync
        //we make it async to be scalable, handle multiple requests/threads and not wait for a query or request to finish
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repo.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _repo.GetProductByIdAsync(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _repo.GetProductBrandsAsync()); //we wrap it in ok so .net allows us to return an IReadOnlyList
        }
        [HttpGet("productTypes")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _repo.GetProductTypesAsync());
        }
    }
}