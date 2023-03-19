using COMP1640.ViewModels.Catalog.Response;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace COMP1640.Services;

public class AnalysisService
{
    private readonly IIdeaRepository _ideaRepository;    
    private readonly IDepartmentRepository _departmentRepository;


    private readonly IAcademicYearRepository _academicYearRepository;
    public AnalysisService(IIdeaRepository ideaRepository, IAcademicYearRepository academicYearRepository, IDepartmentRepository departmentRepository)
    {
        _ideaRepository = ideaRepository;
        _academicYearRepository = academicYearRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<AnalysisResponse> GetAnalysisAsync(int? academicYearId)
    {
        var academicYear = academicYearId == null 
            ? await _academicYearRepository.GetCurrentAsync()
            : await _academicYearRepository.GetAsync(a => a.Id == academicYearId);

        if (academicYear == null) return new AnalysisResponse();

        return new AnalysisResponse
        {
            TotalIdeasTimelines = await GetTotalIdeasTimelinesAsync(academicYear),
            TotalIdeaDepartments = await GetTotalIdeasDepartmentAsync(academicYear.Id),
            TotalContributorDepartments = await GetTotalContributorsDepartmentAsync()
        };
    }

    private async Task<List<AnalysisLabelValueItem<int>>> GetTotalIdeasTimelinesAsync(AcademicYear academicYear)
    {
        var months = Enumerable.Range(0, (academicYear.ClosureDate.Year - academicYear.OpenDate.Year) * 12 + (academicYear.ClosureDate.Month - academicYear.OpenDate.Month) + 1)
            .Select(m => new DateTime(academicYear.OpenDate.Year, academicYear.OpenDate.Month, 1).AddMonths(m))
            .ToList();

        var data = await _ideaRepository.GetAllQuery()
            .Where(i => i.AcademicYearId == academicYear.Id && !i.IsDeleted)
            .GroupBy(x => new { x.CreatedOn.Year, x.CreatedOn.Month })
            .OrderBy(k => k.Key.Year)
            .ThenBy(k => k.Key.Month)
            .Select(x => new { Timeline = x.Key, TotalIdea = x.Count() })
            .ToListAsync();

        return months
            .GroupJoin(data, 
                month => new { month.Year, month.Month },
                dataItem => new { dataItem.Timeline.Year, dataItem.Timeline.Month },
                (month, dataItems) => new { Month = month, DataItems = dataItems })
            .SelectMany(
                x => x.DataItems.DefaultIfEmpty(),
                (month, dataItem) => new AnalysisLabelValueItem<int>
                {
                    Label = month.Month.ToString("MM/yy"),
                    Value = dataItem?.TotalIdea ?? 0
                })
            .ToList();
    }
    private async Task<List<AnalysisLabelValueItem<int>>> GetTotalIdeasDepartmentAsync(int academicYearId)
    {
        return await _departmentRepository.GetAllQuery()
            .Where(d => !d.IsDeleted)
            .Select(d => new AnalysisLabelValueItem<int>
            {
                Label = d.Name,
                Value = d.Ideas.Count(i => i.AcademicYearId == academicYearId)
            })
            .ToListAsync();
    }
    private async Task<List<AnalysisLabelValueItem<int>>> GetTotalContributorsDepartmentAsync()
    {
        return await _departmentRepository.GetAllQuery()
            .Where(d => !d.IsDeleted)
            .Select(g => new AnalysisLabelValueItem<int>()
            {
                Label = g.Name,
                Value = g.UserDepartments.Count()
            })
            .ToListAsync();
    }
}