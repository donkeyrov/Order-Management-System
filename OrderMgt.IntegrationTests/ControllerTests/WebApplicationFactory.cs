using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using OrderMgt.API.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace OrderMgt.IntegrationTests.ControllerTests
{
    public class WebApplicationFactory: WebApplicationFactory<Program>
    {
        public Mock<IOrderRepository> OrderRepositoryMock { get; }

        public WebApplicationFactory()
        {
            OrderRepositoryMock = new Mock<IOrderRepository>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(OrderRepositoryMock.Object);
            });
        }
    }
}
