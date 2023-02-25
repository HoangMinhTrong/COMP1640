using COMP1640.ViewModels.Category.Requests;
using COMP1640.ViewModels.Category.Responses;
using Domain;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace COMP1640.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CategoryService(ICategoryRepository category, IUnitOfWork unitOfWork)
    {
        _categoryRepository = category;
        _unitOfWork = unitOfWork;
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
            })
            .ToListAsync();
    }

}