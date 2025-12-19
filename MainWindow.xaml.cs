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

namespace Page_Navigation_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer player = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            player.Close();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                player.Open(new Uri("C418-Aria-Math-192.mp3", UriKind.Relative));

                player.MediaOpened += (s, ev) =>
                {
                    player.Position = TimeSpan.FromSeconds(158); 
                    player.Play();
                };


                player.MediaEnded += (s, ev) =>
                {
                    player.Position = TimeSpan.FromSeconds(158); 
                    player.Play();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed in playing music: " + ex.Message);
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                this.DragMove();
            }
        }
    }
}
