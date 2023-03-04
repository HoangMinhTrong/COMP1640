namespace COMP1640.ViewModels.Comment.Responses;

public class CommentInfoResponse
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string UserName { get; set; }
    public int IdeaId { get; set; }
    public int UserId { get; set; }
    public bool IsAnonymous { get; set; }
}