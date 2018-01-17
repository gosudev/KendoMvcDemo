using System.Web.Mvc;
using Autofac;

namespace KendoMvcDemo.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            IocContext = DependencyResolver.Current.GetService<IComponentContext>();
        }

        protected IComponentContext IocContext { get; private set; }
    }
}