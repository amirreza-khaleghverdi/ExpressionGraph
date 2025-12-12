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
        private List<string> tokens;
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
                { "^", 3 },
                { "√", 3 }
            };
        }

        public void SetPrecedence(int addSub, int mulDiv, int powRad)
        {
            precedence["+"] = addSub;
            precedence["-"] = addSub;
            precedence["*"] = mulDiv;
            precedence["/"] = mulDiv;
            precedence["^"] = powRad;
            precedence["√"] = powRad;
        }

        public node Parse()
        {
            return ParseExpression(0);
        }
        private node ParseExpression(int minPrec)
        {
            node left = ParsePrimary();

            while (true)
            {
                string op = Current();

                if (op == null || !precedence.ContainsKey(op))
                    break;

                int prec = precedence[op];
                if (prec < minPrec)
                    break;
                Next();

                int nextMinPrec = IsRightAssociative(op) ? prec : prec + 1;

                node right = ParseExpression(nextMinPrec);

                left = new node(op, left, right);
            }

            return left;
        }
         
        private bool IsRightAssociative(string op)
        {
            return op == "^" || op == "√";
        }
        private node ParsePrimary()
        {
            string tok = Current();
            if (tok == null)
                throw new Exception("Unexpected end of expression");
            if (tok == "sin" || tok == "cos" || tok == "tan" || tok == "cot")
            {
                Next();
                if (Current() != "(") throw new Exception("Expected '('");
                Next();

                node arg = ParseExpression(0);

                if (Current() != ")") throw new Exception("Expected ')'");
                Next();

                return new node(tok, arg);
            }
            if (tok == "-")
            {
                Next();
                return new node("u-", ParsePrimary());
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
    }
}
