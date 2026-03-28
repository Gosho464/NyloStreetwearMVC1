using Xunit;
using Microsoft.EntityFrameworkCore;
using NyloStreetwearMVC1.Controllers;
using NyloStreetwearMVC1.Data;
using NyloStreetwearMVC1.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.InMemory;

public class CategoriesControllerTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);

        var category = new Category { Id = 1, Name = "Men" };
        var product = new Product { Id = 1, Title = "Hoodie", Price = 50 };

        var pc = new ProductCategory
        {
            Category = category,
            Product = product
        };

        category.ProductCategories = new List<ProductCategory> { pc };

        context.Category.Add(category);
        context.Product.Add(product);
        context.ProductCategories.Add(pc);

        context.SaveChanges();

        return context;
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenIdIsNull()
    {
        var controller = new CategoriesController(GetDbContext());

        var result = await controller.Details(null);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsCategory_WhenValidId()
    {
        var controller = new CategoriesController(GetDbContext());

        var result = await controller.Details(1);

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Category>(viewResult.Model);

        Assert.Equal("Men", model.Name);
        Assert.Single(model.ProductCategories);
    }

    [Fact]
    public async Task ByCategory_ReturnsNotFound_WhenNameIsNull()
    {
        var controller = new CategoriesController(GetDbContext());

        var result = await controller.ByCategory(null);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task ByCategory_ReturnsCorrectCategory()
    {
        var controller = new CategoriesController(GetDbContext());

        var result = await controller.ByCategory("Men");

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Category>(viewResult.Model);

        Assert.Equal("Men", model.Name);
    }
}
