using System.ComponentModel.DataAnnotations;

namespace COMP1640.ViewModels.Idea.Requests
{
    public class EditIdeaRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public bool IsAnonymous { get; set; }
        [Required]
        public int CategoryId { get; set; }

    }
}
