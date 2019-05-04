using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;
using VSU_CarService.Entities;

namespace VSU_CarService.Repository
{
    public class CarServiceSqliteRepository : ICarServiceRepository
    {
        private static readonly NLog.Logger Log = LogManager.GetCurrentClassLogger();
        private bool _isTestDataBase;
        private CarServiceSqliteContext _inMemorycontext;

        #region Context
        /// <summary>
        /// Создает и возвращает InMemory контектс для тестирования .
        /// После вызова все методы используют один внутренний контекст.
        /// Отключается вызов CloseConnection() и Dispose()
        /// </summary>
        /// <returns></returns>
        public CarServiceSqliteContext CreateAndGetInMemoryContext(string name)
        {
            var options = new DbContextOptionsBuilder<CarServiceSqliteContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;
            _inMemorycontext = new CarServiceSqliteContext(options);
            _isTestDataBase = true;
            return _inMemorycontext;
        }

        private CarServiceSqliteContext GetDbContext()
        {
            if (!_isTestDataBase)
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Repository.db");
                var options = new DbContextOptionsBuilder<CarServiceSqliteContext>()
                    .UseSqlite($"Data Source={path}")
                    .Options;
                return new CarServiceSqliteContext(options);
            }
            return _inMemorycontext;
        }


        #endregion

        /// <summary>
        /// Создает файл БД, если он не существует
        /// </summary>
        /// <returns>true - файл создан, или уже существует. false - ошибка</returns>
        public async Task<bool> CreateDbIfNotExist()
        {
            var db = GetDbContext();
            Log.Trace($"<{nameof(CreateDbIfNotExist)}()>");
            try
            {

                var isCreated = await db.Database.EnsureCreatedAsync();
                await db.SaveChangesAsync();
                Log.Trace(isCreated
                    ? $"<{nameof(CreateDbIfNotExist)}()> - database created"
                    : $"<{nameof(CreateDbIfNotExist)}()> - database already exist");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"<{nameof(CreateDbIfNotExist)}()> - error");
                Log.Error(ex);
                return false;
            }
            finally
            {
                if (!_isTestDataBase)
                {
                    db.Database.CloseConnection();
                    db.Dispose();
                }
            }
        }

        /// <summary>
        /// Получить все марки авто, включая модели
        /// </summary>
        /// <returns></returns>
        public async Task<List<CarBrand>> GetAllCarBrands()
        {
            var db = GetDbContext();

            var methodName = $"{nameof(GetAllCarBrands)}>()";
            Log.Trace(methodName);
            try
            {
                var col = await db.CarBrands
                    .Include(c=>c.CarModels)
                    .ToListAsync();
                Log.Trace($"{methodName} - return [{col.Count} entities]");
                return col;
            }
            catch (Exception ex)
            {
                Log.Error($"<{nameof(CreateDbIfNotExist)}()> - error");
                Log.Error(ex);
                return new List<CarBrand>();
            }
            finally
            {
                if (!_isTestDataBase)
                {
                    db.Database.CloseConnection();
                    db.Dispose();
                }
            }
        }

        /// <summary>
        /// Получить всех клиентов, включая их автомобили
        /// </summary>
        /// <returns></returns>
        public async Task<List<Customer>> GetAllCustomers()
        {
            var db = GetDbContext();

            var methodName = $"{nameof(GetAllCustomers)}>()";
            Log.Trace(methodName);
            try
            {
                var col = await db.Customers
                    .Include(c => c.CustomerCars)
                    .ToListAsync();
                Log.Trace($"{methodName} - return [{col.Count} entities]");
                return col;
            }
            catch (Exception ex)
            {
                Log.Error($"<{nameof(CreateDbIfNotExist)}()> - error");
                Log.Error(ex);
                return new List<Customer>();
            }
            finally
            {
                if (!_isTestDataBase)
                {
                    db.Database.CloseConnection();
                    db.Dispose();
                }
            }
        }

        /// <summary>
        /// Получить клиента, включая автомобили и заказы
        /// </summary>
        /// <returns></returns>
        public async Task<Customer> GetCustomersById(Guid id)
        {
            var db = GetDbContext();

            var methodName = $"{nameof(GetCustomersById)}>()";
            Log.Trace(methodName);
            try
            {
                var entity = await db.Customers
                    .Include(x=>x.CustomerCars)
                    .Include(x=>x.Orders)
                    .FirstOrDefaultAsync(c => c.Id == id);

                Log.Trace($"{methodName} - done");
                return entity;
            }
            catch (Exception ex)
            {
                Log.Error($"<{nameof(CreateDbIfNotExist)}()> - error");
                Log.Error(ex);
                return null;
            }
            finally
            {
                if (!_isTestDataBase)
                {
                    db.Database.CloseConnection();
                    db.Dispose();
                }
            }
        }

        /// <summary>
        /// Получить всех мастеров
        /// </summary>
        /// <returns></returns>
        public async Task<List<ServiceMaster>> GetAllServiceMasteers()
        {
            var db = GetDbContext();

            var methodName = $"{nameof(GetAllServiceMasteers)}>()";
            Log.Trace(methodName);
            try
            {
                var col = await db.ServiceMasters
                    .ToListAsync();
                Log.Trace($"{methodName} - return [{col.Count} entities]");
                return col;
            }
            catch (Exception ex)
            {
                Log.Error($"<{nameof(CreateDbIfNotExist)}()> - error");
                Log.Error(ex);
                return new List<ServiceMaster>();
            }
            finally
            {
                if (!_isTestDataBase)
                {
                    db.Database.CloseConnection();
                    db.Dispose();
                }
            }
        }

        /// <summary>
        /// Получить мастера, включая его заказы
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceMaster> GetServiceMasterById(Guid id)
        {
            var db = GetDbContext();

            var methodName = $"{nameof(GetServiceMasterById)}>()";
            Log.Trace(methodName);
            try
            {
                var entity = await db.ServiceMasters
                    .Include(x => x.Orders)
                    .FirstOrDefaultAsync(c => c.Id == id);

                Log.Trace($"{methodName} - done");
                return entity;
            }
            catch (Exception ex)
            {
                Log.Error($"<{nameof(CreateDbIfNotExist)}()> - error");
                Log.Error(ex);
                return null;
            }
            finally
            {
                if (!_isTestDataBase)
                {
                    db.Database.CloseConnection();
                    db.Dispose();
                }
            }
        }

        /// <summary>
        /// Получить все производимые работы
        /// </summary>
        /// <returns></returns>
        public async Task<List<WorkType>> GetAllWorkTypes()
        {
            var db = GetDbContext();

            var methodName = $"{nameof(GetAllWorkTypes)}>()";
            Log.Trace(methodName);
            try
            {
                var col = await db.WorkTypes
                    .ToListAsync();
                Log.Trace($"{methodName} - return [{col.Count} entities]");
                return col;
            }
            catch (Exception ex)
            {
                Log.Error($"<{nameof(CreateDbIfNotExist)}()> - error");
                Log.Error(ex);
                return new List<WorkType>();
            }
            finally
            {
                if (!_isTestDataBase)
                {
                    db.Database.CloseConnection();
                    db.Dispose();
                }
            }
        }

        #region Generic

        public async Task<T> Create<T>(T entity) where T : class
        {
            var db = GetDbContext();
            var dbSet = db.Set<T>();

            var methodName = $"{nameof(Create)}<{nameof(T)}>()";
            Log.Trace(methodName);
            try
            {
                dbSet.Add(entity);
                await db.SaveChangesAsync();
                Log.Trace($"{methodName} - done");
                return entity;
            }
            catch (Exception ex)
            {
                Log.Error($"<{nameof(CreateDbIfNotExist)}()> - error");
                Log.Error(ex);
                return null;
            }
            finally
            {
                if (!_isTestDataBase)
                {
                    db.Database.CloseConnection();
                    db.Dispose();
                }
            }
        }

        public async Task<T> Remove<T>(T entity) where T : class
        {
            var db = GetDbContext();
            var dbSet = db.Set<T>();

            var methodName = $"{nameof(Remove)}<{nameof(T)}>()";
            Log.Trace(methodName);
            try
            {
                dbSet.Remove(entity);
                await db.SaveChangesAsync();
                Log.Trace($"{methodName} - done");
                return entity;
            }
            catch (Exception ex)
            {
                Log.Error($"<{nameof(CreateDbIfNotExist)}()> - error");
                Log.Error(ex);
                return null;
            }
            finally
            {
                if (!_isTestDataBase)
                {
                    db.Database.CloseConnection();
                    db.Dispose();
                }
            }
        }
        public async Task<T> Update<T>(T entity) where T : class
        {
            var db = GetDbContext();

            var methodName = $"{nameof(Remove)}<{nameof(T)}>()";
            Log.Trace(methodName);
            try
            {
                db.Entry(entity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                Log.Trace($"{methodName} - done");
                return entity;
            }
            catch (Exception ex)
            {
                Log.Error($"<{nameof(CreateDbIfNotExist)}()> - error");
                Log.Error(ex);
                return null;
            }
            finally
            {
                if (!_isTestDataBase)
                {
                    db.Database.CloseConnection();
                    db.Dispose();
                }
            }
        }

        #endregion        
    }
}
