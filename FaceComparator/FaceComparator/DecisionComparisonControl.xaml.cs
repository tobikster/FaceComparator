using System;
using System.Windows;
using System.Windows.Controls;
using FaceComparator.Algorithm;

namespace FaceComparator
{
    /// <summary>
    /// Interaction logic for CriterionComparisonControl.xaml
    /// </summary>
    public partial class DecisionComparisonControl : UserControl
    {
        public DecisionComparisonControl()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var a =
                ((CriterionPreferenceMatrix) DataContext).ToString();

            Console.Out.WriteLine(a);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }

        private void PairGrid_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var pair = (sender as Grid).Tag as DecisionPair;
            this.Lightbox.DataContext = pair;
            this.Lightbox.Visibility = Visibility.Visible;
        }
    }
}