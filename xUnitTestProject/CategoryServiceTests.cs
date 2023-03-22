using System.Linq.Expressions;
using COMP1640.Services;
using COMP1640.ViewModels.Category.Requests;
using COMP1640.ViewModels.Category.Responses;
using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using NToastNotify;

namespace ServiceTests;

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