using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KendoMvcDemo.Core.Persistence.Models;

namespace KendoMvcDemo.Core.Persistence
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

            DbInterception.Add(new FtsInterceptor());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
    }
}
