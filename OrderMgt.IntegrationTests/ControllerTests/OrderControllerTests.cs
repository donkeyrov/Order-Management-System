using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrderMgt.Model.Entities;
using OrderMgt.Model.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace OrderMgt.IntegrationTests.ControllerTests
{
    public class OrderControllerTests: IDisposable
    {
        private WebApplicationFactory _webApplicationFactory;
        private HttpClient _httpClient;

        public OrderControllerTests()
        {
            _webApplicationFactory = new WebApplicationFactory();
            _httpClient = _webApplicationFactory.CreateClient();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _webApplicationFactory?.Dispose();
        }

        private async Task<string> GetToken(string username, string password)
        {
            var user = new LoginModel
            {
                Username = username,
                Password = password
            };

            string json = JsonConvert.SerializeObject(user);   

            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync("api/Authentication/Login", content);
            
            if (!response.IsSuccessStatusCode) return null;

            var contents = await response.Content.ReadAsStringAsync();            
            var model = JsonConvert.DeserializeObject<LoginResponseModel>(contents);
            return model?.token;
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllOrders_WhenOrdersExist()
        {
            string token = await GetToken("user@demo.com", "test1234");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var Orders = new List<Order>()
            {
                new(){OrderID = 1,OrderNo = "001",OrderStatus = "Created",CustomerID = 1,TransactionDate = DateTime.Now,Details= "sample order",Total=2300,CompletedBy="user",DateCreated=DateTime.Now },
                new(){OrderID = 1,OrderNo = "002",OrderStatus = "Created",CustomerID = 1,TransactionDate = DateTime.Now,Details= "sample order 2",Total=4300,CompletedBy="user",DateCreated=DateTime.Now },
                new(){OrderID = 1,OrderNo = "003",OrderStatus = "Created",CustomerID = 2,TransactionDate = DateTime.Now,Details= "sample order 3",Total=6300,CompletedBy="user",DateCreated=DateTime.Now },
                new(){OrderID = 1,OrderNo = "004",OrderStatus = "Created",CustomerID = 3,TransactionDate = DateTime.Now,Details= "sample order 4",Total=1300,CompletedBy="user",DateCreated=DateTime.Now }
            };

            _webApplicationFactory.OrderRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(Orders);

            var response = await _httpClient.GetAsync("/api/Order/GetAllAsync");
            var contents = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<BaseResponseModel>(contents);

            var result = model.Data;

            model.Success.Should().BeTrue();
            result.Should().NotBeNull();            
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoOrdersExist()
        {
            string token = await GetToken("user@demo.com", "test1234");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            _webApplicationFactory.OrderRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Order>());

            var response = await _httpClient.GetAsync("/api/Order/GetAllAsync");
            var contents = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<BaseResponseModel>(contents);

            model.Success.Should().BeTrue();
            model.Data.Should().NotBeNull();
            var orders = ((JArray)model.Data).ToObject<List<Order>>();
            orders.Should().BeEmpty();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOrder_WhenOrderExists()
        {
            string token = await GetToken("user@demo.com", "test1234");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var order = new Order
            {
                OrderID = 10,
                OrderNo = "010",
                OrderStatus = "Created",
                CustomerID = 2,
                TransactionDate = DateTime.Now,
                Details = "order details",
                Total = 5000,
                CompletedBy = "user",
                DateCreated = DateTime.Now
            };

            _webApplicationFactory.OrderRepositoryMock.Setup(r => r.GetAsync(10)).ReturnsAsync(order);

            var response = await _httpClient.GetAsync("/api/Order/GetAsync?id=10");
            var contents = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<BaseResponseModel>(contents);

            model.Success.Should().BeTrue();
            model.Data.Should().NotBeNull();
            var returnedOrder = ((JObject)model.Data).ToObject<Order>();
            returnedOrder.OrderID.Should().Be(10);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNotFound_WhenOrderDoesNotExist()
        {
            string token = await GetToken("user@demo.com", "test1234");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            _webApplicationFactory.OrderRepositoryMock.Setup(r => r.GetAsync(99)).ReturnsAsync((Order)null);

            var response = await _httpClient.GetAsync("/api/Order/GetAsync?id=99");
            var contents = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<BaseResponseModel>(contents);

            //model.Success.Should().BeFalse();
            model.Data.Should().BeNull();
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldReturnSuccess_WhenOrderIsValid()
        {
            string token = await GetToken("user@demo.com", "test1234");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var order = new Order
            {
                OrderNo = "011",
                OrderStatus = "Created",
                CustomerID = 3,
                TransactionDate = DateTime.Now,
                Details = "new order",
                Total = 7000,                                
                CompletedBy = "user",
                DateCreated = DateTime.Now
            };            

            _webApplicationFactory.OrderRepositoryMock
                    .Setup(r => r.AddAsync(It.IsAny<Order>()))
                    .ReturnsAsync(true);

            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Order/AddAsync", content);
            var contents = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<BaseResponseModel>(contents);

            model.Success.Should().BeTrue();
            model.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldReturnBadRequest_WhenOrderIsInvalid()
        {
            string token = await GetToken("user@demo.com", "test1234");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var order = new Order(); // missing required fields

            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Order/AddAsync", content);
            var contents = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<BaseResponseModel>(contents);

            model.Success.Should().BeFalse();
        }

    }
}