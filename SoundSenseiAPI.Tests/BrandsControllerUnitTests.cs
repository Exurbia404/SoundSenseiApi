using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Controllers.Tests
{
    public class BrandsControllerUnitTests
    {
        private DbContextOptions<SoundSenseiContext> GetInMemoryDatabaseOptions()
        {
            return new DbContextOptionsBuilder<SoundSenseiContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task GetBrand_ReturnsBrandWithMatchingId()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions();
            using (var context = new SoundSenseiContext(options))
            {
                var existingBrand = new Brand { Id = 1, Name = "Existing Brand", BrandImageLink = "existing-image.jpg" };
                context.Brands.Add(existingBrand);
                context.SaveChanges();
            }

            using (var context = new SoundSenseiContext(options))
            {
                var controller = new BrandsController(context);

                // Act
                var result = controller.GetBrand(1);

                // Assert
                var brand = Assert.IsType<Brand>(result);
                Assert.Equal(1, brand.Id);
                // Add additional assertions as needed
            }
        }

        [Fact]
        public async Task PutBrand_ReturnsNoContentResult()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions();
            using (var context = new SoundSenseiContext(options))
            {
                var brand = new Brand { Id = 2, Name = "Brand 1", BrandImageLink = "image1.jpg" };
                context.Brands.Add(brand);
                context.SaveChanges();
            }

            using (var context = new SoundSenseiContext(options))
            {
                var controller = new BrandsController(context);
                var updatedBrand = new Brand { Id = 2, Name = "Updated Brand", BrandImageLink = "updated-image.jpg" };

                // Act
                var result = await controller.PutBrand(2, updatedBrand);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }
        }

        [Fact]
        public async Task PostBrand_ReturnsCreatedResponseWithBrand()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions();
            using (var context = new SoundSenseiContext(options))
            {
                context.Database.EnsureCreated();
            }

            using (var context = new SoundSenseiContext(options))
            {
                var controller = new BrandsController(context);
                var newBrand = new Brand { Id = 3, Name = "New Brand", BrandImageLink = "new-image.jpg" };

                // Act
                var result = await controller.PostBrand(newBrand);

                // Assert
                Assert.IsType<CreatedAtActionResult>(result.Result);
                //var brand = Assert.IsType<Brand>(createdResponse.Value);
                //Assert.Equal(1, brand.Id);
            }
        }

        [Fact]
        public async Task DeleteBrand_ReturnsNoContentResult()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions();
            using (var context = new SoundSenseiContext(options))
            {
                var brand = new Brand { Id = 4, Name = "Brand 1", BrandImageLink = "image1.jpg" };
                context.Brands.Add(brand);
                context.SaveChanges();
            }

            using (var context = new SoundSenseiContext(options))
            {
                var controller = new BrandsController(context);

                // Act
                var result = await controller.DeleteBrand(4);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }
        }

    }
}