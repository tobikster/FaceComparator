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
using Microsoft.Win32;

namespace FaceComparator
{
    /// <summary>
    /// Interaction logic for ProblemDefinitionControl.xaml
    /// </summary>
    public partial class ProblemDefinitionControl : UserControl
    {

        public ProblemDefinitionControl()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "Supported Media|*.bmp;*.gif;*.jpg;*.png;";
            openDialog.Multiselect = true;

                if (openDialog.ShowDialog().Value)
                {
                    foreach (var file in openDialog.FileNames)
                    {
                        var img = new BitmapImage(new Uri(file));
                        ((Problem) DataContext).AddDecision(img);
                    }
                }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var prompt = new TextPrompt("Nazwa kryterium", "Podaj nazwę nowego kryterium:");
            prompt.Owner = this.Parent as Window;
            var result = prompt.ShowDialog();
            if (result.HasValue && result.Value)
            {
                ((Problem) DataContext).AddCriterion(prompt.Result);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ((Problem) DataContext).RemoveDecision(DecisionList.SelectedItem as Decision);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ((Problem) DataContext).RemoveCriterion(CriterionList.SelectedItem as Criterion);
        }
    }
}
