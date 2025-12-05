using Evaluator;
using Expression_Tree;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Views;

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

    }

    void DrawTree(node root)
    {
        TreeCanvas.Children.Clear();
        DrawNode(root, 220, 20, 80, 80);
    }

    void DrawNode(node n, double x, double y, double x_offset, double y_offset)
    {
        if (n == null) return;

        Ellipse circle = new Ellipse
        {
            Width = 35,
            Height = 35,
            Fill = Brushes.WhiteSmoke,
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };
        Canvas.SetLeft(circle, x - circle.Width/2);
        Canvas.SetTop(circle, y - circle.Height/2);
        TreeCanvas.Children.Add(circle);


        TextBlock text = new TextBlock
        {
            Text = n.Value,
            FontWeight = FontWeights.Bold,
            FontSize = 16
        };
        Canvas.SetLeft(text, x - 7);
        Canvas.SetTop(text, y - 10);
        TreeCanvas.Children.Add(text);


        if (n.Left != null)
        {
            Line line = new Line
            {
                X1 = x,
                Y1 = y + circle.Height/2,
                X2 = x - x_offset,
                Y2 = y + y_offset,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            TreeCanvas.Children.Add(line);

            DrawNode(n.Left, x - x_offset, y + y_offset, x_offset * 0.7, y_offset);
        }


        if (n.Right != null)
        {
            Line line = new Line
            {
                X1 = x,
                Y1 = y + circle.Height/2,
                X2 = x + x_offset,
                Y2 = y + y_offset,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            TreeCanvas.Children.Add(line);

            DrawNode(n.Right, x + x_offset, y + y_offset, x_offset * 0.7, y_offset);
        }
    }

    private void Result_Button(object sender, RoutedEventArgs e)
    {
        calculate_result(InputBox.Text);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Button button = sender as Button;
        string text = Cal_Text.Text;

        try
        {
            switch (button.Content.ToString())
            {
                case "=":
                    calculate_result(Cal_Text.Text);
                    break;

                case "C":
                    Cal_Text.Text = "";
                    break;

                case "Del":
                    text = text.Remove(text.Length - 1);
                    Cal_Text.Text = text;
                    break;

                default:
                    Cal_Text.Text += button.Content.ToString();
                    break;
            }
        }
        catch (Exception ex)
        {
            InputBox.Text = "error " + ex.Message;
        }
    }

    private void calculate_result(string input)
    {
        try
        {
            string normalized = Preprocess.Normalize(input);
            List<string> tokenized = Tokenizer.Tokenize(normalized);

            expression_tree Tree = new expression_tree(tokenized);
            node root = Tree.ParseAddSub();
            DrawTree(root);

            double result = evaluator.Evaluate(root);
            Cal_Text.Text = Convert.ToString(result);
        }
        catch (Exception ex)
        {
            InputBox.Text = "error " + ex.Message;
        }

    }

    private void GoPage1_Click(object sender, RoutedEventArgs e)
    {
        MainWindow homepage = new();
        homepage.Show();
        this.Close();
    }

    private void GoPage2_Click(object sender, RoutedEventArgs e)
    {
        Window1 precedence = new();
        precedence.Show();
        this.Close();
    }

    private void GoPage3_Click(object sender, RoutedEventArgs e)
    {
        Window2 variable = new();
        variable.Show();
        this.Close();
    }
}