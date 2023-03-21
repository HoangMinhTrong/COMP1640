using Domain;
using Domain.Interfaces;

namespace Utilities.Helpers;

public class ActivityTimelineValidation
{
    private readonly IAcademicYearRepository _academicYearRepository;
    public ActivityTimelineValidation(IAcademicYearRepository academicYearRepository)
    {
        _academicYearRepository = academicYearRepository;
    }

    public async Task<bool> IsEnableSubmitIdea()
    {
        return await _academicYearRepository.IsEnableSubmitIdea();
    }
    
    public async Task<bool> IsEnableSubmitComment()
    {
        return await _academicYearRepository.IsEnableSubmitComment();
    }
}