using COMP1640.Services;
using COMP1640.ViewModels.Idea.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COMP1640.Controllers
{
    public class IdeaController : Controller
    {
        private readonly IdeaService _ideaService;

        public IdeaController(IdeaService ideaService)
        {
            _ideaService = ideaService;
        }
        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateIdeaRequest request)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Home");
            try
            {
                var isSucceed = await _ideaService.CreateIdeaAsync(request);
                if (isSucceed) return RedirectToAction("Index", "Home");

                ModelState.AddModelError("create_failure", "Failure to create a new idea.");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("create_exception", "Failure to create a new idea.");
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
