using COMP1640.Services;
using COMP1640.ViewModels.Catalog.Response;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace COMP1640.Controllers;

[Route("analysis")]
public class AnalysisController : Controller
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly AnalysisService _analysisService;
    
    public AnalysisController(ApplicationDbContext applicationDbContext, AnalysisService analysisService)
    {
        _applicationDbContext = applicationDbContext;
        _analysisService = analysisService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAnalysis([FromQuery]int? academicYearId)
    {
        AnalysisResponse response = await _analysisService.GetAnalysisAsync(academicYearId);
        return Ok(response);
    }
}