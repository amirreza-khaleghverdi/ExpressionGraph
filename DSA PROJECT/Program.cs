
namespace DSA_PROJECT;

class Program
{
    static void Main(string[] args)
    {
        string normalstr = "";
        while (true)
        {
            try
            {
                Console.Write("Enter input: ");
                string input = Console.ReadLine();
                normalstr = Normalizer.Normalizer.Normalize(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ==> " + ex.Message);
                Console.WriteLine("Please Try Again.\n");
                // Thread.Sleep(1500);
                // Console.Clear();
            }
        }
        Console.WriteLine(normalstr);
    }
}

