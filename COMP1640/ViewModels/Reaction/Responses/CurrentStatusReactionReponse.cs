using Domain;

namespace COMP1640.ViewModels.Reaction.Responses
{
    public class ReactionReponse
    {
        public ReactionStatusEnum? Status { get; set; }
        public int TotalLike { get; set; }
        public int TotalDisLike { get; set; }
    }
}
