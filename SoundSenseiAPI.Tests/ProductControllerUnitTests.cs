using Backend.Controllers;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SoundSenseiAPI.Tests
{
    public class ProductsControllerUnitTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;

        public ProductsControllerUnitTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        private DbContextOptions<SoundSenseiContext> GetInMemoryDatabaseOptions()
        {
            return _fixture.Options;
        }

        [Fact]
        public async Task GetProducts_ReturnsListOfProducts()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions();
            using (var context = new SoundSenseiContext(options))
            {
                var products = new List<Product>
                {
                    new Product { Id = 1, Name = "Product 1", Description = "Description 1", brand_id = 1, ProductImageLink = "image1.jpg", ReleaseDate = DateTime.Now },
                    new Product { Id = 2, Name = "Product 2", Description = "Description 2", brand_id = 2, ProductImageLink = "image2.jpg", ReleaseDate = DateTime.Now }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            using (var context = new SoundSenseiContext(options))
            {
                var controller = new ProductsController(context);

                // Act
                var result = await controller.GetProducts();

                // Assert
                var products = Assert.IsType<List<Product>>(result.Value);
                Assert.Equal(6, products.Count);
                // Add additional assertions as needed
                context.Dispose();

            }
        }

        [Fact]
        public async Task GetProduct_ReturnsProductWithMatchingId()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions();
            using (var context = new SoundSenseiContext(options))
            {
                var products = new List<Product>
                {
                    new Product { Id = 3, Name = "Product 1", Description = "Description 1", brand_id = 1, ProductImageLink = "image1.jpg", ReleaseDate = DateTime.Now },
                    new Product { Id = 4, Name = "Product 2", Description = "Description 2", brand_id = 2, ProductImageLink = "image2.jpg", ReleaseDate = DateTime.Now }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }

            using (var context = new SoundSenseiContext(options))
            {
                var controller = new ProductsController(context);

                // Act
                var result = await controller.GetProduct(3);

                // Assert
                var product = Assert.IsType<Product>(result.Value);
                Assert.Equal(3, product.Id);
                // Add additional assertions as needed
                context.Dispose();

            }
        }

        [Fact]
        public async Task PutProduct_ReturnsNoContentResult()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions();
            using (var context = new SoundSenseiContext(options))
            {
                var product = new Product { Id = 5, Name = "Product 1", Description = "Description 1", brand_id = 1, ProductImageLink = "image1.jpg", ReleaseDate = DateTime.Now };
                context.Products.Add(product);
                context.SaveChanges();
            }

            using (var context = new SoundSenseiContext(options))
            {
                var controller = new ProductsController(context);
                var updatedProduct = new Product { Id = 5, Name = "Updated Product 1", Description = "Updated Description 1", brand_id = 1, ProductImageLink = "updated_image1.jpg", ReleaseDate = DateTime.Now };

                // Act
                var result = await controller.PutProduct(5, updatedProduct);

                // Assert
                Assert.IsType<NoContentResult>(result);
                // Add additional assertions as needed
                context.Dispose();

            }
        }

        [Fact]
        public async Task PostProduct_ReturnsCreatedResponseWithProduct()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions();
            using (var context = new SoundSenseiContext(options))
            {
                var controller = new ProductsController(context);
                var newProduct = new Product { Id = 6, Name = "New Product", Description = "New Description", brand_id = 1, ProductImageLink = "new_image.jpg", ReleaseDate = DateTime.Now };

                // Act
                var result = await controller.PostProduct(newProduct);

                // Assert
                var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
                var product = Assert.IsType<Product>(createdResult.Value);
                Assert.Equal(6, product.Id);
                // Add additional assertions as needed
                context.Dispose();

            }
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNoContentResult()
        {
            // Arrange
            var options = GetInMemoryDatabaseOptions();
            using (var context = new SoundSenseiContext(options))
            {
                var product = new Product { Id = 7, Name = "Product 1", Description = "Description 1", brand_id = 1, ProductImageLink = "image1.jpg", ReleaseDate = DateTime.Now };
                context.Products.Add(product);
                context.SaveChanges();
            }

            using (var context = new SoundSenseiContext(options))
            {
                var controller = new ProductsController(context);

                // Act
                var result = await controller.DeleteProduct(7);

                // Assert
                Assert.IsType<NoContentResult>(result);
                // Add additional assertions as needed
                context.Dispose();

            }
        }
    }
}
