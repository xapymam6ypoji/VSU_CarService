using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VSU_CarService.Entities;

namespace VSU_CarService.Repository
{
    public class CarServiceSqliteContext : DbContext
    {
        public virtual DbSet<CarBrand> CarBrands { get; set; }
        public virtual DbSet<CarModel> CarModels { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<ServiceMaster> ServiceMasters { get; set; }
        public virtual DbSet<WorkType> WorkTypes { get; set; }
        public virtual DbSet<CustomerCar> СustomerCars { get; set; }

        public CarServiceSqliteContext()
        { }

        public CarServiceSqliteContext(DbContextOptions<CarServiceSqliteContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (optionsBuilder.IsConfigured)
            {
                var path = Path.Combine(Environment.CurrentDirectory, "Repository.db");
                optionsBuilder.UseSqlite($"Data Source={path}");
            }*/
        }
    }
}
