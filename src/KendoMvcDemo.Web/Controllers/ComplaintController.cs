using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoMvcDemo.Core.Models;
using KendoMvcDemo.Core.Services;
using KendoMvcDemo.Core.ViewModels;

namespace KendoMvcDemo.Web.Controllers
{
    public class ComplaintController : Controller
    {
        private readonly DataContext _db = new DataContext();
        private readonly ComplaintService _complaintService;

        public ComplaintController()
        {
            _complaintService = new ComplaintService(_db);
        }

        public ActionResult Index()
        {
            ViewData["products"] = _db.Products.Select(x => new ProductViewModel()
            {
                ProductId = x.ProductId,
                Name = x.Name
            });

            return View();
        }

        public ActionResult EditingPopup_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _complaintService.GetAll().ToDataSourceResult(request);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, ComplaintViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _complaintService.Create(model);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, ComplaintViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _complaintService.Update(model);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, ComplaintViewModel model)
        {
            if (model != null)
            {
                _complaintService.Destroy(model);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }


        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
