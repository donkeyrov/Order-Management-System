using OrderMgt.API.Interfaces;
using OrderMgt.API.Repositories;
using OrderMgt.API.Services;

namespace OrderMgt.API.Extensions
{
    /// <summary>
    /// Provides extension methods for registering application services and dependencies  into an <see
    /// cref="IServiceCollection"/>.
    /// </summary>
    /// <remarks>This class contains methods to simplify the registration of repositories, services,  and
    /// other dependencies commonly used in the application. The registered services  include transient and scoped
    /// lifetimes, as well as additional configurations such as  distributed memory caching and exception
    /// handling.</remarks>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderHistoryRepository, OrderHistoryRepository>();
            services.AddTransient<IOrderProductRepository, OrderProductRepository>();           
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<ISegmentRepository, SegmentRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ITransactionCodeRepository, TransactionCodeRepository>();
            services.AddTransient<IPromotionRepository, PromotionRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddTransient<ExceptionHandler>();
            services.AddDistributedMemoryCache();

            services.AddExceptionHandler<ExceptionHandler>();
            return services;
        }
    }
}
