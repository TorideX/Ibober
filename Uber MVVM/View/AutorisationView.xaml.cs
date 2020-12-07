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
using System.Windows.Threading;
using Uber_MVVM.Model;
using Uber_MVVM.ModelView;
using Uber_MVVM.View;

namespace Uber_MVVM
{
    public partial class AutorisationView : Window
    {
        public AutorisationView()
        {
            InitializeComponent();
            WindowManager.Reference.Autorisation = this;
            this.DataContext = MyIoC.Reference.Resolve<AutorisationViewModel>();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            dynamicGrid.Children.Clear();
            dynamicGrid.Children.Add(new LoginPage());
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            dynamicGrid.Children.Clear();
            dynamicGrid.Children.Add(new SignInPage());
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }    
}
