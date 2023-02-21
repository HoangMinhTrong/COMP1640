using COMP1640.ViewModels.Category.Requests;
using COMP1640.ViewModels.Category.Responses;
using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace COMP1640.Services;

public class CategoryService
{
    private readonly ICategoryRepository _category;
    private readonly IUnitOfWork _unitOfWork;
    public CategoryService(ICategoryRepository category, IUnitOfWork unitOfWork)
    {
        _category = category;
        _unitOfWork = unitOfWork;
        
    }

    public async Task<InforCategoryResponse> CreateCategory(InforCategoryResponse categoryResponse)
    {
        if (categoryResponse == null)
        {
            throw new Exception("Category information is null, please try again.");
        }

        var category = new Category()
        {
            Id = categoryResponse.Id,
            Name = categoryResponse.Name,
            TenantId = categoryResponse.TenantId
        };

        if (category.Id <= 0)
        {
            throw new Exception("Category ID is invalid, please try again.");
        }

        var obj = _category.Add(category);

        if (obj == null)
        {
            throw new Exception("Error while creating a category, please try again.");
        }

        return categoryResponse;

    }

}