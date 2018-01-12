using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoMvcDemo.Core.Persistence;
using KendoMvcDemo.Core.Services;
using KendoMvcDemo.Core.ViewModels;

namespace KendoMvcDemo.Web.Controllers
{
    public class ComplaintController : Controller
    {
        private readonly DataContext _db = new DataContext();
        private readonly ComplaintService _complaintService;
        private const string FORM_SEARCH_FILTER_KEY = "FORM_SEARCH_FILTER_KEY";

        public ComplaintController()
        {
            _complaintService = new ComplaintService(_db);
        }

        public ActionResult Index()
        {
            PopulateProductsDropdown();

            return View();
        }

        public ActionResult EditingPopup_Read([DataSourceRequest]DataSourceRequest request)
        {
            string searchTerm = TempData["FORM_SEARCH_FILTER_KEY"] as string;

            DataSourceResult result = _complaintService.Search(searchTerm).ToDataSourceResult(request);

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

        [HttpPost]
        public ActionResult Search(FormCollection formData)
        {
            TempData["FORM_SEARCH_FILTER_KEY"] = formData["TextBoxSearchData"];

            PopulateProductsDropdown();

            return View("Index");
        }

        private void PopulateProductsDropdown()
        {
            ViewData["products"] = _db.Products.Select(x => new ProductViewModel()
            {
                ProductId = x.ProductId,
                Name = x.Name
            });
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
