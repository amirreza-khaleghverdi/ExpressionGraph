using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Page_Navigation_App.Model;
using Page_Navigation_App.Utilities;

namespace Page_Navigation_App.ViewModel
{
    class OrderVM : Utilities.ViewModelBase
    {

        private string _inputExpression;
        public string InputExpression
        {
            get => _inputExpression;
            set { _inputExpression = value; OnPropertyChanged(); }
        }

        private string _variableExpression;
        public string VariableExpression
        {
            get => _variableExpression;
            set { _variableExpression = value; OnPropertyChanged(); }
        }

        private string _valueExpression;
        public string ValueExpression
        {
            get => _valueExpression;
            set { _valueExpression = value; OnPropertyChanged(); }
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

        public OrderVM()
        {
            // Initialize the command
            CalculateCommand = new RelayCommand(o => Calculate(InputExpression));
        }

        public void Calculate(string input)
        {
            try
            {
                List<Tuple<string, double>> vars = new List<Tuple<string, double>>();
                string[] variables = VariableExpression.Split(",");
                string[] values = ValueExpression.Split(",");

                if(variables.Length==0 || values.Length==0)
                {
                    throw new Exception("it cant be null.");
                }

                if (variables.Length != values.Length)
                {
                    throw new Exception ("Number of variables and values do not match.");
                }

                int m = variables.Length;
                for (int i = 0; i < m; i++)
                {
                    variables[i] = variables[i].Trim();
                    values[i] = values[i].Trim();
                    string normalize = Preprocess.Normalize(values[i]);
                    List<string> tokenizee = Tokenizer.Tokenize(normalize);
                    expression_tree parser = new expression_tree(tokenizee);
                    node roott = parser.Parse();
                    double resultt = evaluator.Evaluate(roott);

                    vars.Add(Tuple.Create(variables[i], resultt));
                }

                string Normalize = Preprocess.Normalize(input);
                List<string> tokenize = Tokenizer.Tokenize(Normalize);
                expression_tree tree = new expression_tree(tokenize);
                node root = tree.Parse();
                CurrentTree = root;
                double result = evaluator.Evaluate(root, vars);
                ResultValue = $"Result: {result}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
