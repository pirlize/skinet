
namespace API.Dtos
{ //A Data Transfer Object is a container basically for moving data between layers and does not contain business logic
  // the reason is to flat the response we get instead of nested json objects in brands and types e.g.
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }

    }
}