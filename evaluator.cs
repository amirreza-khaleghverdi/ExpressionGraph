using Expression_Tree;

namespace Evaluator;
public static class evaluator
{
    public static double Evaluate(node root)
    {
        return Evaluate(root, new List<Tuple<string, double>>());
    }
    public static double Evaluate(node root, List<Tuple<string, double>> vars)
    {
        if (root == null)
            throw new Exception("Invalid expression tree");

        return EvaluatePostfix(root, vars);
    }
    private static double EvaluatePostfix(node root, List<Tuple<string, double>> vars)
    {
        if (root.Left == null && root.Right == null)
        {
            if (double.TryParse(root.Value, out double num))
                return num;

            var variable = vars.FirstOrDefault(v => v.Item1 == root.Value);

            return variable != null ? variable.Item2 : 0;
        }

        if (root.Value == "u-")
            return -EvaluatePostfix(root.Left, vars);

        if (root.Value == "sin" || root.Value == "cos" ||
            root.Value == "tan" || root.Value == "cot")
        {
            double arg = EvaluatePostfix(root.Left, vars);

            return root.Value switch
            {
                "sin" => Math.Sin(arg),
                "cos" => Math.Cos(arg),
                "tan" => Math.Tan(arg),
                "cot" => 1 / Math.Tan(arg),
                _ => throw new Exception("Unknown function")
            };
        }
        double left = EvaluatePostfix(root.Left, vars);
        double right = EvaluatePostfix(root.Right, vars);
        return root.Value switch
        {
            "+" => left + right,
            "-" => left - right,
            "*" => left * right,
            "/" => left / right,
            "^" => Math.Pow(left, right),
            "√" => Math.Pow(right, 1.0 / left),
            _ => throw new Exception("Unknown operator: " + root.Value)
        };
    }
}
