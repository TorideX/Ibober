using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Uber_MVVM.Model;

namespace Uber_MVVM.ModelView
{
    class AutorisationViewModel: INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChangedHandler();
            }
        }
        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                PropertyChangedHandler();
            }
        }
        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                PropertyChangedHandler();
            }
        }
        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                PropertyChangedHandler();
            }
        }


        public Command LoginClickCommand { get; set; }
        public Command SignInClickCommand { get; set; }        

        public AutorisationViewModel()
        {
            LoginClickCommand = new Command(LoginClickExecute, LoginClickEnable);
            SignInClickCommand = new Command(SignInClickExecute, SignInClickEnable);            
        }

        private bool SignInClickEnable(object obj)
        {
            return !(string.IsNullOrEmpty((obj as PasswordBox).Password) || 
                string.IsNullOrEmpty(Name) || 
                string.IsNullOrEmpty(Surname) || 
                string.IsNullOrEmpty(Phone) || 
                string.IsNullOrEmpty(Login));
        }
        
        private void SignInClickExecute(object obj)
        {
            UserService.Reference.User.Name = Name;
            UserService.Reference.User.Surname = Surname;
            UserService.Reference.User.Phone = Phone;
            UserService.Reference.User.Login = Login;
            UserService.Reference.User.Password = (obj as PasswordBox).Password;

            UserService.Reference.SignIn();

            Name = string.Empty;
            Surname = string.Empty;
            Phone = string.Empty;
            Login = string.Empty;

            MessageBox.Show("Sign in Success...", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool LoginClickEnable(object obj)
        {
            return !(string.IsNullOrEmpty((obj as PasswordBox).Password) || string.IsNullOrEmpty(Login));
        }

        private void LoginClickExecute(object obj)
        {
            UserService.Reference.User.Password = (obj as PasswordBox).Password;
            UserService.Reference.User.Login = Login;

            if(UserService.Reference.LogIn())
            {
                // Logged In
                MessageBox.Show("Logged In...", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                WindowManager.Reference.CreateIboberWindow();
            }
            else
            {
                //Wrong Login Or Password
                MessageBox.Show("Wrong Login or Password...", "Info", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChangedHandler([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }     
        
    }
}
