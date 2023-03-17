using COMP1640.Services;
using COMP1640.ViewModels.Category.Requests;
using COMP1640.ViewModels.Comment.Requests;
using Domain;
using Microsoft.AspNetCore.Mvc;
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
            var s3Object = await _attachmentService.GetS3Object(keyName);
            var file = await _attachmentService.GetAsync(keyName);

            return File(s3Object.ResponseStream
                , s3Object.Headers.ContentType
                , file.Name);
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
            if (idea == null) return NotFound();

            idea.Comments = await _commentService.CommentList(id);
            // Call IncreasesViewAsync asynchronously and continue execution
            await _ideaService.IncreasesViewAsync(id);
            return View(idea);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetIdeaHistories([FromQuery] int ideaId)
        {
            var ideaHistories = await _ideaService.GetIdeaHistoriesAsync(ideaId);
            return Ok(ideaHistories);
        }
    }
}
