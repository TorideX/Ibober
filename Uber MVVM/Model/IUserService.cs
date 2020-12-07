using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uber_MVVM.Model
{
    public interface IUserServiceSingleton
    {
        IUserService Reference { get; set; }
    }

    public interface IUserService
    {
        UserClass User { get; set; }
        void SignIn();
        bool LogIn();
    }
}
