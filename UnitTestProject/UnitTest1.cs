using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using COMP1640.Services;

namespace UnitTestProject
{
    [TestClass]
    public class DepartmentServiceTests
    {

        private DepartmentService _departmentService;
        private Mock<IDepartmentRepository> _departmentRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _departmentRepositoryMock = new Mock<IDepartmentRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _departmentService = new DepartmentService(_departmentRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [TestMethod]
        public async Task CreateDepartment_WithValidRequest_ShouldReturnTrue()
        {
            // Arrange
            var request = new CreateDepartmentRequest
            {
                Name = "Test Department",
                QacoordinatorId = 1
            };
            _departmentRepositoryMock
                .Setup(x => x.AnyAsync(It.IsAny<Expression<Func<Department, bool>>>()))
                .ReturnsAsync(false);
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _departmentService.CreateDepartment(request);

            // Assert
            Assert.IsTrue(result);
            _departmentRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<Department>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        public async Task CreateDepartment_WithExistingName_ShouldReturnFalse()
        {
            // Arrange
            var request = new CreateDepartmentRequest
            {
                Name = "Test Department",
                QacoordinatorId = 1
            };
            _departmentRepositoryMock
                .Setup(x => x.AnyAsync(It.IsAny<Expression<Func<Department, bool>>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _departmentService.CreateDepartment(request);

            // Assert
            Assert.IsFalse(result);
            _departmentRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<Department>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(), Times.Never);
        }
    }

}
