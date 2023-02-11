using Domain;

namespace Utilities.Identity.Models
{
    public class CookieUserModel
    {
        public CookieUserModel(int id
            , string userName
            , string email
            , RoleTypeEnum role
            , int tenantId)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Role = role;
            TenantId = tenantId;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public RoleTypeEnum Role { get; set; }
        public int TenantId { get; set; }
    }
}
