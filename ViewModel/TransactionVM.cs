using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        public TransactionVM()
        {
            // Initialize the command
            CalculateCommand = new RelayCommand(o => Calculate(InputExpression));
        }

        // Inside your Calculate method:
        public void Calculate(string input)
        {
            string normalized_input = Preprocess.Normalize(input);
            List<string> tokenized_input = Tokenizer.Tokenize(normalized_input);

            expression_tree expression_tree = new expression_tree(tokenized_input);
            node root = expression_tree.Parse();
            CurrentTree = root;

            double result = evaluator.Evaluate(root);
            ResultValue = $"Result: {result}";
        }
    }
}
