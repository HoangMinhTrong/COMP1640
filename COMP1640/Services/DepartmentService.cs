using COMP1640.ViewModels.Department.Requests;
using COMP1640.ViewModels.Department.Responses;
using COMP1640.ViewModels.Department.Responses;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace COMP1640.Services;

public class DepartmentService
{

    private readonly IDepartmentRepository _departmentRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DepartmentService(IDepartmentRepository department, IUnitOfWork unitOfWork)
    {
        _departmentRepository = department;
        _unitOfWork = unitOfWork;
    }


    public async Task<bool> CreateDepartment(CreateDepartmentRequest departmentRequest)
    {
        var department = new Department(departmentRequest.Name);


        await _departmentRepository.InsertAsync(department);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }


    public async Task<List<InforDepartmentResponse>> GetListDepartment(GetListDepartmentRequest request)
    {
        return await _departmentRepository
        .GetQuery(request.Filter())
            .Select(_ => new InforDepartmentResponse
            {
                Id = _.Id,
                Name = _.Name,
                TenantId = _.TenantId,
                //QaCoordinatorId = (int)_.QaCoordinatorId,
            })
            .ToListAsync();
    }
}



