using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Tech.Assessment.Model.Enum;
using Tech.Assessment.Repository;
using Tech.Assessment.Repository.DBContext;
using Tech.Assessment.Repository.Entity;
using Tech.Assessment.Repository.Repository;
using Tech.Assessment.Service;

namespace Tech.Assessment.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Tech.Assessment"
                });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            });
            services.AddDbContext<ApplicationDBContext>(options => options.UseInMemoryDatabase("Tech"));
            services.AddHttpContextAccessor();
            ConfigureCustomServices(services);
        }

        private void ConfigureCustomServices(IServiceCollection services)
        {
            #region Services       
            services.AddScoped<IOrderService, OrderService>();
            #endregion

            #region Repository
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IPackageCalculationRepository, PackageCalculationRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            #endregion

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDBContext context, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AddTestData(context);

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tech.Assessment API V1"));
        }

        private void AddTestData(ApplicationDBContext context)
        {

            context.PackageCalculationDetail.Add(new PackageCalculationDetail
            {
                Id = 1,
                ProductType = (int)ProductType.PhotoBook,
                ProductSymbol = "0",
                MinWidth = 19,
                WidthUnit = "mm",
                StackCapacity = 1,
                CreatedBy = 100,
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
            });
            context.PackageCalculationDetail.Add(new PackageCalculationDetail
            {
                Id = 2,
                ProductType = (int)ProductType.Calendar,
                ProductSymbol = "|",
                MinWidth = 10,
                WidthUnit = "mm",
                StackCapacity = 1,
                CreatedBy = 100,
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
            });
            context.PackageCalculationDetail.Add(new PackageCalculationDetail
            {
                Id = 3,
                ProductType = (int)ProductType.Canvas,
                ProductSymbol = "*",
                MinWidth = 16,
                WidthUnit = "mm",
                StackCapacity = 1,
                CreatedBy = 100,
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
            });
            context.PackageCalculationDetail.Add(new PackageCalculationDetail
            {
                Id = 4,
                ProductType = (int)ProductType.Cards,
                ProductSymbol = "$",
                MinWidth = 4.7,
                WidthUnit = "mm",
                StackCapacity = 1,
                CreatedBy = 100,
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
            });
            context.PackageCalculationDetail.Add(new PackageCalculationDetail
            {
                Id = 5,
                ProductType = (int)ProductType.Mug,
                ProductSymbol = ".",
                MinWidth = 94,
                WidthUnit = "mm",
                StackCapacity = 4,
                CreatedBy = 100,
                CreatedOn = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
            });

            context.Customer.Add(new Customer()
            {
                Id = 1,
                Name = "Customer01",
                Phone = "01231212",
                PostalCode = "98922",
                City = "Tokyo",
                Country = "Japan",
                State = "TokyoState",
                Street = "101-10"
            });
            context.SaveChanges();
        }
    }
}