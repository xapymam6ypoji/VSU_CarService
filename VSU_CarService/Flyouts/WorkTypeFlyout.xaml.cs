using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VSU_CarService.Entities;
using VSU_CarService.Flyouts.Vm;
using VSU_CarService.Repository;
using VSU_CarService.Services;

namespace VSU_CarService.Flyouts
{
    public partial class WorkTypeFlyout : UserControl
    {
        private readonly Action _hideFlyout;
        public WorkType NewWorkType { get; private set; }
        public WorkTypeFlyout(IValidationService validation, ICarServiceRepository repository, Action hideFlyout)
        {
            _hideFlyout = hideFlyout ?? throw new ArgumentNullException(nameof(hideFlyout));
            InitializeComponent();
            var vm = new WorkTypeFlyoutVm(OnCloseAction, repository, validation);
            DataContext = vm;
        }

        private void OnCloseAction(WorkType obj)
        {
            NewWorkType = obj;
            _hideFlyout?.Invoke();
        }
    }
}
