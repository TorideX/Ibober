using Microsoft.Maps.MapControl.WPF;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Uber_MVVM.Model;
using Uber_MVVM.ModelView;

namespace Uber_MVVM.View
{
    public partial class IboberView : Window
    {
        int currentIndex = -1;
        bool isMenuOpened = false;        
        public IboberView()
        {
            InitializeComponent();
            this.DataContext = new IboberViewModel();

            MenuListView.SelectedIndex = 0;
        }

        private void MenuButtonClick(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();
            if (isMenuOpened == false)
            {
                isMenuOpened = true;
                animation.To = 220;
                animation.Duration = TimeSpan.FromSeconds(0.3);
            }
            else
            {
                isMenuOpened = false;
                animation.To = 60;
                animation.Duration = TimeSpan.FromSeconds(0.3);
            }
            menuGrid.BeginAnimation(Grid.WidthProperty, animation);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.WindowState = WindowState.Minimized;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (WindowStyle != WindowStyle.None)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, (DispatcherOperationCallback)delegate (object unused)
                {
                    WindowStyle = WindowStyle.None;
                    return null;
                }
                , null);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = (sender as ListView).SelectedIndex;
            if (index != currentIndex && index == 0)
            {
                currentIndex = 0;
                dynamicGrid.Children.Clear();
                dynamicGrid.Children.Add(new MainMenuPage());
            }
            else if (index != currentIndex && index == 1)
            {
                currentIndex = 1;
            }
            else if (index != currentIndex && index == 2)
            {
                currentIndex = 2;
                dynamicGrid.Children.Clear();
                dynamicGrid.Children.Add(new MyProfilePage());
            }
        }
    }
}
