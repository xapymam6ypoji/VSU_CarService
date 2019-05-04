using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSU_CarService.Entities;

namespace VSU_CarService.Repository
{
    public interface ICarServiceRepository
    {
        Task<T> Create<T>(T entity) where T : class;
        CarServiceSqliteContext CreateAndGetInMemoryContext(string name);
        Task<bool> CreateDbIfNotExist();
        Task<List<CarBrand>> GetAllCarBrands();
        Task<List<Customer>> GetAllCustomers();
        Task<List<ServiceMaster>> GetAllServiceMasteers();
        Task<List<WorkType>> GetAllWorkTypes();
        Task<Customer> GetCustomersById(Guid id);
        Task<ServiceMaster> GetServiceMasterById(Guid id);
        Task<T> Remove<T>(T entity) where T : class;
        Task<T> Update<T>(T entity) where T : class;
    }
}