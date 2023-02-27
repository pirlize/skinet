
namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public ProductType ProductType { get; set; } //related entity
        //we give id to help entity framework when it creates migration its gonna know 
        //that Product has a relationship with ProductType and ProductBrand
        public int ProductTypeId { get; set; }
        //after creating the related entities we add to dbset in storecontext
        public ProductBrand ProductBrand { get; set; } //related entity
        public int ProductBrandId { get; set; }
    }
}