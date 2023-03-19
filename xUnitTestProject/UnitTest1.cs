using System.Threading.Tasks;
using COMP1640.Services;
using COMP1640.ViewModels.Category.Requests;
using COMP1640.ViewModels.Department.Requests;
using COMP1640.ViewModels.Idea.Requests;
using COMP1640.ViewModels.AcademicYear.Request;
using COMP1640.ViewModels.Reaction.Requests;
using COMP1640.ViewModels.HRM.Requests;
using Domain;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Moq;
using NToastNotify;
using Utilities.Identity.Interfaces;
using Xunit;
using Utilities.StorageService;
using COMP1640.ViewModels.Attachment.Responses;
using COMP1640.ViewModels.Comment.Requests;

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
    }

}
