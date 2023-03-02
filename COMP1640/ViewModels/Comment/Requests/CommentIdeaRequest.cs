namespace COMP1640.ViewModels.Comment.Requests;

public class CommentIdeaRequest
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int IdeaId { get; set; }
}