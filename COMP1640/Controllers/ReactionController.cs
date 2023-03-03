﻿using COMP1640.Services;
using Domain;
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

        [HttpDelete("cancelthumbup/{id:int}")]
        public async Task<ActionResult> DeleteThumbUpAsync([FromRoute] int id)
        {
            var isSucceed = await _reactionService.DeleteThumbUpAsync(id);
            if (isSucceed) return Ok();
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<ActionResult> HandleReact(int ideaId, ReactionStatusEnum reactionStatusEnum)
        {
            var result = await _reactionService.HandleReaction(ideaId, reactionStatusEnum);

            if (!result) return Json(new { success = false });
            
            // TODO: Get latest like, dislike count
            var ideaReaction = await _reactionService.GetIdeaReaction(ideaId);
            if (!result) return Json(new { success = true, like = ideaReaction.Item1, disLike = ideaReaction.Item2 });
            throw new NotImplementedException();
        }

        [HttpDelete("cancelthumbdown/{id:int}")]
        public async Task<ActionResult> DeleteThumbDownAsync([FromRoute] int id)
        {
            var isSucceed = await _reactionService.DeleteThumbDownAsync(id);
            if (isSucceed) return Ok();
            return Json(new { success = false });
        }

        [HttpGet("checkstatus/{id:int}")]
        public async Task<ActionResult> CheckStatusAsync([FromRoute] int id)
        {
            var statusModel = await _reactionService.CheckStatusBeforeAction(id);
            return Json(new { status = statusModel.status });
        }
        
        
    }
}
