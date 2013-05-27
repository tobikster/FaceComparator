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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var mat = new PreferenceMatrix(3);

            mat.SetValue(0, 2, 7);
            mat.SetValue(1,2, 3);

            Console.Out.WriteLine(mat);
            var a = mat.GetPreferenceVector();
        }
    }
}
