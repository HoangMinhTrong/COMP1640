using COMP1640.ViewModels.AcademicYear;
using COMP1640.ViewModels.AcademicYear.Request;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace COMP1640.Services;

public class AcademicYearService
{
    private readonly IAcademicYearRepository _academicYearRepository;
    private readonly IUnitOfWork _unitOfWork;


    public AcademicYearService(IAcademicYearRepository academicYearRepository, IUnitOfWork unitOfWork)
    {
        _academicYearRepository = academicYearRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<AcademicYearResponse>> GetAcademicYearsAsync()
    {
        var academicYearResponses = await _academicYearRepository.GetAllQuery()
            .OrderByDescending(y => y.EndDate)
            .Select(new AcademicYearResponse().GetSelection())
            .ToListAsync();

        return academicYearResponses;
    }

    public async Task<bool> CreateAcademicYearAsync(UpsertAcademicYearRequest request)
    {
        var isValid = await IsGreaterThanLatestAcademicYearAsync(request.ClosureDate);

        if (!isValid) return false;

        var academicYear =
            new AcademicYear(request.Name, request.ClosureDate, request.FinalClosureDate, request.EndDate);

        await _academicYearRepository.InsertAsync(academicYear);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    private async Task<bool> IsGreaterThanLatestAcademicYearAsync(DateTime requestClosureDate)
    {
        var latestAcademicYear = await _academicYearRepository.GetLatestAcademicYearAsync();
        return requestClosureDate > latestAcademicYear?.EndDate;
    }
}