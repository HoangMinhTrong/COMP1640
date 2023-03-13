namespace COMP1640.ViewModels.Comment.Responses;

public class CommentInfoResponse
{
    public CommentInfoResponse()
    {
        
    }

    public int Id { get; set; }
    public string Content { get; set; }
    public int IdeaId { get; set; }
    public CommentAuthor? Author { get; set; }
    public bool IsAnonymous { get; set; }
}

public class CommentAuthor
{
    public int Id { get; set; }
    public string Name { get; set; }
}