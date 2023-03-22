using System.Linq.Expressions;
using COMP1640.Services;
using COMP1640.ViewModels.Department.Requests;
using COMP1640.ViewModels.Department.Responses;
using Domain;
using Domain.Interfaces;
using Moq;

namespace ServiceTests;

public class DepartmentServiceTests
{

    [Fact]
    public async Task CreateDepartment_ReturnsTrueWhenSuccessful()
    {
        // Arrange
        var mockDepartmentRepository = new Mock<IDepartmentRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockUserRepository = new Mock<IUserRepository>();

        var departmentService = new DepartmentService(mockDepartmentRepository.Object, mockUnitOfWork.Object, mockUserRepository.Object);

        var departmentRequest = new CreateDepartmentRequest { Name = "TestDepartment" };

        // Act
        var result = await departmentService.CreateDepartment(departmentRequest);

        // Assert
        mockDepartmentRepository.Verify(r => r.InsertAsync(It.IsAny<Department>()), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }


    [Fact]
    public async Task CreateDepartment_WhenDepartmentDoesNotExist_ReturnsTrue()
    {
        // Arrange
        var departmentName = "Test Department";
        var qaCoordinatorId = 1;
        var departmentRequest = new CreateDepartmentRequest
        {
            Name = departmentName,
            QacoordinatorId = qaCoordinatorId
        };

        var mockUserRepository = new Mock<IUserRepository>();
        var mockDepartmentRepository = new Mock<IDepartmentRepository>();
        mockDepartmentRepository
            .Setup(r => r.AnyAsync(It.IsAny<Expression<Func<Department, bool>>>()))
            .ReturnsAsync(false);
        mockDepartmentRepository
            .Setup(r => r.InsertAsync(It.IsAny<Department>()))
            .Returns(Task.CompletedTask);

        var mockUnitOfWork = new Mock<IUnitOfWork>();

        var departmentService = new DepartmentService(mockDepartmentRepository.Object, mockUnitOfWork.Object, mockUserRepository.Object);

        // Act
        var result = await departmentService.CreateDepartment(departmentRequest);

        // Assert
        Assert.True(result);

        mockDepartmentRepository.Verify(r => r.AnyAsync(
            It.Is<Expression<Func<Department, bool>>>(expr =>
                expr.Compile().Invoke(new Department { Name = departmentName })
            )
        ));
        mockDepartmentRepository.Verify(r => r.InsertAsync(
            It.Is<Department>(d =>
                d.Name == departmentName && d.QaCoordinatorId == qaCoordinatorId
            )
        ));
        mockUnitOfWork.Verify(u => u.SaveChangesAsync());
    }


    [Fact]
    public async Task GetListDepartment_ReturnsCorrectDepartments()
    {
        // Arrange
        var departments = new List<Department>
        {
            new Department { Id = 1, Name = "Department 1", QaCoordinator = new User { Email = "coordinator1@example.com" }, TenantId = 1, IsDeleted = false },
            new Department { Id = 2, Name = "Department 2", QaCoordinator = new User { Email = "coordinator2@example.com" }, TenantId = 1, IsDeleted = false },
            new Department { Id = 3, Name = "Department 3", QaCoordinator = new User { Email = "coordinator3@example.com" }, TenantId = 2, IsDeleted = false },
            new Department { Id = 4, Name = "Department 4", QaCoordinator = new User { Email = "coordinator4@example.com" }, TenantId = 2, IsDeleted = true }
        };

        var mockDepartmentRepository = new Mock<IDepartmentRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockUserRepository = new Mock<IUserRepository>();
        mockDepartmentRepository.Setup(r => r.GetQuery(It.IsAny<Expression<Func<Department, bool>>>()))
            .Returns(departments.AsQueryable());

        var service = new DepartmentService(mockDepartmentRepository.Object, mockUnitOfWork.Object, mockUserRepository.Object);

        // Act
        var result = await service.GetListDepartment(new GetListDepartmentRequest());

        // Assert
        var expectedDepartments = departments
            .Where(d => !d.IsDeleted)
            .Select(d => new InforDepartmentResponse
            {
                Id = d.Id,
                Name = d.Name,
                QaCoordinatorName = d.QaCoordinator.Email,
                TenantId = d.TenantId,
                IsDelete = d.IsDeleted,
            })
            .ToList();

        Assert.Equal(expectedDepartments, result);
    }

    [Fact]
    public async Task GetCoordinatorForCreateDepartmentAsync_ReturnsCorrectResponse()
    {
        // Arrange
        var existingCoordinatorIds = new List<string> { "coordinator1", "coordinator2" };
        var expectedResponse = new List<SelectCoordinatorForCreateDepartmentResponse>
        {
            new SelectCoordinatorForCreateDepartmentResponse { Id = 1, Name = "Coordinator 3", IsEnable = true },
            new SelectCoordinatorForCreateDepartmentResponse { Id = 2, Name = "Coordinator 4", IsEnable = false },
            new SelectCoordinatorForCreateDepartmentResponse { Id = 3, Name = "Coordinator 5", IsEnable = true }
        };
        var mockDepartmentRepository = new Mock<IDepartmentRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockUserRepository = new Mock<IUserRepository>();
        mockDepartmentRepository.Setup(r => r.GetQuery(It.IsAny<Expression<Func<Department, bool>>>()))
            .Returns(new List<Department>
            {
                new Department { Id = 1, Name = "Department 1", QaCoordinatorId = 1 },
                new Department { Id = 2, Name = "Department 2", QaCoordinatorId = 2 },
            }.AsQueryable());

   
        mockUserRepository.Setup(r => r.GetAllQuery())
            .Returns(new List<User>
            {
                new User { Id = 1, UserName = "Coordinator 1" },
                new User { Id = 2, UserName = "Coordinator 2" },
  
            }.AsQueryable());

        var service = new DepartmentService(mockDepartmentRepository.Object, mockUnitOfWork.Object, mockUserRepository.Object);

        // Act
        var result = await service.GetCoordinatorForCreateDepartmentAsync();

        // Assert
        Assert.Equal(expectedResponse.Count, result.Count);
        for (int i = 0; i < expectedResponse.Count; i++)
        {
            Assert.Equal(expectedResponse[i].Id, result[i].Id);
            Assert.Equal(expectedResponse[i].Name, result[i].Name);
            Assert.Equal(expectedResponse[i].IsEnable, result[i].IsEnable);
        }
    }
}