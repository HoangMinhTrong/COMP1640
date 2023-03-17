using COMP1640.Services;
using COMP1640.ViewModels.Catalog.Response;
using COMP1640.ViewModels.Common;
using COMP1640.ViewModels.Idea.Requests;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Utilities.Helpers;

namespace COMP1640.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IdeaService _ideaService;
        private readonly CategoryService _categoryService;
        private readonly IToastNotification _toastNotification;


        public HomeController(ILogger<HomeController> logger, IdeaService ideaService, CategoryService categoryService, IToastNotification toastNotification)
        {
            _logger = logger;
            _ideaService = ideaService;
            _categoryService = categoryService;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] GetIdeaIndexRequest request)
        {
            var ideas = await _ideaService.GetIdeaIndexAsync(request);

            var response = new IdeaIndexResponse()
            {
                IdeaIndexItems = ideas,
                SortOptionPicklist = EnumHelper.GetSelectListItems<IdeaIndexSortingEnum>(),
                Categories = await _categoryService.GetCategoryPicklistAsync(),
                CurrentSearchString = request.SearchString,
                CurrentCategoryFilter = request.CategoryFilterOption,
                CurrentSort = request.SortOption,
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = request.PageNo,
                    TotalItems = ideas.TotalItems,
                    ItemsPerPage = ideas.Count,
                    TotalPages = ideas.TotalPages,
                    Next = ideas.HasNextPage,
                    Previous = ideas.HasPreviousPage
                }
            };
            return View("Index", response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIdeaRequest request)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(_ => _.Errors).ToList();
                foreach (var modelError in modelErrors)
                    _toastNotification.AddErrorToastMessage("Error: " + modelError.ErrorMessage);

                return RedirectToAction("Index", "Home");
            }


            var isSucceed = await _ideaService.CreateIdeaAsync(request);
            if (isSucceed) return RedirectToAction("Index", "Home");

            ModelState.AddModelError("create_exception", "Failure to create a new idea.");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AdminDashboard()
        {
            return View();
        }
    }
}