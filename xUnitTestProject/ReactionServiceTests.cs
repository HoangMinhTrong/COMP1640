using COMP1640.Services;
using COMP1640.ViewModels.Reaction.Requests;
using Domain;
using Domain.Interfaces;
using Moq;
using NToastNotify;
using Utilities.Identity.Interfaces;

namespace ServiceTests;

public class ReactionServiceTests
{

    [Fact]
    public async Task HandleReaction_ReturnsTrueWhenSuccessful()
    {
        // Arrange


        var mockReactionRepository = new Mock<IReactionRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockCurrentUserInfo = new Mock<ICurrentUserInfo>();

        var mockServiceProvider = new Mock<IServiceProvider>();
        var mockToastNotification = new Mock<IToastNotification>();

        var ReactionService = new ReactionService(
            mockReactionRepository.Object,
            mockUnitOfWork.Object,
            mockCurrentUserInfo.Object);


        var reactionRequest = new ReactRequest { IdeaId = 1, ReactionStatusEnum = ReactionStatusEnum.Like };

        // Act
        var result = await ReactionService.HandleReactionAsync(reactionRequest);

        // Assert
        mockReactionRepository.Verify(r => r.InsertAsync(It.IsAny<Reaction>()), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}