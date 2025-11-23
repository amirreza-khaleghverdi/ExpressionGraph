

namespace Expression_Tree;

public class ExpressionTree
{
    private abstract class ExprNode
    {
        public abstract double Evaluate();
    }
    private class OperandNode : ExprNode
    {
        public double Value { get; set; }
        public OperandNode(double value) { Value = value; }
        public override double Evaluate() => Value;
    }
    private class OperatorNode : ExprNode
    {
        public char Operator { get; set; }
        public ExprNode Left { get; set; }
        public ExprNode Right { get; set; }

        public OperatorNode(char op, ExprNode left, ExprNode right)
        {
            Operator = op;
            Left = left;
            Right = right;
        }

        public override double Evaluate()
        {
            double leftVal = Left.Evaluate();
            double rightVal = Right.Evaluate();

            switch (Operator)
            {
                case '+': return leftVal + rightVal;
                case '-': return leftVal - rightVal;
                case '*': return leftVal * rightVal;
                case '/': return leftVal / rightVal;
                case '^': return Math.Pow(leftVal, rightVal);
                default: throw new Exception("Unknown operator");
            }

        }
    }
    private ExprNode root;
    public ExpressionTree(string expression)
    {
        root = BuildTree(expression);
    }
    public double Evaluate()
    {
        if (root == null) throw new Exception("Expression tree is empty");
        return root.Evaluate();
    }
    private ExprNode BuildTree(string expr)
    {
        Stack<ExprNode> values = new Stack<ExprNode>();
        Stack<char> ops = new Stack<char>();
        Dictionary<char, int> precedence = new Dictionary<char, int>()
        {
            { '^', 4 }, { '*', 3 }, { '/', 3 }, { '+', 2 }, { '-', 2 }
        };

        for (int i = 0; i < expr.Length; i++)
        {
            char c = expr[i];

            if (char.IsDigit(c))
            {
                string number = "";
                while (i < expr.Length && char.IsDigit(expr[i]))
                {
                    number += expr[i];
                    i++;
                }
                i--;
                values.Push(new OperandNode(double.Parse(number)));
            }
            else if (precedence.ContainsKey(c))
            {
                while (ops.Count > 0 && ops.Peek() != '(' &&
                        precedence[ops.Peek()] >= precedence[c])
                {
                    char op = ops.Pop();
                    ExprNode right = values.Pop();
                    ExprNode left = values.Pop();
                    values.Push(new OperatorNode(op, left, right));
                }
                ops.Push(c);
            }
            else if (c == '(')
            {
                ops.Push(c);
            }
            else if (c == ')')
            {
                while (ops.Peek() != '(')
                {
                    char op = ops.Pop();
                    ExprNode right = values.Pop();
                    ExprNode left = values.Pop();
                    values.Push(new OperatorNode(op, left, right));
                }
                ops.Pop();
            }
        }

        while (ops.Count > 0)
        {
            char op = ops.Pop();
            ExprNode right = values.Pop();
            ExprNode left = values.Pop();
            values.Push(new OperatorNode(op, left, right));
        }

        return values.Pop();
    }
}