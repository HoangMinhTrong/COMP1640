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
        return DateTime.UtcNow < new DateTime(2023, 2, 18);
    }
    
    public bool IsEnableSubmitComment ()
    {
        return DateTime.Now < new DateTime(2023, 2, 18);
    }
}