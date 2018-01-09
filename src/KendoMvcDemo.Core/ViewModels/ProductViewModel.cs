using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KendoMvcDemo.Core.Models;

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
