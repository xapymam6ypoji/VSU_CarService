using System;
using System.Collections.Generic;
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
using VSU_CarService.Repository;
using VSU_CarService.Services;

namespace VSU_CarService.Flyouts
{
    public partial class ServiceMasterAddFlyout : UserControl
    {
        private readonly IValidationService _validation;
        private readonly ICarServiceRepository _repository;
        private readonly Action _hideFlyout;
        public ServiceMaster NewServiceMaster { get; private set; }
        public ServiceMasterAddFlyout(IValidationService validation, ICarServiceRepository repository, Action hideFlyout)
        {
            _validation = validation;
            _repository = repository;
            _hideFlyout = hideFlyout ?? throw new ArgumentNullException(nameof(hideFlyout));
            InitializeComponent();
            BtnAdd.IsEnabled = false;
        }

        private async void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            NewServiceMaster = new ServiceMaster()
            {
                FirstName = TbFirstName.Text,
                MiddleName = TbMiddleName.Text,
                LastName = TbLastName.Text
            };
            await _repository.Create(NewServiceMaster);

            _hideFlyout.Invoke();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NewServiceMaster = null;
            _hideFlyout.Invoke();
        }

        private void TbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var isFirstNameValid = !string.IsNullOrEmpty(TbFirstName.Text)
                                   && _validation.ValidateStringPropertyLenght<ServiceMaster>("FirstName",
                                       TbFirstName.Text);
            var isMiddleNameValid = !string.IsNullOrEmpty(TbFirstName.Text)
                                   && _validation.ValidateStringPropertyLenght<ServiceMaster>("MiddleName",
                                       TbMiddleName.Text);
            var isLastNameValid = !string.IsNullOrEmpty(TbFirstName.Text)
                                   && _validation.ValidateStringPropertyLenght<ServiceMaster>("LastName",
                                       TbLastName.Text);
            BtnAdd.IsEnabled = isFirstNameValid && isMiddleNameValid && isLastNameValid;
        }
    }
}
