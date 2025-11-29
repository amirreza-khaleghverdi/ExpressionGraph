namespace Expression_Builder;

public static class Expression_Builder
{
    static int Precedence(char op)
    {
        if (op == '^' || op == '√') return 3;
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



}
