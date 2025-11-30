using System;
using System.Collections.Generic;
using System.Text;

public class Tokenizer
{
    public static List<string> Tokenize(string input)
    {
        List<string> tokens = new List<string>();
        int i = 0;

        while (i < input.Length)
        {
            char c = input[i];

            // Numbers
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

                tokens.Add(num.ToString()); 
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

                if (!char.IsLetter(var[0]))
                    throw new Exception("Variable must start with a letter.");

                tokens.Add(var.ToString()); // <-- add as string
                continue;
            }

            // Operators & parentheses
            switch (c)
            {
                case '+': tokens.Add("+"); break;
                case '-': tokens.Add("-"); break;
                case '*': tokens.Add("*"); break;
                case '/': tokens.Add("/"); break;
                case '^': tokens.Add("^"); break;
                case '√': tokens.Add("√"); break;
                case '(': tokens.Add("("); break;
                case ')': tokens.Add(")"); break;
                default:
                    throw new Exception($"Wrong character: {c}");
            }
            i++;
        }

        return tokens;
    }
}
