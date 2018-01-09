using System;
using System.Collections.Generic;
using System.Linq;
using KendoMvcDemo.Core.Models;
using KendoMvcDemo.Core.ViewModels;

namespace KendoMvcDemo.Core.Services
{
    public class ComplaintService
    {
        private DataContext _db;
        public ComplaintService(DataContext db)
        {
            _db = db;
        }

        public IList<ComplaintViewModel> GetAll()
        {
            return _db.Complaints.Include("Product").Select(x => new ComplaintViewModel()
            {
                ComplaintId = x.ComplaintId,
                Title = x.Title,
                WhatHappend = x.WhatHappend,
                Company = x.Company,
                SentDate = x.SentDate,
                ProductId = x.ProductId,
                Product = x.Product
            }).ToList();
        }
    }
}
