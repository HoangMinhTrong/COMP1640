using COMP1640.Services;
using COMP1640.ViewModels.Catalog.Response;
using COMP1640.ViewModels.Category.Requests;
using COMP1640.ViewModels.Comment.Requests;
using COMP1640.ViewModels.Common;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Utilities.Helpers;
using Utilities.Identity.Interfaces;
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
        private readonly ICurrentUserInfo _currentUser;

        public IdeaController(IdeaService ideaService
            , CategoryService categoryService
            , AttachmentService attachmentService
            , CommentService commentService
            , ICurrentUserInfo currentUser)
        {
            _ideaService = ideaService;
            _categoryService = categoryService;
            _attachmentService = attachmentService;
            _commentService = commentService;
            _currentUser = currentUser;
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

        [HttpGet("request-list")]
        public async Task<IActionResult> ViewRequestList([FromQuery] GetIdeaIndexRequest request)
        {;
            var ideas = await _ideaService.GetIdeaIndexAsync(request
                , departmentId: _currentUser.DepartmentId
                , status: IdeaStatusEnum.Waiting);

            var response = new IdeaIndexResponse()
            {
                IdeaIndexItems = ideas,
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
            return View(response);
        }

        [HttpPut("{id:int}/approve")]
        public async Task<IActionResult> ApproveIdea([FromRoute] int id)
        {
            await _ideaService.ApproveAsync(id);
            return Ok();
        }

        [HttpPut("{id:int}/reject")]
        public async Task<IActionResult> RejectIdea([FromRoute] int id)
        {
            await _ideaService.RejectAsync(id);
            return Ok();
        }
    }
}
