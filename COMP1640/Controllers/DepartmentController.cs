using COMP1640.Services;
using COMP1640.ViewModels.Department.Requests;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Utilities.ValidataionAttributes;

namespace COMP1640.Controllers
{
    [Route("department")]
    [COMP1640Authorize(RoleTypeEnum.QAManager)]
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


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDepartmentInfo([FromRoute] int id)
        {
            var vm = await _departmentService.GetDepartmentInfoDetailsAsync(id);
            return Json(vm);
        }

        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] EditDepartmentRequest request)
        {
            if (!ModelState.IsValid) return RedirectToAction("ViewDepartment");
            var isSucceed = await _departmentService.EditDepartmentInfoAsync(id, request);
            if (isSucceed) return Ok();

            ModelState.AddModelError("delete_failure", "Failure to edit an department.");
            return RedirectToAction("ViewDepartment");
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


