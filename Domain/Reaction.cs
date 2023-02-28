namespace Domain
{
    public class Reaction
    {
        public Reaction()
        {

        }
        public Reaction(int ideaId, int userId, ReactionStatusEnum status)
        {
            IdeaId = ideaId;
            UserId = userId;
            Status = status;
        }

        public int Id { get; set; }
        public int IdeaId { get; set; }
        public int UserId { get; set; }
        public ReactionStatusEnum Status { get; set; }

        public virtual Idea Idea { get; set; }
        public virtual User User { get; set; }
    }
}
