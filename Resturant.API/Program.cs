using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Resturant.BL.AppServices;
using Resturant.BL.Contracts;
using Resturant.BL.Features.Categories.Mapping;
using Resturant.BL.Features.MenuItems.Mapping;
using Resturant.BL.Features.Orders.Mapping;
using Resturant.BL.Features.Tables.Mapping;
using Resturant.Core.Interfaces.Repositories;
using Resturant.DA.Context;
using Resturant.DA.Implementations.Base;
using Resturant.DA.Implementations.Repositories;

namespace Resturant.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // DbContext Configuration
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            // Repositories
            builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IStaffRepository, StaffRepository>();
            builder.Services.AddScoped<ITableRepository, TableRepository>();

            // Services
            builder.Services.AddScoped<ITableService, TableService>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<IMenuItemServices, MenuItemsServices>();
            builder.Services.AddScoped<IOrderServices, OrderServices>();    

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(TableMappingProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(CategoryMappingProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(MenuItemMappingProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(OrderMappingProfile).Assembly);

            // MVC Controllers
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Middleware Pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
