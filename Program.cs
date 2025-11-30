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
            string Normalize = Preprocess.Normalize(infix);
            List <string> tokenize = Tokenizer.Tokenize(Normalize);



            expression_tree parser = new expression_tree(tokenize);
            node root = parser.ParseAddSub();


            double result = evaluator.Evaluate(root);


            Console.WriteLine($"\nResult = {result}");
        }

    }
}

