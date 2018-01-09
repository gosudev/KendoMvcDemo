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
            return View();
        }

        public ActionResult Complaints_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = _complaintService.GetAll().ToDataSourceResult(request, c => c);

            return Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
