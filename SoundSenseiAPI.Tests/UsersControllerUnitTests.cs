using Backend.Controllers;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class UsersControllerUnitTests
{
    private readonly SoundSenseiContext _context;

    public UsersControllerUnitTests()
    {
        // Create an in-memory database and initialize the context
        var options = new DbContextOptionsBuilder<SoundSenseiContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new SoundSenseiContext(options);
    }

    [Fact]
    public void GetUsers_ReturnsOkResult()
    {
        // Arrange
        var controller = new UsersController(_context);

        // Act
        var result = controller.GetUsers();

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetUser_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var controller = new UsersController(_context);
        var user = new User { Id = 1, Username = "John", Password = "password", Email = "john@example.com", UserImageLink = "image.jpg" };
        _context.Users.Add(user);
        _context.SaveChanges();

        // Act
        var result = controller.GetUser(1);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetUser_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var controller = new UsersController(_context);

        // Act
        var result = controller.GetUser(2);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void CreateUser_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var controller = new UsersController(_context);
        var user = new User { Username = "John", Password = "password", Email = "john@example.com", UserImageLink = "image.jpg" };

        // Act
        var result = controller.CreateUser(user);

        // Assert
        Assert.IsType<CreatedAtActionResult>(result.Result);
    }

    [Fact]
    public void UpdateUser_WithInvalidId_ReturnsBadRequestResult()
    {
        // Arrange
        var controller = new UsersController(_context);
        var invalidId = 999;
        var user = new User { Id = 1, Username = "John", Password = "123456", Email = "john@example.com", UserImageLink = "image.jpg" };

        // Act
        var result = controller.UpdateUser(invalidId, user);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public void DeleteUser_WithValidId_ReturnsUser()
    {
        // Arrange
        var controller = new UsersController(_context);
        var user = new User { Id = 5, Username = "John", Password = "password", Email = "john@example.com", UserImageLink = "image.jpg" };
        _context.Users.Add(user);
        _context.SaveChanges();

        // Act
        var result = controller.DeleteUser(5);

        // Assert
        Assert.Equal(user, result.Value);
    }

    [Fact]
    public void DeleteUser_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var controller = new UsersController(_context);

        // Act
        var result = controller.DeleteUser(6);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    //Fix this
    [Fact]
    public void Authenticate_WithValidCredentials_ReturnsUserId()
    {
        // Arrange
        var controller = new UsersController(_context);
        var user = new User { Id = 7, Username = "John", Password = "password", Email = "john@example.com", UserImageLink = "image.jpg" };
        _context.Users.Add(user);
        _context.SaveChanges();

        // Act
        var result = controller.Authenticate("john@example.com", "password");

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void Authenticate_WithInvalidCredentials_ReturnsZero()
    {
        // Arrange
        var controller = new UsersController(_context);
        var user = new User { Id = 8, Username = "John", Password = "password", Email = "john@example.com", UserImageLink = "image.jpg" };
        _context.Users.Add(user);
        _context.SaveChanges();

        // Act
        var result = controller.Authenticate("john@example.com", "wrongpassword");

        // Assert
        Assert.Equal(0, result);
    }
}