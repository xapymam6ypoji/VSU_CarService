using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace VSU_CarService.Flyouts.Vm
{
    public class WorkTypeFlyoutVm :BindableBase
    {
        private string _name;
        private double? _workingHours;
        private double? _price;

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
    }
}
