using WebDb.Repositories;
using WebDb.Repositories.IPostgres;
using WebDb.Repositories.postgres;
using Microsoft.EntityFrameworkCore;
using WebDb.Entities;
using System.Text.Json.Serialization;
using WebDb.Helpers;

namespace WebDb
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<Category>>();
            services.AddSingleton(typeof(ILogger), logger);

            //services.AddSingleton<IConnectionMultiplexer, ConnectionMultiplexer>(c => {
            //    var config = ConfigurationOptions.Parse(_configuration.GetConnectionString("Redis"), true);
            //    return ConnectionMultiplexer.Connect(config);
            //});

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICategoryRepo, CategoryRepository>();
            services.AddScoped<ICustomerRepo, CustomerRepository>();
            services.AddScoped<IDeliveryRepo, DeliveryRepository>();
            services.AddScoped<IManufactureRepo, ManufactureRepository>();
            services.AddScoped<IPriceChangeRepo, PriceChangeRepository>();
            services.AddScoped<IProductRepo, ProductRepository>();
            services.AddScoped<IPurchaseItemRepo, PurchaseItemRepository>();
            services.AddScoped<IPurchaseRepo, PurchaseRepository>();
            services.AddScoped<IStoreRepo, StoreRepository>();

            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddHttpContextAccessor();

            services.AddDbContext<ContextDb>(options =>
            {
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            //var context = app.ApplicationServices.CreateScope().ServiceProvider
            //    .GetRequiredService<ContextDb>();
            //SeedData.SeedDatabase(context);

            app.UseRouting();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Social CRM API v1");
                x.RoutePrefix = "swagger";
            });
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}

