using System;
using Xunit;
using AutoFixture;
using Moq;
using FluentAssertions;
using OrderMgt.API.Interfaces;
using OrderMgt.API.Controllers;
using Microsoft.Extensions.Logging;
using OrderMgt.Model.Entities;
using OrderMgt.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace OrderMgt.TEST.Controllers
{
    public class OrderController_Tests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<IOrderService> _orderService;
        private readonly Mock<ILogger<OrderController>> _logger;
        private readonly OrderController _orderController;

        public OrderController_Tests()
        {
            _fixture = new Fixture();
            _orderService = _fixture.Freeze<Mock<IOrderService>>();
            _orderRepository = _fixture.Freeze<Mock<IOrderRepository>>();
            _logger = _fixture.Freeze<Mock<ILogger<OrderController>>>();
            _orderController = new OrderController(_orderRepository.Object, _orderService.Object, _logger.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnOKReponse_WhenOrderIsAdded()
        {
            //Arrange
            var orderMock = _fixture.Create<Order>();
            
            //Act
            var result = await _orderController.AddAsync(orderMock);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseResponseModel>();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.Should().As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(orderMock.GetType());
        }

        [Fact]
        public async Task AddAsync_ShouldReturnNotFound_WhenOrderIsEmpty()
        {
            //Arrange
            var orderMock = new Order();

            //Act
            var result = await _orderController.AddAsync(orderMock);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BaseResponseModel>();
            result.Should().BeAssignableTo<NotFoundResult>();           
        }
    }
}
