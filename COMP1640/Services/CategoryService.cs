using COMP1640.ViewModels.Category.Requests;
using COMP1640.ViewModels.Category.Responses;
using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;


namespace COMP1640.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdeaRepository _ideaRepository;
    private readonly IToastNotification _toastNotification;
    public CategoryService(ICategoryRepository category, IUnitOfWork unitOfWork, IIdeaRepository ideaRepository, IToastNotification toastNotification)
    {
        _categoryRepository = category;
        _unitOfWork = unitOfWork;
        _ideaRepository = ideaRepository;
        _toastNotification = toastNotification;
    }

    public async Task<bool> CreateCategory(CreateCategoryRequest categoryRequest)
    {
        var category = new Category(categoryRequest.Name);


            await _categoryRepository.InsertAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return true;
    }


    public async Task<List<InforCategoryResponse>> GetListCategory(GetListCategoryRequest request)
    {
        return await _categoryRepository
            .GetQuery(request.Filter())
            .Select(_ => new InforCategoryResponse
            {
                Id = _.Id,
                Name = _.Name,
                TenantId = _.TenantId,
                IsDelete = _.IsDeleted,
            }).Where(x=>!x.IsDelete)
            .ToListAsync();
    }
    
    public async Task<bool> DeleteCategory(int id)
    {
        var category = await _categoryRepository.GetById(id).FirstOrDefaultAsync();
        var ideas = _ideaRepository.GetAllQuery().Where(x => x.CategoryId == id);
        if (category == null)
            return false;
        foreach (var idea in ideas)
        {
            if (idea.InUse == true)
            {
                _toastNotification.AddErrorToastMessage("Can not delete category when in used");
                throw new NotImplementedException("Can not delete category when in use");
            }
        }
        category.SoftDelete();
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
    public async Task<IEnumerable<SelectListItem>> GetCategoryPicklistAsync()
    {
        return await _categoryRepository.GetAllQuery()
            .Select(_ => new SelectListItem()
            {
                Text = _.Name,
                Value = _.Id.ToString()
            })
            .ToListAsync();
    }

}