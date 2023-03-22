using Amazon.S3.Model;
using COMP1640.Services;
using COMP1640.ViewModels.Comment.Requests;
using COMP1640.ViewModels.Comment.Responses;
using Domain;
using Domain.Interfaces;
using Moq;
using System.Linq.Expressions;

namespace ServiceTests
{
    public class CommentServiceTests
    {

        [Fact]
        public async Task CreateComment_ReturnsTrueWhenSuccessful()
        {
            // Arrange


            var mockServiceProvider = new Mock<IServiceProvider>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();



            var CommentService = new CommentService(
                mockCommentRepository.Object,
                mockUnitOfWork.Object,
                mockServiceProvider.Object);


            var commentRequest = new CommentIdeaRequest { Content = "abc" };

            // Act
            var result = await CommentService.CommentIdea(commentRequest);

            // Assert
            mockCommentRepository.Verify(r => r.InsertAsync(It.IsAny<Comment>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CommentList_ReturnsCorrectComments()
        {
            // Arrange

            var mockServiceProvider = new Mock<IServiceProvider>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var ideaId = 1;
            var expectedComments = new List<CommentInfoResponse>
            {
                new CommentInfoResponse
                {
                    Id = 1,
                    Content = "Comment 1",
                    IdeaId = ideaId,
                    Author = new CommentAuthor
                    {
                        Id = 1,
                        Name = "author1@example.com",
                    },
                    IsAnonymous = false,
                },
                new CommentInfoResponse
                {
                    Id = 2,
                    Content = "Comment 2",
                    IdeaId = ideaId,
                    Author = new CommentAuthor
                    {
                        Id = 2,
                        Name = "author2@example.com",
                    },
                    IsAnonymous = true,
                },
            };
         
            mockCommentRepository.Setup(r => r.GetQuery(It.IsAny<Expression<Func<Comment, bool>>>()))
                .Returns(new List<Comment>
                {
            new Comment
            {
                Id = 1,
                Content = "Comment 1",
                IdeaId = ideaId,
                CreatedBy = 1,
                IsAnonymous = false,
                CreatedByNavigation = new User
                {
                    Id = 1,
                    Email = "author1@example.com",
                },
            },
            new Comment
            {
                Id = 2,
                Content = "Comment 2",
                IdeaId = ideaId,
                CreatedBy = 1,
                IsAnonymous = true,
            },
                }.AsQueryable());

            var CommentService = new CommentService(
                mockCommentRepository.Object,
                mockUnitOfWork.Object,
                mockServiceProvider.Object);


            // Act
            var result = await CommentService.CommentList(ideaId);

            // Assert
            Assert.Equal(expectedComments.Count, result.Count);
            for (var i = 0; i < expectedComments.Count; i++)
            {
                var expectedComment = expectedComments[i];
                var actualComment = result[i];
                Assert.Equal(expectedComment.Id, actualComment.Id);
                Assert.Equal(expectedComment.Content, actualComment.Content);
                Assert.Equal(expectedComment.IdeaId, actualComment.IdeaId);
                Assert.Equal(expectedComment.IsAnonymous, actualComment.IsAnonymous);
                if (expectedComment.Author == null)
                {
                    Assert.Null(actualComment.Author);
                }
                else
                {
                    Assert.Equal(expectedComment.Author.Id, actualComment.Author.Id);
                    Assert.Equal(expectedComment.Author.Name, actualComment.Author.Name);
                }
            }
        }

    
    }

}
