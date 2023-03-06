namespace COMP1640.ViewModels.EmailModel
{
    public class CommentAddedEmailModel
    {
        public CommentAddedEmailModel(Domain.Comment comment, string apiRoute)
        {
            Comment = comment;
            APIRoute = apiRoute;
        }

        public Domain.Comment Comment { get; set; }
        public string APIRoute { get; set; }
    }
}
