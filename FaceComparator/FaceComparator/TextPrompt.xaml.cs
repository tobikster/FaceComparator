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
using System.Windows.Shapes;

namespace FaceComparator
{
    /// <summary>
    /// Interaction logic for TextPrompt.xaml
    /// </summary>
    public partial class TextPrompt : Window
    {
        public TextPrompt(string title, string prompt)
        {
            InitializeComponent();

            this.Title = title;
            this.prompt.Content = prompt;
        }

        public string Result { get; set; }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && textBox.Text.Length != 0)
            {
                DialogResult = true;
                this.Close();
            }
        }
    }
}
