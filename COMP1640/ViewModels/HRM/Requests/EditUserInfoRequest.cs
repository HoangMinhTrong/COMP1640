using Domain;

namespace COMP1640.ViewModels.HRM.Requests
{
    public class EditUserInfoRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public RoleTypeEnum Role { get; set; }
    }
}
