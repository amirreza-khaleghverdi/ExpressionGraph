using Expression_Tree;
using Evaluator;
using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("choose your option");
            Console.WriteLine("1.normal");
            Console.WriteLine("2.add variable");
            Console.WriteLine("3.change priority opertor ");

            int n = int.Parse(Console.ReadLine());

            if (n == 1)
            {
                Console.Write("Enter Input : ");
                string? infix = Console.ReadLine();

                string Normalize = Preprocess.Normalize(infix);
                List<string> tokenize = Tokenizer.Tokenize(Normalize);
                expression_tree parser = new expression_tree(tokenize);
                node root = parser.Parse();
                double result = evaluator.Evaluate(root);   
                Console.WriteLine($"\nResult = {result}");
            }
            if (n==2)
            {
                Console.WriteLine("Enter Count of variable: ");
                int m = int.Parse(Console.ReadLine());
                List<Tuple<string, double>> vars = new List<Tuple<string, double>>();
                for (int i = 0; i < m; i++)
                {
                    Console.Write("Enter name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter value: ");
                    double value = double.Parse(Console.ReadLine());

                    vars.Add(Tuple.Create(name, value));
                }
                Console.WriteLine("Now Enter your Input for calculate: ");
                string input = Console.ReadLine();
                string Normalize = Preprocess.Normalize(input);
                List<string> tokenize = Tokenizer.Tokenize(Normalize);
                expression_tree tree = new expression_tree(tokenize);
                node root = tree.Parse();
                double result = evaluator.Evaluate(root, vars);
                Console.WriteLine("Result = " + result);
            }
            if (n == 3)
            {

                Console.WriteLine("Enter the Priority from 1 to 3: ");

                Console.Write("Enter the Add_Sub: ");
                int add_sum = int.Parse(Console.ReadLine());

                Console.Write("Enter the Mul_Div: ");
                int mul_div = int.Parse(Console.ReadLine());

                Console.Write("Enter the Pow_Rad: ");
                int pow_rad = int.Parse(Console.ReadLine());

                Console.Write("Enter Input : ");
                string? infix = Console.ReadLine();

                string Normalize = Preprocess.Normalize(infix);
                List<string> tokenize = Tokenizer.Tokenize(Normalize);

                expression_tree parser = new expression_tree(tokenize);

                parser.SetPrecedence(add_sum, mul_div, pow_rad);
                node root = parser.Parse();   
                double result = evaluator.Evaluate(root);
                Console.WriteLine("Result = " + result);


            }
        }
    }
}
