using System.ComponentModel.DataAnnotations;

namespace COMP1640.ViewModels.Idea.Requests
{
    public class EditIdeaRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsAnonymous { get; set; }
        public int CategoryId { get; set; }

    }
}
