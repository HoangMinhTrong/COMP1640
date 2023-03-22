using COMP1640.Services;
using COMP1640.ViewModels.AcademicYear.Request;
using Domain;
using Domain.Interfaces;
using Moq;

namespace ServiceTests;

public class AcademicServiceTests
{

    [Fact]
    public async Task CreateAcademic_ReturnsTrueWhenSuccessful()
    {
        // Arrange


        var mockIdeaRepository = new Mock<IIdeaRepository>();
        var mockAcademicYearRepository = new Mock<IAcademicYearRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        // var mockAttachmentService = new Mock<AttachmentService>();

        var academicService = new AcademicYearService(
            mockAcademicYearRepository.Object,
            mockUnitOfWork.Object,
            mockIdeaRepository.Object,
            null);


        var AcademicRequest = new UpsertAcademicYearRequest { Name = "TestAcademic" };

        // Act
        var result = await academicService.CreateAcademicYearAsync(AcademicRequest);

        // Assert
        mockAcademicYearRepository.Verify(r => r.InsertAsync(It.IsAny<AcademicYear>()), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }



}