namespace Domain
{
    public class Category
    {
        public Category()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<Idea> Ideas { get; set; } = new HashSet<Idea>();
    }
}
