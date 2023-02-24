using COMP1640.Services;
using COMP1640.ViewModels.PersonalDetail.Requests;
using Microsoft.AspNetCore.Mvc;

namespace COMP1640.Controllers
{
    public class PersonalController : Controller
    {

  
        private readonly PersonalService _personalService;

        public PersonalController(PersonalService personalService)
        {
            _personalService = personalService;

        }



        [HttpGet]        
        public async Task<IActionResult> ViewProfile()
        {
            var pf = await _personalService.GetProfileDetailsAsync();
            if (pf == null)
                ModelState.AddModelError("get_personal_profile", "Failure to get user profile details.");
            return View(pf);
        }



        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}