class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            try
            {
                Console.Write("enter input: ");
                string? input = Console.ReadLine();
                string postfix = Expression_Builder.Expression_Builder.InToPost(input);
                double result = Expression_Builder.Expression_Builder.evalute(postfix);
                Console.WriteLine($"the final result is {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ==> " + ex.Message);
                Console.WriteLine("Please Try Again.\n");
            }
        }
    }
}

