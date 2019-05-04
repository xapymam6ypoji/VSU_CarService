using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSU_CarService.Entities;
using VSU_CarService.Flyouts;
using VSU_CarService.Repository;
using VSU_CarService.Views;

namespace VSU_CarService.Services
{
    public class FlyoutsService : IFlyoutsService
    {
        private readonly IFlyoutsContainer _container;
        private readonly IValidationService _validation;
        private readonly ICarServiceRepository _repository;

        public FlyoutsService(MainWindow container, IValidationService validation, ICarServiceRepository repository)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _validation = validation ?? throw new ArgumentNullException(nameof(validation));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc />
        /// <summary>
        /// Вызвать окно добавления нового мастера
        /// </summary>
        /// <returns>
        /// Если создание успешно - возвращает сущность
        /// Если отмена - null
        /// </returns>
        public async Task<ServiceMaster> ShowAddServiceMasterView()
        {
            var uc = new ServiceMasterAddFlyout(_validation, _repository, _container.HideLeftFlyout);
            await _container.ShowLeftFlyout(uc, "Добавление мастера", true);
            return uc.NewServiceMaster;
        }
        /// <summary>
        /// Вызвать окно добавления нового типа работ
        /// </summary>
        /// <returns>
        /// Если создание успешно - возвращает сущность
        /// Если отмена - null
        /// </returns>
        public async Task<WorkType> ShowAddWorkTypeView()
        {
            var uc = new WorkTypeFlyout(_validation, _repository, _container.HideRightFlyout);
            await _container.ShowRightFlyout(uc, "Добавление вида работы", true);
            return uc.NewWorkType;
        }
    }
}
