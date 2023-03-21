using COMP1640.Services;
using COMP1640.ViewModels.AcademicYear;
using COMP1640.ViewModels.AcademicYear.Request;
using Domain;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Utilities.ValidataionAttributes;

namespace COMP1640.Controllers;

[Route("academic-year")]
[COMP1640Authorize(RoleTypeEnum.Admin, RoleTypeEnum.QAManager)]
public class AcademicYearController : Controller
{
    private readonly AcademicYearService _academicYearService;
    private readonly IToastNotification _toastNotification;

    public AcademicYearController(AcademicYearService academicYearService
        , IToastNotification toastNotification)
    {
        _academicYearService = academicYearService;
        _toastNotification = toastNotification;
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
            _toastNotification.AddErrorToastMessage("Exception: Failure to create academic year");

        return RedirectToAction("Index");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _academicYearService.DeleteAcademicYearAsync(id);


        return result.IsLeft ? Ok() : BadRequest(result.Right.ErrorMessage);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UpsertAcademicYearRequest request)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index");

        var isSuccess = await _academicYearService.UpdateAcademicYearAsync(id, request);

        return isSuccess ? Ok() : BadRequest("Failure to update");
    }

    [HttpGet("{id:int}/data/export")]
    public async Task<IActionResult> DataExport([FromRoute] int id)
    {
        var response = await _academicYearService.ExportAcademicYearDataAsync(id);
        if (response == null)
        {
            _toastNotification.AddErrorToastMessage("Exception: Empty Data");
            return RedirectToAction("Index");
        }

        return File(response.Data, response.Type, response.Name);
    }
}