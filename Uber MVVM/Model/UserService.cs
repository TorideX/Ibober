using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uber_MVVM.Model
{
    public class UserService : IUserService
    {
        private static UserService _reference;
        public static UserService Reference
        {
            get
            {
                if (_reference == null)
                    _reference = new UserService();
                return _reference;
            }
            set => _reference = value;
        }

        public UserClass User { get; set; }

        private UserService()
        {
            User = new UserClass();
            if (!File.Exists("Users.json"))
                File.Create("Users.json");
        }

        public void Edit(UserClass user)
        {
            var str = File.ReadAllText("Users.json");
            List<UserClass> users = JsonConvert.DeserializeObject<List<UserClass>>(str);

            foreach (var item in users)
            {
                if (item.Equals(User))
                {
                    users[users.IndexOf(User)].Copy(user);
                    break;
                }

            }
            User.Copy(user);

            var ser = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText("Users.json", ser);
        }
            
        
        public void SignIn()
        {
            List<UserClass> users = null;
            var str = File.ReadAllText("Users.json");
            users = JsonConvert.DeserializeObject<List<UserClass>>(str);
            if (users != null)
            {
                users.Add(User);
            }
            else
            {
                users = new List<UserClass>();
                users.Add(User);
            }

            var ser = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText("Users.json", ser);
        }
        public bool LogIn()
        {
            var ser = File.ReadAllText("Users.json");
            List<UserClass> users = JsonConvert.DeserializeObject<List<UserClass>>(ser);

            bool isLogged = false;
            if (users != null)
            {
                foreach (var item in users)
                {
                    if (User.Login == item.Login.ToString() && User.Password == item.Password.ToString())
                    {
                        User.Copy(item as UserClass);  // ===>>  Deep Copy Lazimdi burda   <<=====
                        isLogged = true;
                        break;
                    }
                }
                if (isLogged == true)  // Logged In
                {
                    return true;
                }
                else  // Wrong Login or Password
                {
                    return false;
                }
            }
            else   // DataBase is empty...
            {
                return false;
            }
        }
    }
}
