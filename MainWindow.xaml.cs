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

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();


        var tokens = new List<string> { "3", "+", "4", "*", "2" };
        expression_tree parser = new expression_tree(tokens);

        node root = parser.ParseAddSub();

        DrawTree(root);
    }

    void DrawTree(node root)
    {
        TreeCanvas.Children.Clear();
        DrawNode(root, 220, 20, 60);
    }

    void DrawNode(node n, double x, double y, double offset)
    {
        if (n == null) return;

        Ellipse circle = new Ellipse
        {
            Width = 35,
            Height = 35,
            Fill = Brushes.LightSkyBlue,
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };
        Canvas.SetLeft(circle, x - 17.5);
        Canvas.SetTop(circle, y - 17.5);
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
                Y1 = y + 16,
                X2 = x - offset,
                Y2 = y + 80,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            TreeCanvas.Children.Add(line);

            DrawNode(n.Left, x - offset, y + 80, offset * 0.6);
        }


        if (n.Right != null)
        {
            Line line = new Line
            {
                X1 = x,
                Y1 = y + 16,
                X2 = x + offset,
                Y2 = y + 80,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            TreeCanvas.Children.Add(line);

            DrawNode(n.Right, x + offset, y + 80, offset * 0.6);
        }
    }
}