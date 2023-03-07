using COMP1640.Services;
using COMP1640.ViewModels.PersonalDetail;
using Microsoft.AspNetCore.Mvc;

namespace COMP1640.Controllers
{
    [Route("personal")]
    public class PersonalController : Controller
    {

        private readonly PersonalService _personalService;

        public PersonalController(PersonalService personalService)
        {
            _personalService = personalService;

        }
        [HttpGet("profile")]
        public async Task<IActionResult> ViewProfile()
        {
            var pf = await _personalService.GetProfileDetailsAsync();
            return Json(pf);
        }

        public IActionResult ViewYourIdea()
        {
            return View();
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            await _personalService.ChangePasswordAsync(request);
            return Ok();
        }

    }
}