using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ExpressionParser
{
    public enum TokenType
    {
        Number,
        Variable,
        Plus,
        Minus,
        Multiply,
        Divide,
        Power,
        Radical,
        LParen,
        RParen
    }
    public class Token
    {
        public TokenType Type;
        public string Value;

        public Token(TokenType type, string value = "")
        {
            Type = type;
            Value = value;
        }

        public override string ToString() => $"{Type}({Value})";
    }
    public class Node
    {
        public string Value;
        public Node Left;
        public Node Right;

        public Node(string value)
        {
            Value = value;
        }
    }
    public class ExpressionEvaluator
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
            string valid = "0123456789+-*/()^√.";
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
        public List<Token> Tokenize(string input)
        {
            List<Token> tokens = new List<Token>();
            int i = 0;

            while (i < input.Length)
            {
                char c = input[i];
                if (char.IsDigit(c))
                {
                    if (c == '0' && i + 1 < input.Length && char.IsDigit(input[i + 1]))
                        throw new Exception("Wrong number.");

                    StringBuilder num = new StringBuilder();
                    num.Append(c);
                    i++;

                    while (i < input.Length && char.IsDigit(input[i]))
                    {
                        num.Append(input[i]);
                        i++;
                    }

                    tokens.Add(new Token(TokenType.Number, num.ToString()));
                    continue;
                }

                // Variables
                if (char.IsLetter(c))
                {
                    StringBuilder var = new StringBuilder();
                    var.Append(c);
                    i++;

                    while (i < input.Length && (char.IsLetterOrDigit(input[i]) || input[i] == '_'))
                    {
                        var.Append(input[i]);
                        i++;
                    }

                    // امتیازی: نام متغیر باید با حرف شروع شود
                    if (!char.IsLetter(var[0]))
                        throw new Exception("نام متغیر باید با حرف شروع شود.");

                    tokens.Add(new Token(TokenType.Variable, var.ToString()));
                    continue;
                }

                // Operators & Parentheses
                switch (c)
                {
                    case '+': tokens.Add(new Token(TokenType.Plus)); break;
                    case '-': tokens.Add(new Token(TokenType.Minus)); break;
                    case '*': tokens.Add(new Token(TokenType.Multiply)); break;
                    case '/': tokens.Add(new Token(TokenType.Divide)); break;
                    case '^': tokens.Add(new Token(TokenType.Power)); break;
                    case '√': tokens.Add(new Token(TokenType.Radical)); break;
                    case '(': tokens.Add(new Token(TokenType.LParen)); break;
                    case ')': tokens.Add(new Token(TokenType.RParen)); break;
                    default:
                        throw new Exception($"Wrong character: {c}");
                }
                i++;
            }
            return tokens;
        }
        private int Precedence(TokenType t)
        {
            switch (t)
            {
                case TokenType.Radical: return 4;
                case TokenType.Power: return 3;
                case TokenType.Multiply:
                case TokenType.Divide: return 2;
                case TokenType.Plus:
                case TokenType.Minus: return 1;
                default: return -1;
            }
        }

        public Node BuildTree(List<Token> tokens)
        {
            Stack<Node> values = new Stack<Node>();
            Stack<Token> ops = new Stack<Token>();

            void ApplyOperator()
            {
                var op = ops.Pop();
                Node right = values.Pop();
                Node left = op.Type == TokenType.Radical ? null : values.Pop();

                Node node = new Node(op.Type.ToString())
                {
                    Left = left,
                    Right = right
                };

                values.Push(node);
            }

            foreach (var t in tokens)
            {
                if (t.Type == TokenType.Number || t.Type == TokenType.Variable)
                {
                    values.Push(new Node(t.Value));
                }
                else if (t.Type == TokenType.LParen)
                {
                    ops.Push(t);
                }
                else if (t.Type == TokenType.RParen)
                {
                    while (ops.Peek().Type != TokenType.LParen)
                        ApplyOperator();
                    ops.Pop();
                }
                else
                {
                    while (ops.Count > 0 && Precedence(ops.Peek().Type) >= Precedence(t.Type))
                        ApplyOperator();

                    ops.Push(t);
                }
            }

            while (ops.Count > 0)
                ApplyOperator();

            return values.Pop();
        }
        public double Evaluate(Node node)
        {
            if (double.TryParse(node.Value, out double n))
                return n;

            double left = node.Left != null ? Evaluate(node.Left) : 0;
            double right = Evaluate(node.Right);

            switch (node.Value)
            {
                case "Plus": return left + right;
                case "Minus": return left - right;
                case "Multiply": return left * right;
                case "Divide":
                    if (right == 0)
                        throw new DivideByZeroException("Division by zero. ");
                    return left / right;
                case "Power": return Math.Pow(left, right);
                case "Radical": return Math.Sqrt(right);
                default:
                    throw new Exception("Node Unknown: " + node.Value);
            }
        }
        public double Run(string input)
        {
            string norm = Normalize(input);
            var tokens = Tokenize(norm);
            var tree = BuildTree(tokens);
            return Evaluate(tree);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                ExpressionEvaluator engine = new ExpressionEvaluator();
                Console.WriteLine("Enter Input:");
                string input = Console.ReadLine();
                try
                {
                    double result = engine.Run(input);
                    Console.WriteLine("result: " + result);
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("new Error : " + ex.Message);
                }
            }
        }
    }
}
