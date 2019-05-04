using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using VSU_CarService.Entities;
using VSU_CarService.Repository;
using VSU_CarService.Services;

namespace VSU_CarService.Flyouts.Vm
{
    public class WorkTypeFlyoutVm : BindableBase, IDataErrorInfo
    {
        private readonly Action<WorkType> _onCloseAction;
        private readonly ICarServiceRepository _repository;
        private readonly IValidationService _validation;
        private string _name;
        private double? _workingHours;
        private double? _price;
        private string _description;

        public WorkTypeFlyoutVm(Action<WorkType> onCloseAction, ICarServiceRepository repository, IValidationService validation)
        {
            _onCloseAction = onCloseAction ?? throw new ArgumentNullException(nameof(onCloseAction));
            _repository = repository;
            _validation = validation;
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public double? WorkingHours
        {
            get => _workingHours;
            set => SetProperty(ref _workingHours, value);
        }
        public double? Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Name):
                        if (!_validation.ValidateStringPropertyLenght<WorkType>("Name", Name))
                        {
                            return "Название слишком длинное";
                        }
                        break;
                    case nameof(WorkingHours):
                        break;
                    case nameof(Price):
                        break;
                    case nameof(Description):
                        if (!_validation.ValidateStringPropertyLenght<WorkType>("Description", Description))
                        {
                            return "Описание слишком длинное";
                        }
                        break;
                }

                return null;
            }
        }

        public string Error => string.Empty;
    }
}
