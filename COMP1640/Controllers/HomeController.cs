using COMP1640.Services;
using COMP1640.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;
using Utilities.Helpers;

namespace COMP1640.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IdeaService _ideaService;
        private readonly CategoryService _categoryService;
        

        public HomeController(ILogger<HomeController> logger, IdeaService ideaService, CategoryService categoryService)
        {
            _logger = logger;
            _ideaService = ideaService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(GetIdeaIndexRequest request)
        {
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}