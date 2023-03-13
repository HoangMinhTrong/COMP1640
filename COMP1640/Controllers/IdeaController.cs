using COMP1640.Services;
using COMP1640.ViewModels.Category.Requests;
using COMP1640.ViewModels.Comment.Requests;
using COMP1640.ViewModels.Common;
using COMP1640.ViewModels.Idea.Requests;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utilities.Helpers;
using Utilities.ValidataionAttributes;

namespace COMP1640.Controllers
{
    [Route("idea")]
    [COMP1640Authorize(RoleTypeEnum.QAManager, RoleTypeEnum.DepartmentQA, RoleTypeEnum.Staff)]
    public class IdeaController : Controller
    {
        private readonly IdeaService _ideaService;
        private readonly CategoryService _categoryService;
        private readonly AttachmentService _attachmentService;
        private readonly CommentService _commentService;

        public IdeaController(IdeaService ideaService
            , CategoryService categoryService
            , AttachmentService attachmentService
            , CommentService commentService)
        {
            _ideaService = ideaService;
            _categoryService = categoryService;
            _attachmentService = attachmentService;
            _commentService = commentService;
        }

        [HttpGet("attachments/{keyName}/download")]
        public async Task<IActionResult> DownloadAttachment([FromRoute] string keyName)
        {
            var s3Object = await _attachmentService.GetAsync(keyName);
            return File(s3Object.ResponseStream
                , s3Object.Headers.ContentType
                , s3Object.Metadata["FileName"]);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {            
            return View("Index");
        }

        [HttpGet("categories-selection")]
        public async Task<IActionResult> GetCategoryForCreateIdea()
        {
            var categories = await _ideaService.GetCategoryForCreateIdeaAsync();
            return Ok(categories);
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
            return RedirectToAction("ViewDetail", new { id = request.IdeaId });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ViewDetail([FromRoute] int id)
        {
            var idea = await _ideaService.GetIdeaDetailsAsync(id);
            if(idea == null) return NotFound();

            idea.Comments = await _commentService.CommentList(id);
            return View(idea);
        }
    }
}
