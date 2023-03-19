namespace COMP1640.ViewModels.EmailModel
{
    public class IdeaApprovalEmailModel
    {
        public IdeaApprovalEmailModel(Domain.Idea idea, string aPIRoute)
        {
            Idea = idea;
            APIRoute = aPIRoute;
        }

        public Domain.Idea Idea { get; set; }
        public string APIRoute { get; set; }
    }
}
