using COMP1640.Services;
using COMP1640.ViewModels.Category.Requests;
using COMP1640.ViewModels.Idea.Requests;
using COMP1640.ViewModels.Idea.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace COMP1640.Controllers
{
    [Route("idea")]
    public class IdeaController : Controller
    {
        private readonly IdeaService _ideaService;
        private readonly CategoryService _categoryService;
        public IdeaController(IdeaService ideaService, CategoryService categoryService)
        {
            _ideaService = ideaService;
            _categoryService = categoryService;
        }
        // [HttpGet]
        // public async Task<IActionResult> Create()
        // {
        //     var category_list = await _ideaService.GetCategoryForCreateIdeaAsync();
        //     ViewBag.Categories = category_list.Categories?.Select(c => new SelectListItem()
        //     {
        //         Value = c.Id.ToString(),
        //         Text = c.Name,
        //     }).ToList();
        //     return View();
        // }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIdeaRequest request)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Home");
            
            var isSucceed = await _ideaService.CreateIdeaAsync(request);
            if (isSucceed) return RedirectToAction("Index", "Home");

            ModelState.AddModelError("create_exception", "Failure to create a new idea.");
            return View();
        }
        
        [HttpGet]
        [Route("category")]
        public async Task<IActionResult> ViewCategory([FromQuery] GetListCategoryRequest request)
        {
            var category = await _categoryService.GetListCategory(request);
            return View(category);
        }

        [HttpPost]
        [Route("category")]
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
    }
}
