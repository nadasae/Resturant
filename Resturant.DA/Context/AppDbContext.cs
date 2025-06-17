using Microsoft.EntityFrameworkCore;
using Resturant.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.DA.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {
            
            #region Category --> MenuItems (1:M)
            modelBuilder.Entity<Category>()
                   .HasMany(c => c.MenuItems)
                   .WithOne(m => m.Category)
                   .HasForeignKey(m => m.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict); 
            #endregion

            #region MenuItem --> OrderItems (1:M)
            modelBuilder.Entity<MenuItem>()
             .HasMany(m => m.OrderItems)
             .WithOne(oi => oi.MenuItem)
             .HasForeignKey(oi => oi.MenuItemId)
             .OnDelete(DeleteBehavior.Restrict); 
            #endregion

            #region Order --> OrderItems (1:M)
            modelBuilder.Entity<Order>()
                   .HasMany(o => o.OrderItems)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade); 
            #endregion

            #region Order --> Table (M:1, optional)
            modelBuilder.Entity<Order>()
                  .HasOne(o => o.Table)
                  .WithMany(t => t.Orders)
                  .HasForeignKey(o => o.TableId)
                  .OnDelete(DeleteBehavior.SetNull); 
            #endregion

            #region Order -> Staff (M:1, optional)
            modelBuilder.Entity<Order>()
                   .HasOne(o => o.AssignedStaff)
                   .WithMany(s => s.AssignedOrders)
                   .HasForeignKey(o => o.StaffId)
                   .OnDelete(DeleteBehavior.SetNull); 
            #endregion

            base.OnModelCreating(modelBuilder);
            
        }
        #region Dbsets
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Table> Tables { get; set; } 
        #endregion

    }
}
