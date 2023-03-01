using COMP1640.Services;
using Microsoft.AspNetCore.Mvc;

namespace COMP1640.Controllers
{
    [Route("reaction")]
    public class ReactionController : Controller
    {
        private readonly ReactionService _reactionService;

        public ReactionController(ReactionService reactionService)
        {
            _reactionService = reactionService;
        }

        [HttpPost("thumbup/{id:int}")]
        public async Task<ActionResult> ThumbUpAsync([FromRoute] int id)
        {
            var isSucceed = await _reactionService.CreateThumbUpAsync(id);
            if(isSucceed) return Json(new { success = true });
            return Json(new { success = false });
        }
        [HttpPost("thumbdown/{id:int}")]
        public async Task<ActionResult> ThumbDownAsync([FromRoute] int id)
        {
            var isSucceed = await _reactionService.CreateThumbDownAsync(id);
            if (isSucceed) return Json(new { success = true });
            return Json(new { success = false });
        }

        [HttpGet("checkstatus/{id:int}")]
        public async Task<ActionResult> CheckStatusAsync([FromRoute] int id)
        {
            var status = await _reactionService.CheckStatusBeforeAction(id);
            return Json(new { status = status });
        }
    }
}
