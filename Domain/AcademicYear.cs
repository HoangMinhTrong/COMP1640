namespace Domain
{
    public class AcademicYear
    {
        public AcademicYear()
        {
        }

        public AcademicYear(string name, DateTime closureDate, DateTime finalClosureDate, DateTime endDate)
        {
            Name = name;
            ClosureDate = closureDate;
            FinalClosureDate = finalClosureDate;
            EndDate = endDate;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ClosureDate { get; set; }
        public DateTime FinalClosureDate { get; set; }
        public DateTime EndDate { get; set; }

        public int TenantId { get; set; }

        public virtual ICollection<Idea> Ideas { get; set; } = new HashSet<Idea>();

        public void UpdateAcademicYear(string name, DateTime closureDate, DateTime finalClosureDate, DateTime endDate)
        {
            Name = name;
            ClosureDate = closureDate;
            FinalClosureDate = finalClosureDate;
            EndDate = endDate;
        }
        
    }
}
