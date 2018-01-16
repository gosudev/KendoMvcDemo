using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KendoMvcDemo.Core.Persistence.Models
{
    [Table("Complaint")]
    public class Complaint
    {
        public int ComplaintId { get; set; }
        public string Title { get; set; }
        public string WhatHappend { get; set; }
        public string Company { get; set; }
        public DateTime SentDate { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

    [Table("ComplaintViewSearch")]
    public class ComplaintSearch 
    {
        [Key]
        public int ComplaintId { get; set; }
        public string Title { get; set; }
        public string WhatHappend { get; set; }
        public string Company { get; set; }
        public DateTime SentDate { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string SummarySearchColumn { get; set; }
    }
}
