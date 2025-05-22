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
