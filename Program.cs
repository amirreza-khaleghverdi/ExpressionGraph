using Expression_Tree;
using Evaluator;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Enter infix expression: ");
            string? infix = Console.ReadLine();


            List<string> tokens = Tokenize(infix);


            expression_tree parser = new expression_tree(tokens);
            node root = parser.ParseAddSub();


            double result = evaluator.Evaluate(root);


            Console.WriteLine($"\nResult = {result}");
        }

    }

    static List<string> Tokenize(string s)
    {
        return s.Replace("(", " ( ")
                .Replace(")", " ) ")
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .ToList();
    }
}

