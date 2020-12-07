using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uber_MVVM.Model
{
    public class UserClass
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public CardClass Card { get; set; }

        public UserClass()
        {
            Card = new CardClass();
        }
        public override bool Equals(object obj)
        {
            var t = obj as UserClass;
            if (t == null)
                return false;
            return Name == t.Name && Surname == t.Surname &&
                Phone == t.Phone && Login == t.Login &&
                Password == t.Password; // Card....
        }

        public void Copy(UserClass user)
        {
            Name = string.Copy(user.Name);
            Surname = string.Copy(user.Surname);
            Phone = string.Copy(user.Phone);
            Login = string.Copy(user.Login);
            Password = string.Copy(user.Password);

            if (user.Card != null)
                Card = user.Card;
        }
    }
}
