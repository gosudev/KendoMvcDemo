using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoMvcDemo.Core.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new CustomDataContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //http://www.entityframeworktutorial.net/code-first/configure-one-to-many-relationship-in-code-first.aspx

            modelBuilder.Entity<Complaint>()
                .HasRequired<Product>(s => s.Product)
                .WithMany(g => g.Complaint)
                .HasForeignKey<int>(s => s.ProductId);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
    }

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
