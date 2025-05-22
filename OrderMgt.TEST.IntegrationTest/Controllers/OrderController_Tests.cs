using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using OrderMgt.Model.Entities;
using OrderMgt.Model.Models;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace OrderMgt.TEST.IntegrationTest.Controllers
{
    public class OrderController_Tests
    {
        [Fact]
        public async Task ProcessOrderAsync_UpdateOrderStatusToProcessed()
        {
            //Arrange
            var application = new OrderMgtSystemWebApplicationFactory();

            var client = application.CreateClient();

            //Act
            var response = await client.GetFromJsonAsync<BaseResponseModel>($"api/Order/ProcessOrderAsync/4");

            var order = (Order)response.Data;

            //Assert
            response.Success.Should().BeTrue();
            response.Data.Should().NotBeNull();
            order?.OrderStatus.Should().BeEquivalentTo("Processed");
        }
    }
}
