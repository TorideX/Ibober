using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Uber_MVVM.Model;
using Microsoft.Maps.MapControl.WPF;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Threading;

namespace Uber_MVVM.ModelView
{
    class IboberViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChangedHandler([CallerMemberName]string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public ApplicationIdCredentialsProvider ApiKey { get; set; } = new ApplicationIdCredentialsProvider("JrUI2l5mxe56vn3j7dnO~22ASD4srBWqSSkDRwSykOw~AiIpzB7xejfj5oQ-P5NPIGu5i7c5cnwSxQDIweHimFxwLnFL7HhTeB9WGLyFVvBF");

        #region MainPage Variables

        public Command StartCommand { get; set; }

        private bool _showPopup = false;
        public bool ShowPopup
        {
            get { return _showPopup; }
            set
            {
                _showPopup = value;
                PropertyChangedHandler();
            }
        }

        private Location _carLocation;
        public Location CarLocation
        {
            get { return _carLocation; }
            set
            {
                _carLocation = value;
                PropertyChangedHandler();
            }
        }

        DispatcherTimer timer = new DispatcherTimer();
        int counter = 0;

        #endregion

        #region MyProfile Variables

        public Command CancelCommand { get; set; }
        public Command SaveCommand { get; set; }

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
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                PropertyChangedHandler();
            }
        }

        #endregion

        public IboberViewModel()
        {            
            SetMyProfileVariables();
            StartCommand = new Command(StartCommandExecute);
        }

        private void StartCommandExecute(object obj)
        {
            counter = 0;
            CarLocation = RouteLocs.Locs[counter++];
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += FollowRoad;
            timer.Start();
                       
        }

        private void FollowRoad(object sender, EventArgs e)
        {
            if (counter < RouteLocs.Locs.Count)
                CarLocation = RouteLocs.Locs[counter++];
            else
            {
                timer.Stop();
                ShowPopup = true;
            }
            Thread.Sleep(50);
        }

        private void SetMyProfileVariables()
        {
            Name = UserService.Reference.User.Name;
            Surname = UserService.Reference.User.Surname;
            Phone = UserService.Reference.User.Phone;
            Login = UserService.Reference.User.Login;
            Password = UserService.Reference.User.Password;

            CancelCommand = new Command(CancelCommandExecute);
            SaveCommand = new Command(SaveCommandExecute, SaveCommandCanExecute);            
        }
        
        private void SaveCommandExecute(object obj)
        {
            UserClass user = new UserClass()
            {
                Name = Name,
                Surname = Surname,
                Phone = Phone,
                Login = Login,
                Password = Password
            };  //  Card qaldi.....
            UserService.Reference.Edit(user);
        }

        private bool SaveCommandCanExecute(object obj)
        {
            return !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Phone) || 
                string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password));
        }

        private void CancelCommandExecute(object obj)
        {
            Name = UserService.Reference.User.Name;
            Surname = UserService.Reference.User.Surname;
            Phone = UserService.Reference.User.Phone;
            Login = UserService.Reference.User.Login;
            Password = UserService.Reference.User.Password;
        }
    }
}