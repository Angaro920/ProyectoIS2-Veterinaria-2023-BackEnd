using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Veterinaria_Proyecto.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Pets()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View();
        }
        public ActionResult ClinicHistory()
        {
            return View();
        }
        public ActionResult Clients()
        {
            return View();
        }
    }
}
