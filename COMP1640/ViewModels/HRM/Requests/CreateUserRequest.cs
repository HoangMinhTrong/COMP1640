using Domain;
using System.ComponentModel.DataAnnotations;
using Utilities.ValidataionAttributes;

namespace COMP1640.ViewModels.HRM.Requests
{
    public class CreateUserRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public RoleTypeEnum Role { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [DateOfBirth(MinAge = 18, MaxAge = 60)]
        public DateTime? Birthday { get; set; }
        
        public UserGenderEnum? Gender { get; set; }
    }
}
