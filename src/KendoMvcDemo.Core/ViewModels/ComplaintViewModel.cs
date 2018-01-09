using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KendoMvcDemo.Core.Models;

namespace KendoMvcDemo.Core.ViewModels
{
    public class ComplaintViewModel
    {
        public int ComplaintId { get; set; }
        public string Title { get; set; }
        public string WhatHappend { get; set; }
        public string Company { get; set; }
        public DateTime SentDate { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
