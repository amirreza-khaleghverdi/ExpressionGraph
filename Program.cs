using Expression_Tree;
using Evaluator;
using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Choose Your Option");
            Console.WriteLine("1.Default Calculate ");
            Console.WriteLine("2.Add Variable");
            Console.WriteLine("3.Change Priority Operator");
            int n = int.Parse(Console.ReadLine());
            Thread.Sleep(100);
            Console.Clear();
            switch (n){
            case 1:
                {
                    try
                    {
                        Console.Write("Enter Input : ");
                        string? infix = Console.ReadLine();

                        string Normalize = simplification.Normalize(infix);
                        List<string> tokenize = Tokenizer.Tokenize(Normalize);
                        foreach (var x in tokenize) Console.Write(x + " ");
                        expression_tree parser = new expression_tree(tokenize);
                        node root = parser.Parse();
                        double result = evaluator.Evaluate(root);
                        Console.WriteLine("\nResult :" + result.ToString("F3"));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("==> " + ex.Message);
                    }
                }
                break;
            case 2:
                {
                    try
                    {
                        Console.WriteLine("Enter Count of variable: ");
                        int m = int.Parse(Console.ReadLine());
                        List<Tuple<string, double>> vars = new List<Tuple<string, double>>();
                        for (int i = 0; i < m; i++)
                        {
                            Console.Write("Enter name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter value: ");
                            string inputt = Console.ReadLine();
                            string normalize = simplification.Normalize(inputt);
                            List<string> tokenizee = Tokenizer.Tokenize(normalize);
                            expression_tree parser = new expression_tree(tokenizee);
                            node roott = parser.Parse();
                            double resultt = evaluator.Evaluate(roott);
                            double value = resultt;
                            vars.Add(Tuple.Create(name, value));
                        }
                        Console.WriteLine("Now Enter your Input for calculate: ");
                        string input = Console.ReadLine();
                        string Normalize = simplification.Normalize(input);
                        List<string> tokenize = Tokenizer.Tokenize(Normalize);
                        expression_tree tree = new expression_tree(tokenize);
                        node root = tree.Parse();
                        double result = evaluator.Evaluate(root, vars);
                            Console.WriteLine("\nResult :" + result.ToString("F3"));
                        }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" ==> " + ex.Message);
                    }
                }
                break;
            case 3:
                {
                    try
                    {
                        Console.WriteLine("Enter the Priority from 1 to 2:  2=MAX_Priority ...");
                        Console.Write("Enter the Add_Sub: "); //
                        int add_sum = int.Parse(Console.ReadLine());
                        Console.Write("Enter the Mul_Div: ");
                        int mul_div = int.Parse(Console.ReadLine()); //
                        Console.Write("Enter Input : ");
                        string? infix = Console.ReadLine();
                        string Normalize = simplification.Normalize(infix);
                        List<string> tokenize = Tokenizer.Tokenize(Normalize);
                        expression_tree parser = new expression_tree(tokenize);
                        parser.SetPrecedence(add_sum, mul_div);
                        node root = parser.Parse();
                        double result = evaluator.Evaluate(root);
                        Console.WriteLine("Result = " + result);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" ==> " + ex.Message);
                    }
                }
                break;
            }
        }
    }
}
