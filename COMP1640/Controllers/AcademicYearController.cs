using COMP1640.Services;
using COMP1640.ViewModels.AcademicYear.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace COMP1640.Controllers;

[Route("academic-year")]
public class AcademicYearController : Controller
{
    private readonly AcademicYearService _academicYearService;

    public AcademicYearController(AcademicYearService academicYearService)
    {
        _academicYearService = academicYearService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (TempData.ContainsKey("ErrorMessage"))
        {
            ModelState.AddModelError("error_message", TempData["ErrorMessage"].ToString());
        }
        
        var academicYearResponses = await _academicYearService.GetAcademicYearsAsync();
        return View(academicYearResponses.ToList());
    }

    public async Task<IActionResult> Create(UpsertAcademicYearRequest request)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index");

        var isSuccess = await _academicYearService.CreateAcademicYearAsync(request);

        if (!isSuccess)
        {
            TempData.Add("ErrorMessage", "Failure to create academic year");
        }
        return  RedirectToAction("Index");
    }
}