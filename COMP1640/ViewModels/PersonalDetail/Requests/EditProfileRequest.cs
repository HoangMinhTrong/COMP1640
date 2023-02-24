namespace COMP1640.ViewModels.PersonalDetail.Requests
{
    public class EditProfileRequest
    {
        
        public string Email { get; set; }       
        public string Username { get; set; }
        public int? Gender { get; set; }
        public string? Birthday { get; set; }
    }
}
