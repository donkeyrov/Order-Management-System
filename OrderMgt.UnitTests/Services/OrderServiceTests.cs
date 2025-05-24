using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using OrderMgt.API.Interfaces;
using OrderMgt.API.Services;
using OrderMgt.Model.Entities;
using OrderMgt.Model.Models;
using Xunit;

namespace OrderMgt.UnitTests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IOrderHistoryRepository> _orderHistoryRepositoryMock;
        private readonly Mock<IPromotionRepository> _promotionRepositoryMock;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<ILogger<OrderService>> _loggerMock;
        private readonly OrderService _orderService;


        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderHistoryRepositoryMock = new Mock<IOrderHistoryRepository>();
            _promotionRepositoryMock = new Mock<IPromotionRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _loggerMock = new Mock<ILogger<OrderService>>();

            _orderService = new OrderService(
                _orderHistoryRepositoryMock.Object,
                _promotionRepositoryMock.Object,
                _orderRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task ProcessOrder_OrderNotFound_ReturnsFailure()
        {
            _orderRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((Order)null);

            var result = await _orderService.ProcessOrder(1);

            result.Success.Should().BeFalse();
            result.ErrorMessage.Should().Be("Order not found!");
        }

        [Fact]
        public async Task ProcessOrder_OrderFound_AppliesDiscountAndUpdatesOrder()
        {
            var order = new Order { OrderID = 1, CustomerID = 2, Total = 100 };
            _orderRepositoryMock.Setup(r => r.GetAsync(1)).ReturnsAsync(order);
            _orderRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Order>())).Returns(Task.FromResult(true));
            _orderService
                .GetType()
                .GetMethod("GenerateDiscount")
                .Invoke(_orderService, new object[] { order });

            // Mock GenerateDiscount to return success and updated order
            var discountOrder = new Order { OrderID = 1, CustomerID = 2, Total = 90, Discount = 10 };
            var discountResponse = new BaseResponseModel { Success = true, Data = discountOrder };
            var orderServiceMock = new Mock<OrderService>(
                _orderHistoryRepositoryMock.Object,
                _promotionRepositoryMock.Object,
                _orderRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _loggerMock.Object
            )
            { CallBase = true };
            orderServiceMock.Setup(s => s.GenerateDiscount(order)).ReturnsAsync(discountResponse);

            var result = await orderServiceMock.Object.ProcessOrder(1);

            result.Success.Should().BeTrue();
            ((Order)result.Data).Total.Should().Be(90);
        }

        [Fact]
        public async Task CompleteOrder_OrderNotFound_ReturnsFailure()
        {
            _orderRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((Order)null);

            var result = await _orderService.CompleteOrder(1, "user1");

            result.Success.Should().BeFalse();
            result.ErrorMessage.Should().Be("Order not found!");
        }

        [Fact]
        public async Task CompleteOrder_OrderFound_CompletesOrderAndRemovesIt()
        {
            var order = new Order { OrderID = 1, CustomerID = 2, OrderStatus = "Pending", CreatedBy = "user1", DateCreated = DateTime.Now, TransactionDate = DateTime.Now, Details = "details", Discount = 0, OrderNo = "ORD001", Total = 100 };
            _orderRepositoryMock.Setup(r => r.GetAsync(1)).ReturnsAsync(order);
            _orderHistoryRepositoryMock.Setup(r => r.AddAsync(It.IsAny<OrderHistory>())).Returns(Task.FromResult(true));
            _orderRepositoryMock.Setup(r => r.RemoveAsync(order)).Returns(Task.FromResult(true));

            var result = await _orderService.CompleteOrder(1, "user2");

            result.Success.Should().BeTrue();
            _orderHistoryRepositoryMock.Verify(r => r.AddAsync(It.IsAny<OrderHistory>()), Times.Once);
            _orderRepositoryMock.Verify(r => r.RemoveAsync(order), Times.Once);
        }

        [Fact]
        public async Task GenerateDiscount_CustomerNotFound_ReturnsFailure()
        {
            var order = new Order { CustomerID = 1 };
            _customerRepositoryMock.Setup(r => r.GetAsync(1)).ReturnsAsync((Customer)null);

            var result = await _orderService.GenerateDiscount(order);

            result.Success.Should().BeFalse();
            result.ErrorMessage.Should().Be("No customer found in the order to apply discount");
        }

        
    }
}
