namespace COMP1640.ViewModels.Idea.Requests
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
