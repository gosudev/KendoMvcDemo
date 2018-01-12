using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using KendoMvcDemo.Core.Persistence.Models;

namespace KendoMvcDemo.Core.Persistence
{
    public class CustomDataContextInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {

            for (int i = 0; i < 100; i++)
            {
                var product = new Product()
                {
                    ProductId = i,
                    Name = $"Product {i}"
                };

                context.Products.AddOrUpdate(product);

                var complaint = new Complaint()
                {
                    Company = $"Company {i}",
                    SentDate = DateTime.Now.AddDays(-i),
                    Title = $"Complaint {i}",
                    WhatHappend = $"Explanation text {i}",
                    Product = product
                };

                context.Complaints.Add(complaint);

            }

            base.Seed(context);
        }
    }
}
