namespace Expression_Builder;

public class node
{
    public string Value { get; set; }
    public node Left { get; set; }
    public node Right { get; set; }

    public node(string value, node left, node right)
    {
        Value = value;
        Left = left;
        Right = right;
    }
}

public class Parser
{
    private List<string> tokens;
    private int index;

    public Parser(List<string> tokens)
    {
        this.tokens = tokens;
        this.index = 0;
    }

    private string Peek() => index < tokens.Count ? tokens[index] : null;

    private string Next()
    {
        string t = Peek();
        index++;
        return t;
    }

    // -----------------------------
    // Level 4: + and -
    // -----------------------------
    public node ParseExpression()
    {
        node left = ParseTerm();

        while (Peek() == "+" || Peek() == "-")
        {
            string op = Next();
            node right = ParseTerm();
            left = new node(op, left, right);
        }

        return left;
    }

    // -----------------------------
    // Level 3: * and /
    // -----------------------------
    private node ParseTerm()
    {
        node left = ParsePower();

        while (Peek() == "*" || Peek() == "/")
        {
            string op = Next();
            node right = ParsePower();
            left = new node(op, left, right);
        }

        return left;
    }

    // -----------------------------
    // Level 2: exponent and radical (binary, right associative)
    // -----------------------------
    private node ParsePower()
    {
        node left = ParsePrimary();

        while (Peek() == "^" || Peek() == "√")
        {
            string op = Next();
            node right = ParsePower();   // right associative
            left = new node(op, left, right);
        }

        return left;
    }

    // -----------------------------
    // Level 1: Parentheses, numbers, variables, unary minus
    // -----------------------------
    private node ParsePrimary()
    {
        string tok = Peek();

        // unary minus only
        if (tok == "-")
        {
            Next();
            node child = ParsePrimary();
            return new node("u-", child, null);
        }

        if (tok == "(")
        {
            Next();
            node expr = ParseExpression();
            Next(); // consume ')'
            return expr;
        }

        // number or variable
        Next();
        return new node(tok, null, null);
    }
}


public static class Evaluator
{
    public static double Evaluate(node root)
    {
        if (root == null)
            throw new Exception("Invalid expression tree");

        return EvaluatePostfix(root);
    }

    private static double EvaluatePostfix(node root)
    {
        if (root.Left == null && root.Right == null)
        {
            // leaf → number or variable (only number supported here)
            return double.Parse(root.Value);
        }

        // unary minus
        if (root.Value == "u-")
        {
            double val = EvaluatePostfix(root.Left);
            return -val;
        }

        double left = EvaluatePostfix(root.Left);
        double right = EvaluatePostfix(root.Right);

        switch (root.Value)
        {
            case "+": return left + right;
            case "-": return left - right;
            case "*": return left * right;
            case "/": return left / right;

            case "^": return Math.Pow(left, right);
            case "√": return Math.Pow(right, 1.0 / left);

            default:
                throw new Exception("Unknown operator: " + root.Value);
        }
    }
}