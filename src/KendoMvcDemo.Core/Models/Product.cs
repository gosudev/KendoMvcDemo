﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoMvcDemo.Core.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public ICollection<Complaint> Complaint { get; set; }
    }
}
