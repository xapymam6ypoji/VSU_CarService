using Prism.Commands;
using Prism.Mvvm;
using VSU_CarService.Services;
using VSU_CarService.Views;

namespace VSU_CarService.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private string _currentLoadView;

        public string CurrentLoadView
        {
            get => _currentLoadView;
            set => SetProperty(ref _currentLoadView, value);
        }


        public DelegateCommand ShowServiceMastersViewCommand { get; }
        public DelegateCommand ShowOrdersViewCommand { get; }
        public DelegateCommand ShowCustomersViewCommand { get; }
        public DelegateCommand ShowWorkTypesViewCommand { get; }


        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ShowServiceMastersViewCommand = new DelegateCommand(ShowServiceMasterView, () => 
                    CurrentLoadView != nameof(ServiceMastersView)).ObservesProperty(() => CurrentLoadView);
            ShowWorkTypesViewCommand = new DelegateCommand(ShowWorkTypesView, () =>
                CurrentLoadView != nameof(WorkTypesView)).ObservesProperty(() => CurrentLoadView);
        }

        private void ShowServiceMasterView()
        {
            _navigationService.LoadToContentRegion(nameof(ServiceMastersView));
            CurrentLoadView = nameof(ServiceMastersView);
        }
        private void ShowWorkTypesView()
        {
            _navigationService.LoadToContentRegion(nameof(WorkTypesView));
            CurrentLoadView = nameof(WorkTypesView);
        }
    }
}
