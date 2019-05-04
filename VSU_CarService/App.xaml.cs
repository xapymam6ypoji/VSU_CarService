using System;
using System.IO;
using System.Threading.Tasks;
using VSU_CarService.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Fluent;
using Prism.DryIoc;
using VSU_CarService.Entities;
using VSU_CarService.Repository;
using VSU_CarService.Services;

namespace VSU_CarService
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static readonly NLog.Logger Log = LogManager.GetCurrentClassLogger();
        protected override Window CreateShell()
        {        
            return Container.Resolve<MainWindow>();
        }

        protected override async void RegisterTypes(IContainerRegistry containerRegistry)
      {
            try
            {
                var db = new CarServiceSqliteRepository();
                var isOkDb = await db.CreateDbIfNotExist();
                if(!isOkDb) throw new Exception("Check database fail");

                containerRegistry.RegisterSingleton<MainWindow>();
                containerRegistry.RegisterSingleton<IFlyoutsService, FlyoutsService>();
                containerRegistry.RegisterSingleton<INavigationService, NavigationService>();
                containerRegistry.RegisterInstance<ICarServiceRepository>(db);
                containerRegistry.RegisterSingleton<IValidationService, ValidationService>();

                containerRegistry.RegisterForNavigation<ServiceMastersView>();
                containerRegistry.RegisterForNavigation<WorkTypesView>();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace, e.Message);
                Log.Fatal(e);
            }


        }

        public async Task Get()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Repository.db");
            var options = new DbContextOptionsBuilder<CarServiceSqliteContext>()
                .UseSqlite($"Data Source={path}")
                .Options;
            var db = new CarServiceSqliteContext(options);
            var ff = await db.Customers.Include(x=>x.Orders).Include(x=>x.CustomerCars).FirstOrDefaultAsync();

            var orders = await db.Orders.Include(c => c.ServiceMaster)
                .Include(x=>x.Customer)
                .Include(c=>c.CustomerCar).ToListAsync();
        }
        public async Task Init()
        {
            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Repository.db");
                var options = new DbContextOptionsBuilder<CarServiceSqliteContext>()
                    .UseSqlite($"Data Source={path}")
                    .Options;
                var db = new CarServiceSqliteContext(options);
                var isCreated = await db.Database.EnsureCreatedAsync();
                await db.SaveChangesAsync();

                var kia = new CarBrand()
                {
                    Name = "KIA"
                };
                db.CarBrands.Add(kia);
                await db.SaveChangesAsync();

                var kiaSportage = new CarModel()
                {
                    Name = "Sportage",
                    CarBrand = kia
                };
                db.CarModels.Add(kiaSportage);
                await db.SaveChangesAsync();

                var customer = new Customer()
                {
                    FirstName = "Customer",
                    LastName = "Customer"
                };
                db.Customers.Add(customer);
                await db.SaveChangesAsync();

                var workType = new WorkType()
                {
                    Name = "Замена колодок",
                    Description = "Description"
                };
                db.WorkTypes.Add(workType);
                await db.SaveChangesAsync();

                var serviceMaster = new ServiceMaster()
                {
                    FirstName = "Master1",
                    LastName = "Master1"
                };
                db.ServiceMasters.Add(serviceMaster);
                await db.SaveChangesAsync();

                var customerCar = new CustomerCar()
                {
                    CarModel = kiaSportage,
                    RegistrationNumber = "КО222М",
                    Customer = customer
                };
                db.СustomerCars.Add(customerCar);
                await db.SaveChangesAsync();

                var order = new Order()
                {
                    Customer = customer,
                    CustomerCar = customerCar,
                    ServiceMaster = serviceMaster,
                    TimeStamp = DateTime.Now
                };
                db.Orders.Add(order);
                await db.SaveChangesAsync();

                var orderItem = new OrderItem()
                {
                    Order = order,
                    WorkType = workType
                };
                db.OrderItems.Add(orderItem);
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
