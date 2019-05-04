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
    public class WorkTypesViewModel : BindableBase
    {
        #region services
        private readonly ICarServiceRepository _repository;
        private readonly IFlyoutsService _flyouts;
        #endregion

        #region backing field
        private ObservableCollection<WorkType> _workTypes;
        private bool _isLoadWorkTypes;
        #endregion


        #region indicators
        public bool IsLoadWorkTypes
        {
            get => _isLoadWorkTypes;
            private set => SetProperty(ref _isLoadWorkTypes, value);
        }
        #endregion

        #region Tables
        public ObservableCollection<WorkType> WorkTypes
        {
            get => _workTypes;
            set => SetProperty(ref _workTypes, value);
        }
        #endregion

        public DelegateCommand AddNewWorkTypeCommand { get; }

        public WorkTypesViewModel(ICarServiceRepository repository, IFlyoutsService flyouts)
        {

            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _flyouts = flyouts ?? throw new ArgumentNullException(nameof(flyouts));
            var tsk = Init();

            AddNewWorkTypeCommand = new DelegateCommand(async () =>
            {
                var workType = await _flyouts.ShowAddWorkTypeView();
                if (workType == null) return;
                LoadWorkTypes();
            });
        }

        private async Task Init()
        {
            LoadWorkTypes();
        }

        private async void LoadWorkTypes()
        {
            IsLoadWorkTypes = true;
            var workTypes = await _repository.GetAllWorkTypes();
            WorkTypes = new ObservableCollection<WorkType>(workTypes);
            IsLoadWorkTypes = false;
        }
    }
}
