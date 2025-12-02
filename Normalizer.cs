using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


public class Preprocess
{
    public static string Normalize(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new Exception ("input cannot be empty");
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
    static string RemoveSpaces(string s) => s.Replace(" ", "");
    static void ValidateCharacters(string s)
    {
        string valid = "0123456789+-*/()^√.";
        foreach (char c in s)
        {
            if (!valid.Contains(c))
                throw new Exception($"Wrong Character => '{c}'");
        }
    }
    static void invaliddivide(string s)
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
}
          
