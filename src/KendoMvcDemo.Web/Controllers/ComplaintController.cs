using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoMvcDemo.Core.Models;
 using KendoMvcDemo.Core.ViewModels;

namespace KendoMvcDemo.Web.Controllers
{
    public class ComplaintController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Complaints_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Complaint> complaints = db.Complaints;
            DataSourceResult result = complaints.ToDataSourceResult(request, c => new ComplaintViewModel 
            {
                Id = c.Id,
                Title = c.Title,
                WhatHappend = c.WhatHappend,
                Company = c.Company,
                SentDate = c.SentDate,
                ProductId = c.ProductId
            });

            return Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
