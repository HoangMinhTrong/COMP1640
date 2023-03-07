using System.ComponentModel.DataAnnotations;

namespace COMP1640.ViewModels.PersonalDetail
{
    public class ChangePasswordRequest
    {
        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
