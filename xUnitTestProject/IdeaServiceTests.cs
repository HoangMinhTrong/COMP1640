using COMP1640.Services;
using COMP1640.ViewModels.Idea.Requests;
using Domain;
using Domain.Interfaces;
using Moq;
using Utilities.Identity.Interfaces;

namespace ServiceTests;

public class IdeaServiceTests
{

    [Fact]
    public async Task CreateIdea_ReturnsTrueWhenSuccessful()
    {
        // Arrange


        var mockIdeaRepository = new Mock<IIdeaRepository>();
        var mockIdeaHistoryRepository = new Mock<IIdeaHistoryRepository>();
        var mockCategoryRepository = new Mock<ICategoryRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockCurrentUserInfo = new Mock<ICurrentUserInfo>();
        var mockServiceProvider = new Mock<IServiceProvider>();
        var mockAcademicYearRepository = new Mock<IAcademicYearRepository>();
        var mockAttachmentService = new Mock<AttachmentService>();


        var ideaService = new IdeaService(
            mockIdeaRepository.Object,
            mockUnitOfWork.Object,
            mockCategoryRepository.Object,
            mockCurrentUserInfo.Object,
            mockServiceProvider.Object,
            mockAttachmentService.Object,
            mockIdeaHistoryRepository.Object,
            mockAcademicYearRepository.Object);


        var IdeaRequest = new CreateIdeaRequest { Title = "TestIdea" };

        // Act
        var result = await ideaService.CreateIdeaAsync(IdeaRequest);

        // Assert
        mockIdeaRepository.Verify(r => r.InsertAsync(It.IsAny<Idea>()), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

}