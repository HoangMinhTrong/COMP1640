using COMP1640.Services;
using COMP1640.ViewModels.HRM.Requests;
using COMP1640.ViewModels.HRM.Responses;
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

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditUserRequest request)
        {
            await _hRMService.EditUserInfoAsync(request);
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            await _hRMService.CreateUserAsync(request);
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _hRMService.DeleteUserAsync(id);
            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ViewProfile(int id)
        {
            var profile = await _hRMService.GetUserInfoDetailsAsync(id);
            return View(profile);
        }
        
    }
}
