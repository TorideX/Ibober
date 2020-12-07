using System;
using System.Windows;
using Uber_MVVM.Model;
using Uber_MVVM.ModelView;
using Uber_MVVM.View;

namespace Uber_MVVM
{
    public partial class App : Application
    {
        public App()
        {
            MyIoC.Reference.Register<UserService, IUserService>().
                Register<AutorisationView>().
                Register<AutorisationViewModel>().Build();

            var a = UserService.Reference;

        }
    }
}
