using Domain.Base;

namespace Domain
{
    public class AcademicYear : TenantEntity<int>
    {
        public AcademicYear()
        {
        }

        public AcademicYear(string name, DateTime openDate, DateTime closureDate, DateTime finalClosureDate)
        {
            Name = name;
            ClosureDate = closureDate;
            FinalClosureDate = finalClosureDate;
            OpenDate = openDate;
        }
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime ClosureDate { get; set; }
        public DateTime FinalClosureDate { get; set; }

        public virtual ICollection<Idea> Ideas { get; set; } = new HashSet<Idea>();

        public void UpdateAcademicYear(string name, DateTime openDate, DateTime closureDate, DateTime finalClosureDate)
        {
            Name = name;
            OpenDate = openDate;
            ClosureDate = closureDate;
            FinalClosureDate = finalClosureDate;
        }
        
    }
}
