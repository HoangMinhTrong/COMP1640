namespace COMP1640.ViewModels.Comment.Responses;

public class CommentInfoResponse
{
    public CommentInfoResponse()
    {
        
    }

    public CommentInfoResponse(int id, string content, string userName, int userId)
    {
        Id = id;
        Content = content;
        UserName = userName;
        UserId = userId;
    }

    public int Id { get; set; }
    public string Content { get; set; }
    public string UserName { get; set; }
    public int IdeaId { get; set; }
    public int UserId { get; set; }
    public bool IsAnonymous { get; set; }
}