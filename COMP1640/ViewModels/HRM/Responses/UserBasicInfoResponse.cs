using Domain;

namespace COMP1640.ViewModels.HRM.Responses
{
    public class UserBasicInfoResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public RoleTypeEnum Role { get; set; }
    }
}
