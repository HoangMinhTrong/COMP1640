using COMP1640.ViewModels.Department.Requests;
using COMP1640.ViewModels.Department.Responses;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace COMP1640.Services;

public class DepartmentService
{

    private readonly IDepartmentRepository _departmentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepo;
    public DepartmentService(IDepartmentRepository department, 
        IUnitOfWork unitOfWork, 
        IUserRepository userRepo)
    {
        _departmentRepository = department;
        _unitOfWork = unitOfWork;
        _userRepo = userRepo;
    }


    public async Task<bool> CreateDepartment(CreateDepartmentRequest departmentRequest)
    {
        var department = new Department(departmentRequest.Name, departmentRequest.qacoordinatorId);


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
                IsDelete = _.IsDeleted,
            }).Where(x => !x.IsDelete)
            .ToListAsync();
    }

    public async Task<List<SelectCoordinatorForCreateDepartmentResponse>> GetCoordinatorForCreateDepartmentAsync()
    {
        return await _userRepo.GetAllQuery()
            .Select(c => new SelectCoordinatorForCreateDepartmentResponse()
            {
                Id = c.Id,
                Name = c.UserName
            })
            .AsNoTracking()
            .ToListAsync();
    }


    public async Task<bool> DeleteDepartment(int id)
    {
        var department = await _departmentRepository.GetId(id).FirstOrDefaultAsync();
        if (department == null)
            return false;

        department.SoftDeleteDepartment();
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}



