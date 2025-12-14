using Expression_Tree;
using System.Reflection.Metadata.Ecma335;

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

            return variable != null ? variable.Item2 : 0; //value of type of nonename va taghir name
        }
        if (root.Value == "u-") //unary minus
            return -EvaluatePostfix(root.Left, vars);
        if (root.Value == "sin" || root.Value == "cos" ||
            root.Value == "tan" || root.Value == "cot")
        {
            double arg = EvaluatePostfix(root.Left, vars);
            arg = (arg / 180.0) * Math.PI;
            double val = root.Value switch
            {
                "sin" => Math.Round(Math.Sin(arg), 10),
                "cos" => Math.Round(Math.Cos(arg), 10),
                "tan" => Math.Round(Math.Tan(arg), 10),
                "cot" => Math.Round(1 / Math.Tan(arg), 10),
                _ => throw new Exception("Unknown function")
            };
            if (Math.Abs(val) < 1e-10)
            {
                val = 0;
            }
            if (Math.Abs(val - 0.5) < 1e-10)
            {
                val = 0.5;
            }
            if (Math.Abs(val + 0.5) < 1e-10)
            {
                val = -0.5;
            }
            if (Math.Abs(val - 1) < 1e-10)
            {
                val = 1;
            }
            if (Math.Abs(val + 1) < 1e-10)
            {
                val = -1;
            }
            return val;
        }
        if (root.Value == "log")
        {
            double Base = EvaluatePostfix(root.Left, vars);
            double  INN= EvaluatePostfix(root.Right, vars);
            if (INN <= 0 || Base<=1)
            {
                throw new Exception("minus or valid base and value");
            }
            return Math.Log(INN)/Math.Log(Base);
        }
        double left = EvaluatePostfix(root.Left, vars);
        double right = EvaluatePostfix(root.Right, vars);
        if (root.Value == "/")
        {
            if (Math.Abs(right) < 1e-10)
                throw new DivideByZeroException("Error ==> Division By Zero!");
            return left / right;
        }
        if (root.Value == "√")  
        {
            if (right<0)
            {
                throw new Exception("Error ==> Minus Under Radikal!");
            }
            return Math.Pow(right, 1.0 / left);
        }
        return root.Value switch
        {
            "+" => left + right,
            "-" => left - right,
            "*" => left * right,
            "^" => Math.Pow(left, right),
            _ => throw new Exception("Unknown operator: " + root.Value)
        };
    }
}
