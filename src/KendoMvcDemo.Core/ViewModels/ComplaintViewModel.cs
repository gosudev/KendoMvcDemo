using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KendoMvcDemo.Core.Models;

namespace KendoMvcDemo.Core.ViewModels
{
    public class ComplaintViewModel
    {
        [DisplayName("Number")]
        [ScaffoldColumn(false)]
        public int ComplaintId { get; set; }
        public string Title { get; set; }
        [DisplayName("What Happend")]
        public string WhatHappend { get; set; }
        public string Company { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Sent Date")]
        public DateTime SentDate { get; set; }
        [UIHint("ClientProduct")]
        public int ProductId { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
