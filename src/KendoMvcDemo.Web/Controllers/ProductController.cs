using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoMvcDemo.Core.Models;
using KendoMvcDemo.Core.ViewModels;

namespace KendoMvcDemo.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _db = new DataContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Product> products = _db.Products;
            DataSourceResult result = products.ToDataSourceResult(request, c => new ProductViewModel
            {
                ProductId = c.ProductId,
                Name = c.Name
            });

            return Json(result);
        }

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<Product> products = _db.Products;
            DataSourceResult result = products.ToDataSourceResult(request, c => new ProductViewModel
            {
                ProductId = c.ProductId,
                Name = c.Name
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, ProductViewModel product)
        {
            if (product != null && ModelState.IsValid)
            {
                _db.Products.Add(product.ConvertToDomainModel());
                _db.SaveChanges();
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, ProductViewModel product)
        {
            if (product != null && ModelState.IsValid)
            {

                var item = _db.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
                if (item != null)
                {
                    item.Name = product.Name;
                    _db.Entry(item).State = EntityState.Modified;
                    _db.SaveChanges();
                }
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, ProductViewModel product)
        {
            if (product != null)
            {
                var item = _db.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
                if (item != null)
                {
                    _db.Entry(item).State = EntityState.Deleted;
                    _db.SaveChanges();
                }
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
