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
        Admin = 1,
        Director = 2,
        Manager = 3,
        Staff = 4
    }
}
