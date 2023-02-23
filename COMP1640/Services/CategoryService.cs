
using COMP1640.ViewModels.Category.Requests;
using COMP1640.ViewModels.Category.Responses;
using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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

    public async Task<InforCategoryResponse> CreateCategory(CreateCategoryRequest categoryRequest)
    {
        if (categoryRequest == null)
        {
            throw new Exception("Category information is null, please try again.");
        }
        var count =  _category.GetAll().Count();
        var infoCategory = new InforCategoryResponse()
        {
            Id = ++count,
            Name = categoryRequest.Name,
            TenantId = categoryRequest.TenantId,
        };
        var category = new Category()
        {
            Id = infoCategory.Id,
            Name = infoCategory.Name,
            TenantId = infoCategory.TenantId
        };

        if (category.Id <= 0)
        {
            throw new Exception("Category ID is invalid, please try again.");
        }

        var obj = await _category.Add(category);

        if (obj == null)
        {
            throw new Exception("Error while creating a category, please try again.");
        }
        return infoCategory;
    }


    public async Task<List<InforCategoryResponse>> GetListCategory(GetListCategoryRequest request)
    {
        return await _category
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