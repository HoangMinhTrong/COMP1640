using System.ComponentModel.DataAnnotations;

namespace COMP1640.ViewModels.HRM.Requests
{
    public class EditUserRequest
    {

        [Required]
        public string Email { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public int? Gender { get; set; }
        public string? Birthday { get; set; }
    }
}
