using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Page_Navigation_App.Model;
using Page_Navigation_App.Utilities;

namespace Page_Navigation_App.ViewModel
{
    class TransactionVM : Utilities.ViewModelBase
    {
        private string _inputExpression;
        public string InputExpression
        {
            get => _inputExpression;
            set { _inputExpression = value; OnPropertyChanged(); }
        }

        private node _currentTree;
        public node CurrentTree
        {
            get => _currentTree;
            set { _currentTree = value; OnPropertyChanged(); }
        }

        private string _resultValue;
        public string ResultValue
        {
            get => _resultValue;
            set { _resultValue = value; OnPropertyChanged(); }
        }

        public ICommand CalculateCommand { get; }

        public ICommand SetPriorityCommand { get; }
        public ObservableCollection<WpfTask> Tasks { get; set; }

        private WpfTask _selectedTask;
        public WpfTask SelectedTask
        {
            get => _selectedTask;
            set { _selectedTask = value; OnPropertyChanged(); }
        }

        public TransactionVM()
        {
            Tasks = new ObservableCollection<WpfTask>
            {
                new WpfTask(UpdatePriority) { Name = "add sub" },
                new WpfTask(UpdatePriority) { Name = "mul div" },
                new WpfTask(UpdatePriority) { Name = "pow rad" },
            };

            CalculateCommand = new RelayCommand(_ => Calculate(InputExpression));
        }


        private void UpdatePriority(WpfTask task, string level)
        {
            (int value, string colorHex) = level switch
            {
                "HIGH" => (3, "#FF3B30"),
                "MEDIUM" => (2, "#FF9500"),
                "LOW" => (1, "#8E8E93"),
                _ => (0, "#B2BEC3")
            };

            task.PriorityValue = value;
            task.CurrentPriority = level;
            task.PriorityColor =
                (Brush)new BrushConverter().ConvertFrom(colorHex);
        }



        // Inside your Calculate method:
        public void Calculate(string input)
        {
            try
            {
                if (Tasks.Any(t => t.PriorityValue == 0))
                {
                    ResultValue = "Please set all priorities first";
                    return;
                }

                int addSubPriority =
                    Tasks.First(t => t.Name == "add sub").PriorityValue;

                int mulDivPriority =
                    Tasks.First(t => t.Name == "mul div").PriorityValue;

                int powRadPriority =
                    Tasks.First(t => t.Name == "pow rad").PriorityValue;

                string normalized_input = Preprocess.Normalize(input);
                List<string> tokenized_input = Tokenizer.Tokenize(normalized_input);

                expression_tree expression_tree = new expression_tree(tokenized_input);
                expression_tree.SetPrecedence(addSubPriority, mulDivPriority, powRadPriority);
                node root = expression_tree.Parse();
                CurrentTree = root;

                double result = evaluator.Evaluate(root);
                ResultValue = $"Result: {result}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}
