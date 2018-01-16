using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendoMvcDemo.Core.Persistence.Models
{
    [Table("Product")]
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public ICollection<Complaint> Complaint { get; set; }
    }
}
