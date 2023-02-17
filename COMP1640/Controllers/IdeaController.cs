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
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateIdeaRequest request)
        {
            await _ideaService.CreateIdeaAsync(request);
            return View();
        }


    }
}
