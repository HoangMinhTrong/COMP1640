﻿namespace Utilities.Identity.Interfaces
{
    public interface ICurrentUserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int TenantId { get; set; }
        public int RoleId { get; set; }
    }

    public class CurrentUserInfo : ICurrentUserInfo
    {
        public CurrentUserInfo()
        {

        }

        public CurrentUserInfo(int id, string name, string email, int roleId, int tenantId)
        {
            Id = id;
            Name = name;
            Email = email;
            RoleId = roleId;
            TenantId = tenantId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int TenantId { get; set; }
        public int RoleId { get; set; }
    }
}
