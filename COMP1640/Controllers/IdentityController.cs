using Microsoft.AspNetCore.Mvc;

namespace COMP1640.Controllers
{
    public class IdentityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
