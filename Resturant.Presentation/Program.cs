using Microsoft.EntityFrameworkCore;
using Resturant.BL.AppServices;
using Resturant.BL.Contracts;
using Resturant.BL.Features.Categories.Mapping;
using Resturant.BL.Features.MenuItems.Mapping;
using Resturant.BL.Features.Tables.Mapping;
using Resturant.Core.Interfaces.Repositories;
using Resturant.DA.Context;
using Resturant.DA.Implementations.Base;
using Resturant.DA.Implementations.Repositories;
using System;

namespace Resturant.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(ConnectionString);
            }).AddScoped<AppDbContext>();


            #region Repositories Registeration
            builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IStaffRepository, StaffRepository>();
            builder.Services.AddScoped<ITableRepository, TableRepository>();
            #endregion
            #region Services Registeration
            builder.Services.AddScoped<ITableService, TableService>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<IMenuItemServices, MenuItemsServices>();
            #endregion

            #region Mapping Registeration
            builder.Services.AddAutoMapper(typeof(TableMappingProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(CategoryMappingProfile).Assembly); 
            builder.Services.AddAutoMapper(typeof(MenuItemMappingProfile).Assembly);
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
