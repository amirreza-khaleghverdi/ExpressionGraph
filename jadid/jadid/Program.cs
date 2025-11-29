using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Net.NetworkInformation;
using System.Xml.Schema;
using System.Diagnostics;
namespace DSPROJECT
{
    public static class Normalizer
    {
        public static string Normalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";
            input = RemoveSpaces(input);
            ValidateCharacters(input);
            input = fix_number_startwithzero(input);
            input = SimplifySigns(input);
            input = FixLeadingSigns(input);
            input = RemoveUnaryPlusInsideParentheses(input);
            input = Removeafter_div_or_multi(input);
            invaliddivide(input);
            invaliddivideandmulti(input);
            invalidPower_Square(input);
            //input = fix_if_Invaliddivide_or_multi(input);
            //input = fix_if__Invalidpower_or_square(input);
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
                throw new Exception($"Invalid Divide. First or last character. Try Again.");
            if (s[0] == '*' || s[s.Length - 1] == '*')
            {
                throw new Exception($"Invalid Multipliction. First or last character. Try Again.");
            }
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] == '/' && s[i + 1] == '0')
                    throw new Exception($"{s[i]}{s[i + 1]} Invalid Divide.Division by zero. Try Again.");
            }
        }
        private static void invaliddivideandmulti(string s)
        {
            for (int i = 1; i < s.Length - 1; i++)
            {
                if (s[i] == '*' || s[i] == '/')
                {
                    bool leftValid = char.IsDigit(s[i - 1]) || s[i - 1] == ')';
                    bool rightValid = char.IsDigit(s[i + 1]) || s[i + 1] == '(' || s[i + 1] == '-';

                    if (!leftValid || !rightValid)
                    {
                        throw new Exception("Invalid divide or multiplication. Try again.");
                    }
                }
            }
        }
        private static void invalidPower_Square(string s)
        {
            if (s[0] == '^' || s[s.Length - 1] == '^' || s[s.Length - 1] == '√')
            {
                throw new Exception("Invalid Power or Square =>First or Last charachter .Try again");
            }
            for (int i = 1; i < s.Length - 1; i++)
            {
                if (s[i] == '√')
                {
                    if (s[i + 1] == '-')
                    {
                        throw new Exception("Invalid square => minus under square.Try again");
                    }
                }
                if (s[i] == '^' || s[i] == '√')
                {
                    if (s[i + 1] == '+')
                    {
                        s.Remove(i + 1, 1);
                    }
                }
                if (s[i] == '+' || s[i] == '-')
                {
                    if (s[i + 1] == '^' || s[i + 1] == '√')
                    {
                        s.Remove(i, 1);
                    }
                }
            }
        }
        private static string fix_if_Invaliddivide_or_multi(string s)
        {
            if (s[0] == '*' || s[0] == '/')
            {
                s.Remove(0, 1);
            }
            if (s[s.Length - 1] == '*' || s[s.Length - 1] == '/')
            {
                s.Remove(s.Length - 1, 1);
            }
            return s;
        }
        private static string fix_if__Invalidpower_or_square(string s)
        {
            if (s[0] == '^' || s[0] == '√')
            {
                s.Remove(0, 1);
            }
            if (s[s.Length - 1] == '^' || s[s.Length - 1] == '√')
            {
                s.Remove(s.Length - 1, 1);
            }
            return s;
        }
        private static string fix_number_startwithzero(string s)
        {
            string numberPattern = @"\d+";
            return Regex.Replace(s, numberPattern, match =>
            {
                string number = match.Value;
                if (number.Length > 1 && number.StartsWith("0"))
                {
                    return number.TrimStart('0');
                }
                return number;
            });
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
        private static string Removeafter_div_or_multi(string x)
        {
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == '/' || x[i] == '*')
                {
                    if (x[i + 1] == '+')
                    {
                        x = x.Remove(i + 1, 1);
                    }
                }
            }
            return x;
        }
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
        public static string GetParenthesizedExpression(string expr)
        {
            return ParenthesizeExpression(expr);
        }
    }
    public static class Expression_Builder
    {
        static int Precedence(char op)
        {
            if (op == '√') return 4;       
            if (op == '^') return 3;    
            if (op == '*' || op == '/') return 2;
            if (op == '+' || op == '-') return 1;
            return 0;
        }
        public static string InToPost(string infix)
        {
            Stack<char> stack = new Stack<char>();
            string output = "";

            foreach (char c in infix)
            {
                if (char.IsLetterOrDigit(c))
                {
                    output += c;
                }
                else if (c == '(')
                {
                    stack.Push(c);
                }
                else if (c == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                        output += stack.Pop();
                    stack.Pop(); 
                }
                else
                {
                    while (stack.Count > 0 && stack.Peek() != '(' &&
                           Precedence(stack.Peek()) >= Precedence(c))
                    {
                        output += stack.Pop();
                    }
                    stack.Push(c);
                }
            }

            while (stack.Count > 0)
                output += stack.Pop();

            return output;
        }
        public static double Evaluate(string postfix)
        {
            Stack<double> stack = new Stack<double>();

            foreach (char c in postfix)
            {
                if (char.IsDigit(c))
                {
                    stack.Push(c - '0');
                }
                else if (c == '√')
                {
                    double a = stack.Pop();
                    stack.Push(Math.Sqrt(a));
                }
                else
                {
                    double b = stack.Pop();
                    double a = stack.Pop();

                    switch (c)
                    {
                        case '+': stack.Push(a + b); break;
                        case '-': stack.Push(a - b); break;
                        case '*': stack.Push(a * b); break;
                        case '/': stack.Push(a / b); break;
                        case '^': stack.Push(Math.Pow(a, b)); break;
                    }
                }
            }
            return stack.Pop();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter input: ");
                    string? input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                        continue;
                    string normalized = Normalizer.Normalize(input);
                   // string parenthesized = Normalizer.ParenthesizeExpression(normalized);
                    Console.WriteLine(normalized);
                    string parenthesized = Normalizer.GetParenthesizedExpression(normalized);
                    Console.WriteLine(parenthesized);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error ==> " + ex.Message);
                    Console.WriteLine("Please Try Again.\n");
                }
            }
        }
    }

}