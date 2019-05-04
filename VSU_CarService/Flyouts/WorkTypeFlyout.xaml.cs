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
using VSU_CarService.Repository;
using VSU_CarService.Services;

namespace VSU_CarService.Flyouts
{
    public partial class WorkTypeFlyout : UserControl
    {
        private readonly IValidationService _validation;
        private readonly ICarServiceRepository _repository;
        private readonly Action _hideFlyout;
        private bool _isTextValuesValid;
        private bool _isNumericValuesValid;
        private bool _isDescriptionValid;
        public WorkType NewWorkType { get; private set; }
        public WorkTypeFlyout(IValidationService validation, ICarServiceRepository repository, Action hideFlyout)
        {
            _validation = validation;
            _repository = repository;
            _hideFlyout = hideFlyout ?? throw new ArgumentNullException(nameof(hideFlyout));
            InitializeComponent();
            BtnAdd.IsEnabled = false;
        }

        private async void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if(TbPrice.Value == null || TbWorkingHours.Value == null) return;
            var descr = new TextRange(TbDescription.Document.ContentStart, TbDescription.Document.ContentEnd).Text;
            NewWorkType = new WorkType()
            {
                Description = descr,
                Name = TbName.Text,
                Price = (double)TbPrice.Value,
                WorkingHours = (double)TbWorkingHours.Value
            };

            await _repository.Create(NewWorkType);
            _hideFlyout.Invoke();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NewWorkType = null;
            _hideFlyout.Invoke();
        }

        private void Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isTextValuesValid = !string.IsNullOrEmpty(TbName.Text) 
                                 && _validation.ValidateStringPropertyLenght<WorkType>("Name", TbName.Text);
            EnableBtnAddIfDataValid();
        }

        private void Tb_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            _isNumericValuesValid = TbPrice.Value >= 0 
                                    && TbWorkingHours.Value >= 0;
            EnableBtnAddIfDataValid();
        }

        private void TbDescription_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var descr = new TextRange(TbDescription.Document.ContentStart, TbDescription.Document.ContentEnd).Text;
            _isDescriptionValid = _validation.ValidateStringPropertyLenght<WorkType>("Description", descr);

            EnableBtnAddIfDataValid();
        }

        private void EnableBtnAddIfDataValid()
        {
            BtnAdd.IsEnabled = _isTextValuesValid && _isNumericValuesValid && _isDescriptionValid;
        }
    }
}
