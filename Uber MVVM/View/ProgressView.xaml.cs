using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Uber_MVVM.View
{
    public partial class ProgressView : Window
    {
        public ProgressView()
        {
            InitializeComponent();
            Progress();
        }

        private void Progress()
        {
            Duration dur = new Duration(TimeSpan.FromSeconds(30));
            DoubleAnimation animation = new DoubleAnimation(100, dur);
            progressBar.BeginAnimation(ProgressBar.ValueProperty, animation);
            MessageBox.Show("Ibober Trip Finished!", "Ibober", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
