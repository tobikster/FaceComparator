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

namespace FaceComparator
{
    /// <summary>
    /// Interaction logic for Lightbox.xaml
    /// </summary>
    public partial class Lightbox : UserControl
    {
        public Lightbox()
        {
            InitializeComponent();
        }

        private void Grid_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
