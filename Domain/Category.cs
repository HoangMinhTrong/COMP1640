using Domain.Base;

namespace Domain
{
    public class Category : TenantEntity<int>
    {
        public Category()
        {

        }

        public Category(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Idea> Ideas { get; set; } = new HashSet<Idea>();
        public void SoftDelete()
        {
            IsDeleted = true;
        }
    }
}
