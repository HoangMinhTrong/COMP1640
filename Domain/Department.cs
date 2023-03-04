using Domain.Base;

namespace Domain
{
    public class Department : TenantEntity<int>
    {
        public Department()
        {
        }
        public Department(string name, int qaCoordinatorId)
        {
            Name = name;
            QaCoordinatorId = qaCoordinatorId;
        }

        public bool IsDeleted { get; set; }



        public string Name { get; set; }
        public int? QaCoordinatorId { get; set; }
        public virtual User QaCoordinator { get; set; }

        public virtual ICollection<Idea> Ideas { get; set; } = new HashSet<Idea>();
        public virtual ICollection<UserDepartment> UserDepartments { get; set; } = new HashSet<UserDepartment>();

        public void UpdateQaCoordinator(int userId)
        {
            QaCoordinatorId = userId;
        }
        public void SoftDeleteDepartment()
        {
            IsDeleted = true;
        }

        public void EditInfo(string name
            , int coordinatorId)
        {
            Name = name;
            QaCoordinatorId = coordinatorId;

        }
    }
}
