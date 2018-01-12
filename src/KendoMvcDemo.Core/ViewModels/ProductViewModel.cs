using KendoMvcDemo.Core.Persistence.Models;

namespace KendoMvcDemo.Core.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public Product ConvertToDomainModel()
        {
            return new Product()
            {
                ProductId = this.ProductId,
                Name = this.Name
            };
        }
    }
}
