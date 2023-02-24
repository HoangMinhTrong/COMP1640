using COMP1640.Services;
using COMP1640.ViewModels.AcademicYear;
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
            TempData.Clear();
        }
        
        var academicYearResponses = await _academicYearService.GetAcademicYearsAsync();
        return View(academicYearResponses.ToList());
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<AcademicYearResponse?> GetById([FromRoute] int id)
    {
        return await _academicYearService.GetAcademicYearById(id);
        
    }

    [HttpPost]
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

    [HttpDelete]
    public async Task<IActionResult> Delete(int academicYearId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UpsertAcademicYearRequest request)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index");

        var isSuccess = await _academicYearService.UpdateAcademicYearAsync(id, request);

        return isSuccess ? Ok() : BadRequest(); 
    }
}