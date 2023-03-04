
using API.Dtos;
using API.errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        //when the api call comes, it instanciates a productsController class and when it reads the ctor, it sees that it needs a service
        //and since our service is registered in the program.cs class then it can be used and access all of its methods

        private readonly IGenericRepository<Product> _ProductsRepo;
        private readonly IGenericRepository<ProductBrand> _ProductBrandRepo;
        private readonly IGenericRepository<ProductType> _ProductTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductsRepo, IGenericRepository<ProductBrand> ProductBrandRepo, IGenericRepository<ProductType> ProductTypeRepo, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _ProductTypeRepo = ProductTypeRepo ?? throw new ArgumentNullException(nameof(ProductTypeRepo));
            _ProductBrandRepo = ProductBrandRepo ?? throw new ArgumentNullException(nameof(ProductBrandRepo));
            _ProductsRepo = ProductsRepo ?? throw new ArgumentNullException(nameof(ProductsRepo));

        }

        // replacing it with generic Repo
        //private readonly IProductRepository _repo; //this gives us access to our db context ProductRepository, Dependency Injec
        // public ProductsController(IProductRepository repo)
        // {
        //     _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        // }
        //we are telling it we want this http to return a List of Products
        //and we make it async by adding async task, await, tolistAsync
        //we make it async to be scalable, handle multiple requests/threads and not wait for a query or request to finish
        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithBrandsAndTypesSpecification();
            var products = await _ProductsRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
            //var products = await _ProductsRepo.ListAllAsync(); //pre specific pattern
            // return Ok(products);
            // return products.Select(product => new ProductToReturnDto
            // {
            //     Id = product.Id,
            //     Name = product.Name,
            //     Description = product.Description,
            //     PictureUrl = product.PictureUrl,
            //     Price = product.Price,
            //     ProductBrand = product.ProductBrand.Name,
            //     ProductType = product.ProductType.Name
            // }).ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)] //to tell swagger to know that it can return this as well, typeof to format it
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification(id); //this will run ctor with parameter
            var product = await _ProductsRepo.GetEntityWithSpec(spec);
            if (product == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Product, ProductToReturnDto>(product);
            // return new ProductToReturnDto
            // {
            //     Id = product.Id,
            //     Name = product.Name,
            //     Description = product.Description,
            //     PictureUrl = product.PictureUrl,
            //     Price = product.Price,
            //     ProductBrand = product.ProductBrand.Name,
            //     ProductType = product.ProductType.Name
            // };
            //return await _ProductsRepo.GetByIdAsync(id); //pre specific pattern
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _ProductBrandRepo.ListAllAsync()); //we wrap it in ok so .net allows us to return an IReadOnlyList
        }
        [HttpGet("productTypes")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _ProductTypeRepo.ListAllAsync());
        }
    }
}