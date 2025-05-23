using Challenge.Business.BalanceOperations;
using Challenge.Business.ErrorOperations;
using Challenge.Business.PreOrderOperations;
using Challenge.Business.ProductOperations;
using Challenge.Persistence.Manager.Abstract;
using Challenge.Persistence.Manager.Concrete;
using Challenge.Persistence.Repositories;
using Challenge.Persistence.Repositories.Abstract;
using Challenge.Persistence.Repositories.Concrete;

namespace Challenge.API
{
    public static class ServiceRegistration
    {
        /// <summary>
        /// Registers application services, repositories, managers, and operations with the dependency injection container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.</returns>
        public static IServiceCollection GetServiceCollection(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();
            services.AddScoped<IBalanceRepository, BalanceRepository>();
            services.AddScoped<IErrorRepository, ErrorRepository>();
            services.AddScoped<IPreOrderRepository, PreOrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IBalanceManager, BalanceManager>();
            services.AddScoped<IErrorManager, ErrorManager>();
            services.AddScoped<IPreOrderManager, PreOrderManager>();
            services.AddScoped<IProductManager, ProductManager>();

            services.AddScoped<IBalanceOperations, BalanceOperations>();
            services.AddScoped<IErrorOperations, ErrorOperations>();
            services.AddScoped<IPreOrderOperations, PreOrderOperations>();
            services.AddScoped<IProductOperations, ProductOperations>();

            return services;
        }
    }
}
