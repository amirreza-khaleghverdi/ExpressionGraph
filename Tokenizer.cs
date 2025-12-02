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
            if (char.IsDigit(c) || c == '.')
            {
                StringBuilder num = new StringBuilder();
                num.Append(c);
                i++;
                bool hasDot = c == '.'; 
                while (i < input.Length && (char.IsDigit(input[i]) || (!hasDot && input[i] == '.')))
                {
                    if (input[i] == '.')
                        hasDot = true;
                    num.Append(input[i]);
                    i++;
                }
                tokens.Add(num.ToString());
                continue;
            }
            if (char.IsLetter(c))
            {
                StringBuilder func = new StringBuilder();
                while (i < input.Length && char.IsLetter(input[i]))
                {
                    func.Append(input[i]);
                    i++;
                }

                tokens.Add(func.ToString());
                continue;
            }
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
