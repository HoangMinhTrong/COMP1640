namespace Domain
{
    public class RoleUser
    {
        public RoleUser()
        {

        }

        public RoleUser(User user, Role role)
        {
            User = user;
            Role = role;
        }

        public RoleUser(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        public int RoleId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
