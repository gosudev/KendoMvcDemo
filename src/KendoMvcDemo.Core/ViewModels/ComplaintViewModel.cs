using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KendoMvcDemo.Core.Persistence.Models;

namespace KendoMvcDemo.Core.ViewModels
{
    public class ComplaintViewModel
    {
        [DisplayName("Number")]
        [ScaffoldColumn(false)]
        public int ComplaintId { get; set; }

        [Required]
        public string Title { get; set; }

        [DisplayName("What Happend")]
        [Required]
        public string WhatHappend { get; set; }

        [Required]
        public string Company { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Sent Date")]
        public DateTime SentDate { get; set; }

        [UIHint("ClientProduct")]
        [DisplayName("Product")]
        public int ProductId { get; set; }

        public ProductViewModel Product { get; set; }

        [ScaffoldColumn(false)]
        public string SummarySearchColumn { get; set; }

        public Complaint ConvertToDomainModel()
        {
            return new Complaint()
            {
                ComplaintId = this.ComplaintId,
                Title = this.Title,
                WhatHappend = this.WhatHappend,
                Company = this.Company,
                SentDate = this.SentDate,
                //SummarySearchColumn = this.SummarySearchColumn,
                ProductId = this.ProductId,
                Product = this.Product.ConvertToDomainModel()
            };
        }
    }
}
