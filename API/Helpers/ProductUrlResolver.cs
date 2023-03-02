using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    // to change this  "pictureUrl": "images/products/sb-ang1.png", to this   "pictureUrl": "https://localhost:5001/images/products/sb-ang1.png",
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;

        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}