using COMP1640.Services;
using COMP1640.ViewModels.Common;
using COMP1640.ViewModels.Idea.Requests;
using COMP1640.ViewModels.PersonalDetail;
using Microsoft.AspNetCore.Mvc;
using Utilities.Helpers;

namespace COMP1640.Controllers
{
    [Route("personal")]
    public class PersonalController : Controller
    {

        private readonly PersonalService _personalService;
        private readonly IdeaService _ideaService;
        private readonly CategoryService _categoryService;

        public PersonalController(
            PersonalService personalService, 
            IdeaService ideaService, 
            CategoryService categoryService)
        {
            _personalService = personalService;
            _ideaService = ideaService;
            _categoryService = categoryService;

        }
        [HttpGet("profile")]
        public async Task<IActionResult> ViewProfile()
        {
            var pf = await _personalService.GetProfileDetailsAsync();
            return Json(pf);
        }

        public async Task<IActionResult> ViewYourIdea([FromQuery] GetIdeaIndexRequest request)
        {
            var ideas = await _ideaService.GetPersonalIdeaIndexAsync(request);

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
            return View(response);
        }

        [HttpGet("edit/{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var vm = await _ideaService.GetIdeaByIdAsync(id);
            return Json(vm);
        }

        [HttpPut("editIdea/{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] EditIdeaRequest request)
        {
            if (!ModelState.IsValid) return RedirectToAction("ViewYourIdea");

            var isSucceed = await _ideaService.EditIdeaAsync(id, request);
            if (isSucceed) return Ok();

            ModelState.AddModelError("edit_failure", "Failure to edit an idea.");
            return RedirectToAction("ViewYourIdea");
        }

        [HttpPut("togglesoftdeleteidea/{id:int}")]
        public async Task<IActionResult> ToggleSoftDelete([FromRoute] int id)
        {
            var isSucceed = await _ideaService.SoftDeleteIdeaAsync(id);
            if (isSucceed) return Ok();

            ModelState.AddModelError("delete_failure", "Failure to delete an idea.");
            return RedirectToAction("Index");
        }

        [HttpGet("recyclebin")]
        public async Task<IActionResult> RecycleBin()
        {
            var deletedIdeas = await _ideaService.GetDeletedIdeaAsync();
            return View(deletedIdeas);
        }

        [HttpDelete("harddeleteidea/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var isSucceed = await _ideaService.HardDeleteIdeaAsync(id);
            if (isSucceed) return Ok();

            ModelState.AddModelError("delete_failure", "Failure to delete an idea.");
            return RedirectToAction("Index");
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            await _personalService.ChangePasswordAsync(request);
            return Ok();
        }

    }
}