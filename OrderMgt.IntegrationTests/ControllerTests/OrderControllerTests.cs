using Moq;
using OrderMgt.Model.Entities;
using System.Net;

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

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllOrders_WhenOrdersExist()
        {
            var Orders = new List<Order>()
            {
                new(){OrderID = 1,OrderNo = "001",OrderStatus = "Created",CustomerID = 1,TransactionDate = DateTime.Now,Details= "sample order",Total=2300,CompletedBy="user",DateCreated=DateTime.Now },
                new(){OrderID = 1,OrderNo = "002",OrderStatus = "Created",CustomerID = 1,TransactionDate = DateTime.Now,Details= "sample order 2",Total=4300,CompletedBy="user",DateCreated=DateTime.Now },
                new(){OrderID = 1,OrderNo = "003",OrderStatus = "Created",CustomerID = 2,TransactionDate = DateTime.Now,Details= "sample order 3",Total=6300,CompletedBy="user",DateCreated=DateTime.Now },
                new(){OrderID = 1,OrderNo = "004",OrderStatus = "Created",CustomerID = 3,TransactionDate = DateTime.Now,Details= "sample order 4",Total=1300,CompletedBy="user",DateCreated=DateTime.Now }
            };

            _webApplicationFactory.OrderRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(Orders);

            var response = await _httpClient.GetAsync("/api/Order/GetAllAsync");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}