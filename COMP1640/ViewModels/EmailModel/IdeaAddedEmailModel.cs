namespace COMP1640.ViewModels.EmailModel
{
    public class IdeaAddedEmailModel
    {
        public IdeaAddedEmailModel(Domain.Idea idea, string apiRoute)
        {
            Idea = idea;
            APIRoute = apiRoute;
        }

        public Domain.Idea Idea { get; set; }
        public string APIRoute { get; set; }
    }
}
