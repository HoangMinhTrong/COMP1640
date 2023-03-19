using Domain;
using Domain.Interfaces;

namespace Utilities.Helpers;

public class ActivityTimelineValidation
{
    private readonly AcademicYear _academicYear;
    public ActivityTimelineValidation(IAcademicYearRepository academicYearRepository)
    {
        _academicYear = academicYearRepository.GetCurrentAsync().GetAwaiter().GetResult();
    }

    public bool IsEnableSubmitIdea ()
    {
        return DateTime.UtcNow < _academicYear.ClosureDate.ToUniversalTime();
    }
    
    public bool IsEnableSubmitComment ()
    {
        return DateTime.UtcNow < _academicYear.FinalClosureDate.ToUniversalTime();
    }
}