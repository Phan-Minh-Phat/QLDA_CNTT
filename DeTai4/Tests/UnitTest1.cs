using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DeTai4.Services.Interfaces;
using DeTai4.Pages.Customer;
using DeTai4.Reponsitories.Repositories.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using DeTai4.Services;

namespace Tests
{
    [TestClass]
    public class SubmitProjectRequestModelTests
    {
        private Mock<ICustomerService> _mockCustomerService;
        private Mock<IProjectService> _mockProjectService;
        private Mock<IDesignService> _mockDesignService;
        private SubmitProjectRequestModel _submitProjectRequestModel;

        [TestInitialize]
        public void Setup()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _mockProjectService = new Mock<IProjectService>();
            _mockDesignService = new Mock<IDesignService>();

            _submitProjectRequestModel = new SubmitProjectRequestModel(
                _mockCustomerService.Object,
                _mockProjectService.Object,
                _mockDesignService.Object
            );

            // Mock HttpContext, User, and TempData
            var httpContext = new DefaultHttpContext();
            var tempData = new Microsoft.AspNetCore.Mvc.ViewFeatures.TempDataDictionary(httpContext, Mock.Of<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider>());
            _submitProjectRequestModel.TempData = tempData;

            // Set up User Claims
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("UserId", "1") }));
            _submitProjectRequestModel.PageContext = new Microsoft.AspNetCore.Mvc.RazorPages.PageContext
            {
                HttpContext = httpContext
            };
        }

        [TestMethod]
        public async Task OnGetAsync_ShouldLoadDesigns()
        {
            // Arrange
            var designs = new List<Design>
            {
                new Design { DesignId = 1, DesignName = "Design 1" },
                new Design { DesignId = 2, DesignName = "Design 2" }
            };
            _mockDesignService.Setup(service => service.GetAllDesignsAsync()).ReturnsAsync(designs);

            // Act
            await _submitProjectRequestModel.OnGetAsync();

            // Assert
            Assert.AreEqual(2, _submitProjectRequestModel.Designs.Count);
        }

        [TestMethod]
        public async Task OnPostAsync_ShouldReturnError_WhenUserIdCannotBeDetermined()
        {
            // Arrange
            _submitProjectRequestModel.HttpContext.User = new ClaimsPrincipal(); // No UserId in claims
            _submitProjectRequestModel.RequestDetails = "Some project details";
            _submitProjectRequestModel.DesignId = 1;

            // Act
            var result = await _submitProjectRequestModel.OnPostAsync();

            // Assert
            Assert.IsTrue(_submitProjectRequestModel.ModelState.ContainsKey(""));
            Assert.AreEqual("Không thể xác định UserId.", _submitProjectRequestModel.ModelState[""].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public async Task OnPostAsync_ShouldReturnError_WhenCustomerNotFound()
        {
            // Arrange
            int userId = 1;
            _submitProjectRequestModel.RequestDetails = "Some project details";
            _submitProjectRequestModel.DesignId = 1;

            // Mock UserId retrieval
            _submitProjectRequestModel.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("UserId", userId.ToString()) }));

            _mockCustomerService.Setup(service => service.GetCustomerByUserIdAsync(userId)).ReturnsAsync((Customer)null);

            // Act
            var result = await _submitProjectRequestModel.OnPostAsync();

            // Assert
            Assert.IsTrue(_submitProjectRequestModel.ModelState.ContainsKey(""));
            Assert.AreEqual("Không tìm thấy thông tin khách hàng.", _submitProjectRequestModel.ModelState[""].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public async Task OnPostAsync_ShouldReturnError_WhenRequestDetailsOrDesignIdInvalid()
        {
            // Arrange
            int userId = 1;
            _submitProjectRequestModel.RequestDetails = ""; // Invalid request details
            _submitProjectRequestModel.DesignId = 0; // Invalid DesignId

            // Mock UserId retrieval
            _submitProjectRequestModel.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("UserId", userId.ToString()) }));

            var customer = new Customer { CustomerId = userId };
            _mockCustomerService.Setup(service => service.GetCustomerByUserIdAsync(userId)).ReturnsAsync(customer);

            // Act
            var result = await _submitProjectRequestModel.OnPostAsync();

            // Assert
            Assert.IsTrue(_submitProjectRequestModel.ModelState.ContainsKey(""));
            Assert.AreEqual("Thông tin yêu cầu không hợp lệ.", _submitProjectRequestModel.ModelState[""].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public async Task OnPostAsync_ShouldCreateProject_WhenRequestDetailsAndDesignIdValid()
        {
            // Arrange
            int userId = 1;
            _submitProjectRequestModel.RequestDetails = "Valid project details";
            _submitProjectRequestModel.DesignId = 1;

            // Mock UserId retrieval
            _submitProjectRequestModel.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("UserId", userId.ToString()) }));

            var customer = new Customer { CustomerId = userId };
            _mockCustomerService.Setup(service => service.GetCustomerByUserIdAsync(userId)).ReturnsAsync(customer);

            // Act
            var result = await _submitProjectRequestModel.OnPostAsync();

            // Assert
            _mockProjectService.Verify(service => service.CreateProjectAsync(It.IsAny<Project>()), Times.Once);
            Assert.IsTrue(_submitProjectRequestModel.IsRequestSubmitted);
            Assert.AreEqual("Quý khách đã gửi yêu cầu thi công thành công!", _submitProjectRequestModel.TempData["SuccessMessage"]);
        }
    }
}
