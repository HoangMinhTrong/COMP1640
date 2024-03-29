﻿using Domain;
using System.Linq.Expressions;

namespace COMP1640.ViewModels.HRM.Responses
{
    public class UserDetailInfoResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public UserGenderEnum? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string? PhoneNumber { get; set; }


        public Expression<Func<User, UserDetailInfoResponse>> GetSelection()
        {
            return _ => new UserDetailInfoResponse
            {
                Id = _.Id,
                UserName = _.UserName,
                Email = _.Email,
                RoleId = _.RoleUsers.Select(_ => _.RoleId).FirstOrDefault(),
                Role = _.RoleUsers.Select(_ => _.Role.Name).FirstOrDefault(),
                DepartmentId = _.UserDepartments.Select(_ => _.DepartmentId).FirstOrDefault(),
                Department = _.UserDepartments.Select(_ => _.Department.Name).FirstOrDefault(),
                Gender = _.Gender,
                Birthday =  _.Birthday,
                PhoneNumber = _.PhoneNumber
            };
        }
    }
}
