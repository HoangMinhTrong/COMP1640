using COMP1640.Services;
using COMP1640.ViewModels.Reaction.Requests;
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

        [HttpPost]
        public async Task<ActionResult> HandleReact([FromBody] ReactRequest request)
        {
            var result = await _reactionService.HandleReactionAsync(request);
            return Ok(result);
          
        }
    }
}
