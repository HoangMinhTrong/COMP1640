using COMP1640.Services;
using COMP1640.Utils;
using COMP1640.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;

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
            var ideas = await _ideaService.GetIdeaIndexAsync(request);
            
            var response = new IdeaIndexResponse()
            {
                IdeaIndexItems = ideas,
                Categories = await _categoryService.GetCategoryPicklistAsync(),
                SortOptionPicklist = IdeaIndexOption.SortOptionPicklist,
                
                // Comment sort option switch since just sort by descending
                // PopularSortParm = request.SortOption == IdeaIndexOption.PopularIdeaSort
                //     ? IdeaIndexOption.PopularIdeaSortDesc
                //     : IdeaIndexOption.PopularIdeaSort,
                // LatestIdeaSortParm = request.SortOption == IdeaIndexOption.LatestIdeaSort
                //     ? IdeaIndexOption.LatestIdeaSortDesc
                //     : IdeaIndexOption.LatestIdeaSort,
                // LatestCommentSortParm = request.SortOption == IdeaIndexOption.LatestCommentSort
                //     ? IdeaIndexOption.LatestCommentSortDesc 
                //     : IdeaIndexOption.LatestCommentSort,
                // ReactionSortParm = request.SortOption == IdeaIndexOption.ReactionSort
                //     ? IdeaIndexOption.ReactionSortDesc 
                //     : IdeaIndexOption.ReactionSort,
                
                CurrentSearchString = request.SearchString,
                CurrentCategoryFilter = request.FilterOption,
                CurrentSort = request.SortOption,
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = request.PageNumber ?? 1,
                    TotalItems = ideas.TotalItems,
                    ItemsPerPage = ideas.Count,
                    TotalPages = ideas.TotalPages,
                    Next = ideas.HasNextPage,
                    Previous = ideas.HasPreviousPage
                }
            };
            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}