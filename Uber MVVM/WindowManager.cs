using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uber_MVVM.Model;
using Uber_MVVM.View;

namespace Uber_MVVM
{
    class WindowManager
    {
        private static WindowManager _reference;
        public static WindowManager Reference {
            set => _reference = value;
            get
            {
                if (_reference == null)
                    _reference = new WindowManager();
                return _reference;
            }
        } 

        private WindowManager() { }

        public AutorisationView Autorisation { get; set; }
        public void CreateIboberWindow()
        {
            IboberView view = new IboberView();
            view.Show();
            Autorisation.Close();
        }
    }
}