using Expression_Tree;

namespace Evaluator;

public static class evaluator
{
    public static double Evaluate(node root)
    {
        if (root == null)
            throw new Exception("Invalid expression tree");

        return EvaluatePostfix(root);
    }

    static double EvaluatePostfix(node root)
    {
        if (root.Left == null && root.Right == null)
        {
            return double.Parse(root.Value);
        }

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