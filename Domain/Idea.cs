namespace Domain
{
    public class Idea
    {
        public Idea()
        {

        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsAnonymous { get; set; }
        public int DepartmentId { get; set; }
        public int CategoryId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int TenantId { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual Department Department { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; } = new HashSet<Reaction>();
    }
}
