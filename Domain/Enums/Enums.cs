using System.Runtime.Serialization;

namespace Domain
{
    public enum ReactionStatusEnum : byte
    {
        Like = 1,
        DisLike = 2,
    }

    public enum UserGenderEnum : byte
    {
        Male = 1,
        Female = 2,
    }

    public enum RoleTypeEnum : int
    {
        [EnumMember(Value = "Admin")]
        Admin = 1,
        
        [EnumMember(Value = "University QA Manager")]
        QAManager = 2,
        
        [EnumMember(Value = "Department QA Coordinator")]
        DepartmentQA = 3,
        
        [EnumMember(Value = "Staff")]
        Staff = 4
    }
}
