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
using FaceComparator.Algorithm;

namespace FaceComparator
{
    /// <summary>
    /// Interaction logic for CriterionComparisonControl.xaml
    /// </summary>
    public partial class CriterionComparisonControl : UserControl
    {
        public CriterionComparisonControl()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var a =
            ((CriterionPreferenceMatrix) DataContext).ToString();

            Console.Out.WriteLine(a);
        }
    }
}
