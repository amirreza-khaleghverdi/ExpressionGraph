using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
public class Tokenizer
{
    public static List<string> Tokenize(string input)
    {
        input = Preprocess(input);
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

                while (i < input.Length &&
                      (char.IsDigit(input[i]) || (!hasDot && input[i] == '.')))
                {
                    if (input[i] == '.') hasDot = true;
                    num.Append(input[i]);
                    i++;
                }

                tokens.Add(num.ToString());
                continue;
            }
            if (char.IsLetter(c)||c=='_')
            {
                StringBuilder text = new StringBuilder();

                while (i < input.Length && (char.IsLetterOrDigit(input[i]) || input[i] == '_'))
                {
                    text.Append(input[i]);
                    i++;
                }
                tokens.Add(text.ToString());
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
                case ',': tokens.Add(","); break;

                default:
                    throw new Exception($"Wrong character: {c}");
            }

            i++;
        }
        return tokens;
    }
    private static string Preprocess(string input)
    { 
        input = Regex.Replace(
            input,
            @"(\d+)log\(",
            m => $"log({m.Groups[1].Value},"
        );
        input = Regex.Replace(
            input,
            @"(sin|cos|tan|cot)(\d+)",
            m => $"{m.Groups[1].Value}({m.Groups[2].Value})"
        );
        input = Regex.Replace(
            input,
            @"(sin|cos|tan|cot)([a-zA-Z])",
            m => $"{m.Groups[1].Value}({m.Groups[2].Value})"
        );
        return input;
    }
}
