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

namespace Uber_MVVM.View
{
    /// <summary>
    /// Interaction logic for MyProfilePage.xaml
    /// </summary>
    public partial class MyProfilePage : UserControl
    {
        public MyProfilePage()
        {
            InitializeComponent();
        }

        private void EditBtnClick(object sender, RoutedEventArgs e)
        {
            NameTB.IsEnabled = true;
            SurnameTB.IsEnabled = true;
            PhoneTB.IsEnabled = true;
            LoginTB.IsEnabled = true;
            PasswordTB.IsEnabled = true;

            SaveBtn.IsEnabled = true;
            CancelBtn.IsEnabled = true;
            BrowseBtn.IsEnabled = true;
            ChangeBtn.IsEnabled = true;

            EditBtn.IsEnabled = false;
        }
        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
            NameTB.IsEnabled = false;
            SurnameTB.IsEnabled = false;
            PhoneTB.IsEnabled = false;
            LoginTB.IsEnabled = false;
            PasswordTB.IsEnabled = false;

            SaveBtn.IsEnabled = false;
            CancelBtn.IsEnabled = false;
            BrowseBtn.IsEnabled = false;
            ChangeBtn.IsEnabled = false;

            EditBtn.IsEnabled = true;
        }
    }
}
