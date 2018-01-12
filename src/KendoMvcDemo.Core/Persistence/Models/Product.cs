using System.Collections.Generic;

namespace KendoMvcDemo.Core.Persistence.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public ICollection<Complaint> Complaint { get; set; }
    }
}
