using COMP1640.Services;
using COMP1640.ViewModels.Category.Requests;
using COMP1640.ViewModels.Comment.Requests;
using COMP1640.ViewModels.Idea.Requests;
using Microsoft.AspNetCore.Mvc;

namespace COMP1640.Controllers
{
    [Route("idea")]
    public class IdeaController : Controller
    {
        private readonly IdeaService _ideaService;
        private readonly CategoryService _categoryService;
        private readonly CommentService _commentService;
        public IdeaController(IdeaService ideaService, CategoryService categoryService, CommentService commentService)
        {
            _commentService = commentService;
            _ideaService = ideaService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] GetListIdeaRequest request)
        {
            var vm = await _ideaService.GetListIdeas(request);
            return View(vm);
        }

        [HttpGet("categories-selection")]
        public async Task<IActionResult> GetCategoryForCreateIdea()
        {
            var categories = await _ideaService.GetCategoryForCreateIdeaAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIdeaRequest request)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Home");

            var isSucceed = await _ideaService.CreateIdeaAsync(request);
            if (isSucceed) return RedirectToAction("Index", "Home");

            ModelState.AddModelError("create_exception", "Failure to create a new idea.");
            return View();
        }

        [HttpGet("category")]
        public async Task<IActionResult> ViewCategory([FromQuery] GetListCategoryRequest request)
        {
            var category = await _categoryService.GetListCategory(request);
            return View(category);
        }

        [HttpPost("category")]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            await _categoryService.CreateCategory(request);
            return RedirectToAction("ViewCategory");
        }

        [HttpPut("category/{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var isSucceed = await _categoryService.DeleteCategory(id);
            if (isSucceed) return Ok();

            ModelState.AddModelError("delete_failure", "Failure to delete an category.");
            return RedirectToAction("ViewCategory");
        }

        [HttpPost("comment")]
        public async Task<IActionResult> CommentIdea(CommentIdeaRequest request)
        {
            await _commentService.CommentIdea(request);
            return RedirectToAction("Index");
        }
        
        [HttpGet("viewdetail/{id:int}")]
        public async Task<IActionResult> ViewDetail([FromRoute] int id)
        {
            var ideas = await _ideaService.GetIdeaDetailsAsync(id);
            var comment = await _commentService.CommentList(id);
            ViewBag.Comments = comment;
            return View(ideas);
        }
        
    }
}
