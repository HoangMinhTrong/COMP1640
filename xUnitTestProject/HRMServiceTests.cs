using COMP1640.Services;
using COMP1640.ViewModels.HRM.Requests;
using Domain;
using Domain.Interfaces;
using Moq;
using NToastNotify;
using Utilities.Identity.Interfaces;

namespace ServiceTests;

public class HRMServiceTests
{
      

    [Fact]
    public async Task CreateUser_ReturnsTrueWhenSuccessful()
    {
        // Arrange

        var mockUserRepository = new Mock<IUserRepository>();
        var mockDepartmentRepository = new Mock<IDepartmentRepository>();
        var mockRoleRepository = new Mock<IRoleRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockCurrentUserInfo = new Mock<ICurrentUserInfo>();
        var mockServiceProvider = new Mock<IServiceProvider>();
        var mockToastNotification = new Mock<IToastNotification>();

        mockUserRepository.Setup(r => r.FindByEmailAsync(It.IsAny<string>()));
        mockDepartmentRepository.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync(new Department());
        mockRoleRepository.Setup(r => r.GetAsync(It.IsAny<RoleTypeEnum>())).ReturnsAsync(new Role());


        var HRMService = new HRMService(
            mockUserRepository.Object,
            mockUnitOfWork.Object,
            mockDepartmentRepository.Object,
            mockRoleRepository.Object,
            mockCurrentUserInfo.Object,
            mockServiceProvider.Object,
            mockToastNotification.Object);


        var HRMRequest = new CreateUserRequest { Email = "TestHRM" };

        // Act
        var result = await HRMService.CreateUserAsync(HRMRequest);

        // Assert
        mockUserRepository.Verify(r => r.InsertAsync(It.IsAny<User>()), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteUser_ReturnsTrueWhenSuccessful()
    {


        // Arrange

        var mockUserRepository = new Mock<IUserRepository>();
        var mockDepartmentRepository = new Mock<IDepartmentRepository>();
        var mockRoleRepository = new Mock<IRoleRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockCurrentUserInfo = new Mock<ICurrentUserInfo>();
        var mockServiceProvider = new Mock<IServiceProvider>();
        var mockToastNotification = new Mock<IToastNotification>();
        int userId = 1;
        mockUserRepository.Setup(r => r.FindByEmailAsync(It.IsAny<string>()));
        var service = new HRMService(
            mockUserRepository.Object,
            mockUnitOfWork.Object,
            mockDepartmentRepository.Object,
            mockRoleRepository.Object,
            mockCurrentUserInfo.Object,
            mockServiceProvider.Object,
            mockToastNotification.Object);

        // Act
        var result = await service.DeleteUserAsync(userId);

        // Assert
        mockUserRepository.Verify(r => r.DeleteAsync(It.IsAny<User>()), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        Assert.True(result);
    }

    [Fact]
    public async Task EditUserInfoAsync_ReturnsFalse_WhenUserNotFound()
    {
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();
        var mockDepartmentRepository = new Mock<IDepartmentRepository>();
        var mockRoleRepository = new Mock<IRoleRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockCurrentUserInfo = new Mock<ICurrentUserInfo>();
        var mockServiceProvider = new Mock<IServiceProvider>();
        var mockToastNotification = new Mock<IToastNotification>();
        int id = 1;

        var HRMService = new HRMService(
            mockUserRepository.Object,
            mockUnitOfWork.Object,
            mockDepartmentRepository.Object,
            mockRoleRepository.Object,
            mockCurrentUserInfo.Object,
            mockServiceProvider.Object,
            mockToastNotification.Object);
        EditUserRequest request = new EditUserRequest
        {
            Email = "new-email@example.com",
            RoleId = 2,
            DepartmentId = 3,
            Gender = 1,
        };
        mockUserRepository.Setup(x => x.GetById(id));

        // Act
        bool result = await HRMService.EditUserInfoAsync(id, request);

        // Assert
        Assert.False(result);
    }

}