using System;
using System.Collections.Generic;

namespace Expression_Tree
{
    public class node
    {
        public string Value { get; set; }
        public node Left { get; set; }
        public node Right { get; set; }

        public node(string value, node left = null, node right = null)
        {
            Value = value;
            Left = left;
            Right = right;
        }
     }

    public class expression_tree
    {
        private readonly List<string> tokens;
        private int index;
        private Dictionary<string, int> precedence;

        public expression_tree(List<string> tokens)
        {
            this.tokens = tokens;
            index = 0;
            SetDefaultPrecedence();
        }
        private string Current() =>
            index < tokens.Count ? tokens[index] : null;

        private string Next()
        {
            string t = Current();
            index++;
            return t;
        }
        public void SetDefaultPrecedence()
        {
            precedence = new Dictionary<string, int>
            {
                { "+", 1 },
                { "-", 1 },
                { "*", 2 },
                { "/", 2 },
                { "^", 1000 },
                { "√", 1000 }
            };
        }
        public void SetPrecedence(int addSub, int mulDiv)
        {
            precedence["+"] = addSub;
            precedence["-"] = addSub;
            precedence["*"] = mulDiv;
            precedence["/"] = mulDiv;
        }

        public node Parse() => ParseExpression(0);
        
        private node ParseUnary()
        {
            string tok = Current();

            if (tok == null)
                throw new Exception("Unexpected end of expression");
            if (tok == "-")
            {
                Next();
                return new node("u-", ParseUnary());
            }
            if (tok == "sin" || tok == "cos" || tok == "tan" ||
                tok == "cot" || tok == "log")
            {
                string funcName = tok;
                Next();

                if (Current() != "(") throw new Exception("Expected '(' after " + funcName);
                Next();

                if (funcName == "log")
                {
                    node valueNode = ParseExpression(0);
                    if (Current() == ",")
                    {
                        Next();
                        node baseNode = ParseExpression(0);
                        if (Current() != ")") throw new Exception("Expected ')'");
                        Next();
                        return new node("log", baseNode, valueNode);
                    }
                    else
                    {
                        if (Current() != ")") throw new Exception("Expected ')'");
                        Next();
                        return new node("log", new node("10"), valueNode);
                    }
                }

                node arg = ParseExpression(0);
                if (Current() != ")") throw new Exception("Expected ')'");
                Next();
                return new node(funcName, arg);
            }
            if (tok == "(")
            {
                Next();
                node exp = ParseExpression(0);
                if (Current() != ")") throw new Exception("Expected ')'");
                Next();
                return exp;
            }
            Next();
            return new node(tok);
        }
        private node ParseExpression(int minPrec)
        {
            node left = ParseUnary();

            while (true)
            {
                string op = Current();
                if (op == null || !precedence.ContainsKey(op))
                    break;

                int prec = precedence[op];
                if (prec < minPrec)
                    break;

                Next(); 

                int nextMin = IsRightAssociative(op) ? prec : prec + 1;

                node right = ParseExpression(nextMin);

                left = new node(op, left, right);
            }

            return left;
        }
        private bool IsRightAssociative(string op)
        {
            return op == "^" || op == "√";
        }
    }
}
