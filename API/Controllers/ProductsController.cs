
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //when the api call comes, it instanciates a productsController class and when it reads the ctor, it sees that it needs a service
        //and since our service is registered in the program.cs class then it can be used and access all of its methods
        private readonly StoreContext _context; //this gives us access to our db context storeContext, Dependency Injec
        public ProductsController(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        //we are telling it we want this http to return a List of Products
        //and we make it async by adding async task, await, tolistAsync
        //we make it async to be scalable, handle multiple requests/threads and not wait for a query or request to finish
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}