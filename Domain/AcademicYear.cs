namespace Domain
{
    public class AcademicYear
    {
        public AcademicYear()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ClosureDate { get; set; }
        public DateTime FinalClosureDate { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<Idea> Ideas { get; set; } = new HashSet<Idea>();
    }
}
