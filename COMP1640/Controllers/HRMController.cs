using COMP1640.Services;
using COMP1640.ViewModels.HRM.Requests;
using Microsoft.AspNetCore.Mvc;

namespace COMP1640.Controllers
{
    public class HRMController : Controller
    {
        private readonly HRMService _hRMService;

        public HRMController(HRMService hRMService)
        {
            _hRMService = hRMService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] GetListUserRequest request)
        {
            var vm = await _hRMService.GetListUserAsync(request);
            return View(vm);
        }


        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var vm = await _hRMService.GetUserInfoDetailsAsync(id);
            return View(vm);
        }
    }
}
