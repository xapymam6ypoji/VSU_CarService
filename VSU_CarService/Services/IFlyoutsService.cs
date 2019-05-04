using System.Threading.Tasks;
using VSU_CarService.Entities;

namespace VSU_CarService.Services
{
    public interface IFlyoutsService
    {        
        /// <summary>
        /// Вызвать окно добавления нового мастера
        /// </summary>
        /// <returns>
        /// Если создание успешно - возвращает сущность
        /// Если отмена - null
        /// </returns>
        Task<ServiceMaster> ShowAddServiceMasterView();
        /// <summary>
        /// Вызвать окно добавления нового типа работ
        /// </summary>
        /// <returns>
        /// Если создание успешно - возвращает сущность
        /// Если отмена - null
        /// </returns>
        Task<WorkType> ShowAddWorkTypeView();
    }
}