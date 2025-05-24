using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using OrderMgt.API.Interfaces;
using OrderMgt.API.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace OrderMgt.IntegrationTests.ControllerTests
{
    public class WebApplicationFactory : WebApplicationFactory<Program>
    {
        public Mock<IOrderRepository> OrderRepositoryMock { get; }
        public Mock<IOrderService> OrderServiceMock { get; }

        public WebApplicationFactory()
        {
            OrderRepositoryMock = new Mock<IOrderRepository>();
            OrderServiceMock = new Mock<IOrderService>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(OrderRepositoryMock.Object);
                services.AddSingleton(OrderServiceMock.Object);
            });
        }
    }
}
