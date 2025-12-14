using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
public class simplification
{
    public static string Normalize(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "";
        input = RemoveSpaces(input);
        ValidateCharacters(input);
        input = fix_number_startwithzero(input);
        input = simplesigns(input);
        input = FixLeadingSigns(input);
        input = RemoveUnaryPlusInsideParentheses(input);
        input = Removeafter_div_or_multi(input);
        invaliddivide(input);
        return input;
    }
    static string RemoveSpaces(string s) => s.Replace(" ", "");
    static void ValidateCharacters(string s)
    {
        string valid = "0123456789+-*/()^√.,abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_";
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
                throw new Exception($"Error ==> Division By Zero.");
        }
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
    private static string simplesigns(string s)
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

