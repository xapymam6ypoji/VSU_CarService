using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;

namespace VSU_CarService.Views
{
    public interface IFlyoutsContainer
    {
        Task ShowRightFlyout(UserControl uc, string header, bool isModal, int layer);
        Task ShowLeftFlyout(UserControl uc, string header, bool isModal);
        void HideRightFlyout();
        void HideLeftFlyout();
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, IFlyoutsContainer
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Tile_MouseEnter(object sender, MouseEventArgs e)
        {
            var tile = (Tile)sender;
            var spChildren = ((StackPanel)tile.Content).Children;
            var fa = (PackIconFontAwesome)spChildren[0];
            fa.Width = 60;
            fa.Height = 60;
        }

        private void Tile_MouseLeave(object sender, MouseEventArgs e)
        {
            var tile = (Tile)sender;
            var spChildren = ((StackPanel)tile.Content).Children;
            var fa = (PackIconFontAwesome)spChildren[0];
            fa.Width = 55;
            fa.Height = 55;
        }

        public async Task ShowRightFlyout(UserControl uc, string header, bool isModal, int layer)
        {
            switch (layer)
            {
                case 0:
                    await ShowFlyout(uc, header, isModal, FlyoutRightLayer0);
                    break;
                case 1:
                    await ShowFlyout(uc, header, isModal, FlyoutRightLayer1);
                    break;
                default: throw new ArgumentException(nameof(layer));
            }
        }

        public void HideRightFlyout()
        {
            FlyoutRightLayer0.IsOpen = false;
        }
        public async Task ShowLeftFlyout(UserControl uc, string header, bool isModal)
        {
            await ShowFlyout(uc, header, isModal, FlyoutLeft);
        }
        public void HideLeftFlyout()
        {
            FlyoutLeft.IsOpen = false;
        }


        private async Task ShowFlyout(UserControl uc, string header, bool isModal, Flyout flyout)
        {
            flyout.IsModal = isModal;
            flyout.Header = header;
            flyout.Content = uc;
            flyout.IsOpen = true;
            while (flyout.IsOpen)
            {
                await Task.Delay(500);
            }
        }

    }
}
