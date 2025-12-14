using Page_Navigation_App.Utilities;
using System.Windows.Input;
using System.Windows.Media;

public class WpfTask : ViewModelBase
{
    public string Name { get; set; }

    private int _priorityValue = 0;
    public int PriorityValue
    {
        get => _priorityValue;
        set { _priorityValue = value; OnPropertyChanged(); }
    }

    private string _currentPriority = "UNASSIGNED";
    public string CurrentPriority
    {
        get => _currentPriority;
        set { _currentPriority = value; OnPropertyChanged(); }
    }

    private Brush _priorityColor =
        (Brush)new BrushConverter().ConvertFrom("#B2BEC3");

    public Brush PriorityColor
    {
        get => _priorityColor;
        set { _priorityColor = value; OnPropertyChanged(); }
    }

    public ICommand SetPriorityCommand { get; }

    public WpfTask(Action<WpfTask, string> setPriority)
    {
        SetPriorityCommand = new RelayCommand(level =>
        {
            setPriority(this, level.ToString());
        });
    }
}

