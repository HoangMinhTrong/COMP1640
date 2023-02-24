﻿using COMP1640.Services;
using COMP1640.ViewModels.Idea.Requests;
using COMP1640.ViewModels.Idea.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace COMP1640.Controllers
{
    public class IdeaController : Controller
    {
        private readonly IdeaService _ideaService;

        public IdeaController(IdeaService ideaService)
        {
            _ideaService = ideaService;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var category_list = await _ideaService.GetCategoryForCreateIdeaAsync();
            ViewBag.Categories = category_list.Categories?.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            }).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIdeaRequest request)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Home");
            
            var isSucceed = await _ideaService.CreateIdeaAsync(request);
            if (isSucceed) return RedirectToAction("Index", "Home");

            ModelState.AddModelError("create_exception", "Failure to create a new idea.");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            //var vm = await _ideaService.GetIdeaDetailsAsync(ideaId);
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditIdeaRequest request)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");

            var isSucceed = await _ideaService.EditIdeaAsync(request);
            if (isSucceed) return RedirectToAction("Index", "Home");

            ModelState.AddModelError("edit_failure", "Failure to edit an idea.");
            return View();
        }
    }
}