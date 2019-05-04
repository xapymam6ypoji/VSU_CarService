using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using VSU_CarService.Entities;
using VSU_CarService.Repository;
using VSU_CarService.Services;

namespace VSU_CarService.ViewModels
{
    public class ServiceMastersViewModel : BindableBase
    {
        #region services
        private readonly ICarServiceRepository _repository;
        private readonly IFlyoutsService _flyouts;
        #endregion

        #region backing field
        private ObservableCollection<ServiceMaster> _serviceMasters;
        private bool _isLoadServiceMasters;
        #endregion


        #region indicators
        public bool IsLoadServiceMasters
        {
            get => _isLoadServiceMasters;
            private set => SetProperty(ref _isLoadServiceMasters, value);
        }
        #endregion

        #region Tables
        public ObservableCollection<ServiceMaster> ServiceMasters
        {
            get => _serviceMasters;
            set => SetProperty(ref _serviceMasters, value);
        }
        #endregion

        public DelegateCommand AddNewMasterCommand { get; }

        public ServiceMastersViewModel(ICarServiceRepository repository, IFlyoutsService flyouts)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _flyouts = flyouts ?? throw new ArgumentNullException(nameof(flyouts));
            var tsk = Init();

            AddNewMasterCommand = new DelegateCommand(async () =>
            {
                var newMaster = await _flyouts.ShowAddServiceMasterView();
                if (newMaster == null) return;
                LoadServiceMasters();
            });
        }

        private async Task Init()
        {
            LoadServiceMasters();
        }

        private async void LoadServiceMasters()
        {
            IsLoadServiceMasters = true;
            var masters = await _repository.GetAllServiceMasteers();
            ServiceMasters = new ObservableCollection<ServiceMaster>(masters);
            IsLoadServiceMasters = false;
        } 
    }
}
