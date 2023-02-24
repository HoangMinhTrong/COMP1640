using System.Linq.Expressions;
using COMP1640.ViewModels.HRM.Responses;
using Domain;

namespace COMP1640.ViewModels.AcademicYear;

public class AcademicYearResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ClosureDate { get; set; }
    public DateTime FinalClosureDate { get; set; }
    public DateTime EndDate { get; set; }

    
    public Expression<Func<Domain.AcademicYear, AcademicYearResponse>> GetSelection()
    {
        return _ => new AcademicYearResponse()
        {
            Id = _.Id,
            Name = _.Name,
            ClosureDate = _.ClosureDate,
            FinalClosureDate = _.FinalClosureDate,
            EndDate = _.EndDate
        };
    }
}