﻿using COMP1640.Services;
using COMP1640.ViewModels.HRM.Requests;
using COMP1640.ViewModels.HRM.Responses;
using Microsoft.AspNetCore.Mvc;

namespace COMP1640.Controllers
{
    [Route("[controller]")]
    public class HRMController : Controller
    {
        private readonly HRMService _hRMService;
        private readonly ILogger<HRMController> _logger;


        public HRMController(HRMService hRMService, ILogger<HRMController> logger)
        {
            _hRMService = hRMService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] GetListUserRequest request)
        {
            var vm = await _hRMService.GetListUserAsync(request);
            return View(vm);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditUserRequest request)
        {
            await _hRMService.EditUserInfoAsync(request);
            return View("Index");
        }

      
        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            try
            {
                var isSucceed =  await _hRMService.CreateUserAsync(request);
                if(isSucceed) return RedirectToAction("Index");
                
                ModelState.AddModelError("create_failure","Failure to create an account.");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("create_exception","Failure to create an account.");
                return RedirectToAction("Index");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _hRMService.DeleteUserAsync(id);
            return View("Index");
        }
        
        [HttpGet]
        [Route("role")]
        [ProducesResponseType(typeof(SelectPropertyForCreateAccountResponse), 200)]
        public async Task<IActionResult> GetRoleEnums()
        {
            var allowedRoleForCreateAccount = await _hRMService.GetRolesForCreateAccountAsync();
            return Ok(allowedRoleForCreateAccount);
        }
    }
}
