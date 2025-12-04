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

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void GoPage1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow homepage = new();
            homepage.Show();
            this.Close();
        }

        private void GoPage2_Click(object sender, RoutedEventArgs e)
        {
            Window1 precedence = new();
            precedence.Show();
            this.Close();
        }

        private void GoPage3_Click(object sender, RoutedEventArgs e)
        {
            Window2 variable = new();
            variable.Show();
            this.Close();
        }

    }
}
