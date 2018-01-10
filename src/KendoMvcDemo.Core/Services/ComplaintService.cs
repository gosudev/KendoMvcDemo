using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using KendoMvcDemo.Core.Models;
using KendoMvcDemo.Core.ViewModels;

namespace KendoMvcDemo.Core.Services
{
    public class ComplaintService
    {
        private readonly DataContext _db;

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
                Product = new ProductViewModel()
                {
                    Name = x.Product.Name,
                    ProductId = x.Product.ProductId
                }
            }).ToList();
        }

        public void Destroy(ComplaintViewModel model)
        {
            var item = _db.Complaints.FirstOrDefault(x => x.ComplaintId == model.ComplaintId);
            if (item != null)
            {
                _db.Entry(item).State = EntityState.Deleted;
                _db.SaveChanges();
            }
        }

        public void Update(ComplaintViewModel model)
        {
            var item = _db.Complaints.FirstOrDefault(x => x.ComplaintId == model.ComplaintId);
            if (item != null)
            {
                item.Company = model.Company;
                item.SentDate = model.SentDate;
                item.Title = model.Title;
                item.WhatHappend = model.WhatHappend;

                item.ProductId = model.ProductId;

                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();

                var product = _db.Products.Find(model.ProductId);
                if (product == null) throw new ArgumentNullException("product");

                model.Product = new ProductViewModel() { ProductId = product.ProductId, Name = product.Name };
            }
        }

        public void Create(ComplaintViewModel model)
        {
            var product = _db.Products.Find(model.ProductId);

            model.Product = new ProductViewModel() { ProductId = model.ProductId, Name = product?.Name };

            var entity = model.ConvertToDomainModel();
            entity.Product = product;

            _db.Complaints.Add(entity);
            _db.SaveChanges();
        }
    }
}
