using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BackendHelper.Helpers;
using BackendHelper.Repositories;
using BackendHelper.Repositories.IRepos;
using BackendHelper.Repositories.repos;
using BackendEntities.Entities;
using BackendEntities;

namespace BackendApi
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

            // create a logger factory
            var loggerFactory = LoggerFactory.Create(
                builder => builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug));

            // create a logger
            var logger = loggerFactory.CreateLogger<Startup>();

            // logging
            logger.LogTrace("Trace message");
            logger.LogDebug("Debug message");
            logger.LogInformation("Info message");
            logger.LogWarning("Warning message");
            logger.LogError("Error message");
            logger.LogCritical("Critical message");

            services.AddSingleton(typeof(ILogger), logger);

            //var serviceProvider = services.BuildServiceProvider();
            //var logger = serviceProvider.GetService<ILogger<Category>>();
            //services.AddSingleton(typeof(ILogger), logger);

            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);

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
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("BackendApi"));
                //assembly => assembly.MigrationsAssembly("BackendEntities.Migrations"));
        });

            //serviceProvider.Dispose();

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

