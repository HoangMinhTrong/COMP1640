using Amazon.S3.Model;
using COMP1640.Services;
using COMP1640.ViewModels.AcademicYear.Request;
using COMP1640.ViewModels.Category.Requests;
using COMP1640.ViewModels.Category.Responses;
using COMP1640.ViewModels.Comment.Requests;
using COMP1640.ViewModels.Comment.Responses;
using COMP1640.ViewModels.Department.Requests;
using COMP1640.ViewModels.Department.Responses;
using COMP1640.ViewModels.HRM.Requests;
using COMP1640.ViewModels.Idea.Requests;
using COMP1640.ViewModels.Reaction.Requests;
using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using NToastNotify;
using System.Linq.Expressions;
using Utilities.Identity.Interfaces;

namespace ServiceTests
{
    public class CategoryServiceTests
    {

        [Fact]
        public async Task CreateCategory_ReturnsTrueWhenSuccessful()
        {
            // Arrange
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockIdeaRepository = new Mock<IIdeaRepository>();
            var toastT = new Mock<IToastNotification>();

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockUnitOfWork.Object, mockIdeaRepository.Object, toastT.Object);

            var categoryRequest = new CreateCategoryRequest { Name = "TestCategory" };

            // Act
            var result = await categoryService.CreateCategory(categoryRequest);

            // Assert
            mockCategoryRepository.Verify(r => r.InsertAsync(It.IsAny<Category>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetListCategory_ReturnsTrueWhenSuccessful()
        {
            // Arrange
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockIdeaRepository = new Mock<IIdeaRepository>();
            var toastT = new Mock<IToastNotification>();

            var categoryService = new CategoryService(mockCategoryRepository.Object, mockUnitOfWork.Object, mockIdeaRepository.Object, toastT.Object);

            var GetListCategoryRequest = new GetListCategoryRequest { SearchTerm = "TestCategory" };

            // Act
            var result = await categoryService.GetListCategory(GetListCategoryRequest);

            // Assert
            mockCategoryRepository.Verify(r => r.InsertAsync(It.IsAny<Category>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }


        [Fact]
        public async Task GetListCategory_ReturnsExpectedResults()
        {
            // Arrange
            var expectedResults = new List<InforCategoryResponse>
    {
        new InforCategoryResponse { Id = 1},
        new InforCategoryResponse { Id = 2},
        new InforCategoryResponse { Id = 3}
    };

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockIdeaRepository = new Mock<IIdeaRepository>();
            var toastT = new Mock<IToastNotification>();

            mockCategoryRepository.Setup(r => r.GetQuery(It.IsAny<Expression<Func<Category, bool>>>()))
        .Returns((Expression<Func<Category, bool>> filter) =>
        {
            var categories = expectedResults.Select(r => new Category
            {
                Id = r.Id,
            });
            return categories;
        });

            var service = new CategoryService(mockCategoryRepository.Object, mockUnitOfWork.Object, mockIdeaRepository.Object, toastT.Object);

            var request = new GetListCategoryRequest();

            // Act
            var results = await service.GetListCategory(request);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(expectedResults.Count, results.Count);
            for (int i = 0; i < expectedResults.Count; i++)
            {
                Assert.Equal(expectedResults[i].Id, results[i].Id);
                Assert.Equal(expectedResults[i].Name, results[i].Name);
                Assert.Equal(expectedResults[i].TenantId, results[i].TenantId);
                Assert.Equal(expectedResults[i].IsDelete, results[i].IsDelete);
            }
        }


        [Fact]
        public async Task GetCategoryPicklistAsync_ReturnsCorrectPicklist()
        {
            // Arrange
            var categories = new List<Category>()
                {
                    new Category { Id = 1, Name = "Category 1", IsDeleted = false },
                    new Category { Id = 2, Name = "Category 2", IsDeleted = false },
                    new Category { Id = 3, Name = "Category 3", IsDeleted = false }
                };
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockIdeaRepository = new Mock<IIdeaRepository>();
            var toastT = new Mock<IToastNotification>();
            _ = mockCategoryRepository.Setup(r => r.GetQuery(c => !c.IsDeleted)).Returns(categories.AsQueryable());
            var categoryService = new CategoryService(
                mockCategoryRepository.Object,
                mockUnitOfWork.Object,
                mockIdeaRepository.Object,
                toastT.Object);

            // Act
            var result = await categoryService.GetCategoryPicklistAsync();

            // Assert
            var expected = new List<SelectListItem>()
    {
        new SelectListItem { Text = "Category 1", Value = "1" },
        new SelectListItem { Text = "Category 2", Value = "2" },
        new SelectListItem { Text = "Category 3", Value = "3" }
    };
            Assert.Equal(expected.Count, result.Count());
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Text, result.ElementAt(i).Text);
                Assert.Equal(expected[i].Value, result.ElementAt(i).Value);
            }
        }

    }

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


    public class AcademicServiceTests
    {

        [Fact]
        public async Task CreateAcademic_ReturnsTrueWhenSuccessful()
        {
            // Arrange


            var mockIdeaRepository = new Mock<IIdeaRepository>();
            var mockAcademicYearRepository = new Mock<IAcademicYearRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockAttachmentService = new Mock<AttachmentService>();

            var academicService = new AcademicYearService(
                mockAcademicYearRepository.Object,
                mockUnitOfWork.Object,
                mockIdeaRepository.Object,
                mockAttachmentService.Object);


            var AcademicRequest = new UpsertAcademicYearRequest { Name = "TestAcademic" };

            // Act
            var result = await academicService.CreateAcademicYearAsync(AcademicRequest);

            // Assert
            mockAcademicYearRepository.Verify(r => r.InsertAsync(It.IsAny<AcademicYear>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }



    }


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
