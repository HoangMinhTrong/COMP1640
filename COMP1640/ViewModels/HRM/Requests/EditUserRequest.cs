using Domain;
using System.ComponentModel.DataAnnotations;

namespace COMP1640.ViewModels.HRM.Requests
{
    public class EditUserRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public RoleTypeEnum Role { get; set; }

        public DateTime? Birthday { get; set; }
        public UserGenderEnum? Gender { get; set; }
    }
}
