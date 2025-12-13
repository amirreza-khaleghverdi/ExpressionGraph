using Page_Navigation_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Page_Navigation_App.View
{
    /// <summary>
    /// Interaction logic for ExpressionGraphView.xaml
    /// </summary>
    public partial class ExpressionGraphView : UserControl
    {
        public ExpressionGraphView()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty TreeRootProperty =
            DependencyProperty.Register("TreeRoot", typeof(node), typeof(ExpressionGraphView),
            new PropertyMetadata(null, OnTreeRootChanged));

        public node TreeRoot
        {
            get { return (node)GetValue(TreeRootProperty); }
            set { SetValue(TreeRootProperty, value); }
        }

        private static void OnTreeRootChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExpressionGraphView;
            if (control != null && e.NewValue is node root)
            {
                control.DrawTree(root);
            }
        }

        public void DrawTree(node root)
        {
            TreeCanvas.Children.Clear();
            double startX = TreeCanvas.ActualWidth > 0 ? TreeCanvas.ActualWidth / 2 : 220;
            DrawNode(root, startX, 20, 80, 80);
        }

        public void DrawNode(node n, double x, double y, double x_offset, double y_offset)
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
            Canvas.SetLeft(circle, x - circle.Width / 2);
            Canvas.SetTop(circle, y - circle.Height / 2);
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
                    Y1 = y + circle.Height / 2,
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
                    Y1 = y + circle.Height / 2,
                    X2 = x + x_offset,
                    Y2 = y + y_offset,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };
                TreeCanvas.Children.Add(line);

                DrawNode(n.Right, x + x_offset, y + y_offset, x_offset * 0.7, y_offset);
            }
        }

    }
}
