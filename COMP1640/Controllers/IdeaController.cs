using Amazon.S3;
using COMP1640.Services;
using COMP1640.ViewModels.Category.Requests;
using COMP1640.ViewModels.Common;
using COMP1640.ViewModels.Idea.Requests;
using Microsoft.AspNetCore.Mvc;
using Utilities.Helpers;
using Utilities.StorageService.Interfaces;

namespace COMP1640.Controllers
{
   [Route("idea")]
    public class IdeaController : Controller
    {
        private readonly IdeaService _ideaService;
        private readonly CategoryService _categoryService;
        private readonly IS3Service _attachmentService;

        public IdeaController(IdeaService ideaService
            , CategoryService categoryService
            , IS3Service attachmentService)
        {
            _ideaService = ideaService;
            _categoryService = categoryService;
            _attachmentService = attachmentService;
        }

        [HttpGet("attachments")]
        public async Task<IActionResult> GetAttachmentAsync(string keyName)
        {
            var stream = await _attachmentService.GetAsync(keyName);
            var presignedUrl = await _attachmentService.GetPresignedUrl(keyName);

            var reader = new StreamReader(stream);
            var fileContent = await reader.ReadToEndAsync();

            return Ok(presignedUrl);
        }

        [HttpPost("attachments")]
        public async Task PostAttachment(IFormFile formFile)
        {
            var result = await _attachmentService.UploadAsync(formFile);
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] GetIdeaIndexRequest request)
        {
            var ideas = await _ideaService.GetIdeaIndexAsync(request);
            
            var response = new IdeaIndexResponse()
            {
                IdeaIndexItems = ideas,
                SortOptionPicklist = EnumMemberAttributeHelper.GetSelectListItems<IdeaIndexSortingEnum>(),
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
    }
}
