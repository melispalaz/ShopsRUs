using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopsRUs.Api.Controllers;
using ShopsRUs.Api.Models;
using Xunit;

namespace ShopsRUs.UnitTest
{
    public class DiscountControllerUnitTest
    {
        private readonly DiscountController _controller;
        private readonly ShopsRUsDbContext _context;
        private readonly DbContextOptions<ShopsRUsDbContext> _options;
        private readonly ILogger<DiscountController> _logger;

        public DiscountControllerUnitTest()
        {
            _options = new DbContextOptions<ShopsRUsDbContext>();
            _context = new ShopsRUsDbContext(_options);
            _controller = new DiscountController(_logger, _context);
        }

       

        [Fact]
        public void GetById_UnknownCustomerIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.GetCustomerDiscount(0,0);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetById_ExistingCustomerIdPassed_ReturnsOkResult()
        {
            // Arrange
            int customerId = 1;
            double bill = 150;


            // Act
            var discountedBill = _controller.GetCustomerDiscount(customerId, bill);

            // Assert
            Assert.IsType<double>(discountedBill as double?);
        }

    }
}