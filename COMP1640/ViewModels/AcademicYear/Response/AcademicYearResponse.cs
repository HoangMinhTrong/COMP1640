using System.Linq.Expressions;

namespace COMP1640.ViewModels.AcademicYear;

public class AcademicYearResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime ClosureDate { get; set; }
    public DateTime FinalClosureDate { get; set; }


    public Expression<Func<Domain.AcademicYear, AcademicYearResponse>> GetSelection()
    {
        return _ => new AcademicYearResponse()
        {
            Id = _.Id,
            Name = _.Name,
            OpenDate = _.OpenDate,
            ClosureDate = _.ClosureDate,
            FinalClosureDate = _.FinalClosureDate,
        };
    }
}