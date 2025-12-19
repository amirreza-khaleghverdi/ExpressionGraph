using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<WpfTask> Tasks { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Tasks = new ObservableCollection<WpfTask>
            {
                new WpfTask { Name = "Fix Login Bug" },
                new WpfTask { Name = "Update User Profile" },
                new WpfTask { Name = "Prepare Presentation" }
            };
            TaskListBox.ItemsSource = Tasks;
        }

        private void SetHigh(object sender, RoutedEventArgs e) => UpdatePriority(sender, "HIGH", "#FF3B30");
        private void SetMed(object sender, RoutedEventArgs e) => UpdatePriority(sender, "MEDIUM", "#FF9500");
        private void SetLow(object sender, RoutedEventArgs e) => UpdatePriority(sender, "LOW", "#8E8E93");

        private void UpdatePriority(object sender, string level, string colorHex)
        {
            var task = (sender as Button).Tag as WpfTask;
            task.CurrentPriority = level;
            task.PriorityColor = (Brush)new BrushConverter().ConvertFrom(colorHex);
        }
    }

    public class WpfTask : INotifyPropertyChanged
    {
        public string Name { get; set; }

        private string _currentPriority = "UNASSIGNED";
        public string CurrentPriority
        {
            get => _currentPriority;
            set { _currentPriority = value; OnPropertyChanged("CurrentPriority"); }
        }

        private Brush _priorityColor = Brushes.Gray;
        public Brush PriorityColor
        {
            get => _priorityColor;
            set { _priorityColor = value; OnPropertyChanged("PriorityColor"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}