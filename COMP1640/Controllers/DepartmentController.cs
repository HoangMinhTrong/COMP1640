using Domain;
using Microsoft.AspNetCore.Mvc;
using COMP1640.ViewModels.Department.Requests;
using COMP1640.Services;
using COMP1640.ViewModels.HRM.Requests;
using COMP1640.ViewModels.HRM.Responses;
using COMP1640.ViewModels.Department.Responses;

namespace COMP1640.Controllers
{
    [Route("department")]
    public class DepartmentController : Controller
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)

        {
            _departmentService = departmentService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentRequest request)
        {
            await _departmentService.CreateDepartment(request);
            return RedirectToAction("ViewDepartment");
        }


        [HttpGet]
        public async Task<IActionResult> ViewDepartment([FromQuery] GetListDepartmentRequest request)
        {
            var deparment = await _departmentService.GetListDepartment(request);
            return View(deparment);
        }

        [HttpGet("coodinator-selection")]
        public async Task<IActionResult> GetCoordinatorForCreateDepartment()
        {
            var coordinators = await _departmentService.GetCoordinatorForCreateDepartmentAsync();
            return Ok(coordinators);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            var isSucceed = await _departmentService.DeleteDepartment(id);
            if (isSucceed) return Ok();
            return RedirectToAction("ViewDepartment");
        }
    }
}


