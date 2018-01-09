using KendoMvcDemo.Core.Models;

namespace KendoMvcDemo.Core.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<KendoMvcDemo.Core.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(KendoMvcDemo.Core.Models.DataContext context)
        {
           
        }
    }
}
