using System.ComponentModel.DataAnnotations;
using Utilities.ValidataionAttributes;

namespace COMP1640.ViewModels.Idea.Requests
{
    public class CreateIdeaRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public bool IsAnonymous { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public bool InUse = true;

        [FileSizeAttribute(20 * 1024 * 1024)] // max file size is 20MB
        public List<IFormFile> Formfiles { get; set; } = new List<IFormFile>();
    }
}
