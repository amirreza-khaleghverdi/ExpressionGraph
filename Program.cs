using Expression_Builder;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Enter infix expression: ");
            string? infix = Console.ReadLine();

            // STEP 1 — Tokenize (your teammate will replace this)
            List<string> tokens = Tokenize(infix);

            // STEP 2 — Parse into expression tree
            Parser parser = new Parser(tokens);
            node root = parser.ParseExpression();

            // STEP 3 — Evaluate the expression (postfix tree evaluation)
            double result = Evaluator.Evaluate(root);

            // STEP 4 — Show results
            Console.WriteLine("\nExpression Tree:");
            PrintTree(root, "");

            Console.WriteLine($"\nResult = {result}");
        }

    }

    static void PrintTree(node n, string indent)
    {
        if (n == null) return;

        Console.WriteLine(indent + n.Value);

        PrintTree(n.Left, indent + "  ");
        PrintTree(n.Right, indent + "  ");
    }

    static List<string> Tokenize(string s)
    {
        return s.Replace("(", " ( ")
                .Replace(")", " ) ")
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .ToList();
    }
}

