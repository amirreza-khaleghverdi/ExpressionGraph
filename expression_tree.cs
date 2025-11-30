namespace Expression_Tree;

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

public class expression_tree
{
    List<string> tokens;
    int index;

    public expression_tree(List<string> tokens)
    {
        this.tokens = tokens;
        index = 0;
    }

    string Corrent() => index < tokens.Count ? tokens[index] : null;

    string Next()
    {
        string corrent = Corrent();
        index++;
        return corrent;
    }


    public node ParseAddSub()
    {
        node left = ParseMulDiv();

        while (Corrent() == "+" || Corrent() == "-")
        {
            string op = Next();
            node right = ParseMulDiv();
            left = new node(op, left, right);
        }

        return left;
    }


    private node ParseMulDiv()
    {
        node left = ParsePowRad();

        while (Corrent() == "*" || Corrent() == "/")
        {
            string op = Next();
            node right = ParsePowRad();
            left = new node(op, left, right);
        }

        return left;
    }

    private node ParsePowRad()
    {
        node left = ParsePrimary();

        while (Corrent() == "^" || Corrent() == "√")
        {
            string op = Next();
            node right = ParsePowRad();
            left = new node(op, left, right);
        }

        return left;
    }


    private node ParsePrimary()
    {
        string tok = Corrent();

        if (tok == "-")
        {
            Next();
            node child = ParsePrimary();
            return new node("u-", child, null);
        }

        if (tok == "(")
        {
            Next();
            node expr = ParseAddSub();
            Next();
            return expr;
        }

        Next();
        return new node(tok, null, null);
    }
}