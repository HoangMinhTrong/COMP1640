using Microsoft.Build.Framework;

namespace COMP1640.ViewModels.AcademicYear.Request;

public class UpsertAcademicYearRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public DateTime OpenDate { get; set; }
    [Required]
    public DateTime ClosureDate { get; set; }
    [Required]
    public DateTime FinalClosureDate { get; set; }
}