using System.Text;
using System.Text.RegularExpressions;

namespace Normalizer;

public class Normalizer
{
    public static string Normalize(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "";
        //afsd
        input = RemoveSpaces(input);
        ValidateCharacters(input);
        Validinputnumber(input);
        input = SimplifySigns(input);
        input = FixLeadingSigns(input);
        input = RemoveUnaryPlusInsideParentheses(input);
        invaliddivide(input);
        input = ParenthesizeExpression(input);

        return input;
    }

    private static string RemoveSpaces(string s) => s.Replace(" ", "");

    private static void ValidateCharacters(string s)
    {
        string valid = "0123456789+-*/()^√";
        foreach (char c in s)
        {
            if (!valid.Contains(c))
                throw new Exception($"Wrong Character => '{c}'");
        }
    }

    private static void invaliddivide(string s)
    {
        if (s[0] == '/' || s[s.Length - 1] == '/')
            throw new Exception($"Invalid Divide. Try Again.");
        for (int i = 0; i < s.Length - 1; i++)
        {
            if (s[i] == '/' && s[i + 1] == '0')
                throw new Exception($"{s[i]}{s[i + 1]} Invalid Divide. Try Again.");
        }
    }

    private static void Validinputnumber(string s)
    {
        string numberPattern = @"\d+";
        foreach (Match match in Regex.Matches(s, numberPattern))
        {
            string number = match.Value;
            if (number != "0" && number.StartsWith("0"))
                throw new Exception($"{number} Invalid Number. Try Again.");
        }
    }

    private static string SimplifySigns(string s)
    {
        var result = new StringBuilder();
        int i = 0;
        while (i < s.Length)
        {
            if (s[i] == '+' || s[i] == '-')
            {
                int minusCount = 0;
                int j = i;
                while (j < s.Length && (s[j] == '+' || s[j] == '-'))
                {
                    if (s[j] == '-') minusCount++;
                    j++;
                }
                result.Append(minusCount % 2 == 0 ? '+' : '-');
                i = j;
            }
            else
            {
                result.Append(s[i]);
                i++;
            }
        }
        return result.ToString();
    }

    private static string FixLeadingSigns(string s)
    {
        if (s.Length > 1 && s[0] == '+') return s.Substring(1);
        return s;
    }

    private static string RemoveUnaryPlusInsideParentheses(string s)
    {
        var result = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(' && i + 1 < s.Length && s[i + 1] == '+')
            {
                result.Append('(');
                i++;
            }
            else
            {
                result.Append(s[i]);
            }
        }
        return result.ToString();
    }
    //parantes
    private static string ParenthesizeExpression(string expr)
    {
        Dictionary<char, int> precedence = new Dictionary<char, int>()
    {
    { '^', 3 },
    { '√', 3 },
    { '*', 2 },
    { '/', 2 },
    { '+', 1 },
    { '-', 1 }
    };

        Stack<string> values = new Stack<string>();
        Stack<char> ops = new Stack<char>();
        for (int i = 0; i < expr.Length; i++)
        {
            char c = expr[i];

            if (char.IsDigit(c))
            {
                string number = "";
                while (i < expr.Length && char.IsDigit(expr[i]))
                {
                    number += expr[i];
                    i++;
                }
                i--;
                values.Push(number);
            }
            else if (precedence.ContainsKey(c))
            {
                while (ops.Count > 0 && ops.Peek() != '(' &&
                        precedence[ops.Peek()] >= precedence[c])
                {
                    string val2 = values.Pop();
                    string val1 = values.Pop();
                    char op = ops.Pop();
                    values.Push($"({val1}{op}{val2})");
                }
                ops.Push(c);
            }
            else if (c == '(')
            {
                ops.Push(c);
            }
            else if (c == ')')
            {
                while (ops.Peek() != '(')
                {
                    string val2 = values.Pop();
                    string val1 = values.Pop();
                    char op = ops.Pop();
                    values.Push($"({val1}{op}{val2})");
                }
                ops.Pop();
            }
        }
        while (ops.Count > 0)
        {
            string val2 = values.Pop();
            string val1 = values.Pop();
            char op = ops.Pop();
            values.Push($"({val1}{op}{val2})");
        }
        return values.Pop();
    }
    private static int Precedence(char op)
    {
        if (op == '*' || op == '/') return 2;
        if (op == '+' || op == '-') return 1;
        return 0;
    }
}