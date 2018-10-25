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

namespace PointWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Point A = new Point(double.Parse(AxTextBox.Text), double.Parse(AyTextBox.Text));
                Point B = new Point(double.Parse(BxTextBox.Text), double.Parse(ByTextBox.Text));
                Point C = new Point(double.Parse(CxTextBox.Text), double.Parse(CyTextBox.Text));
                double d = double.Parse(dTextBox.Text);
                List<Point> points = Func(A, B, C, d);
                if(points.Count>0)
                {
                    NPointTextBlock.Text = "N: x = " + points[0].X + " ; y = "+points[0].Y;
                    MPointTextBlock.Text = "M: x = " + points[1].X + " ; y = " + points[1].Y;
                }
            
            }
            catch { ErrorLabel.Text = "Error"; }

        }

        public static List<Point> Func(Point A, Point B, Point C, double d)
        {
            if(A.X==B.X&&A.Y==B.Y)
            {
                //Случай совпадения А и В. Результат зависит от требований конкретной задачи
                return new List<Point>();
            }
            if (C.X == B.X && C.Y == B.Y)
            {
                //Случай совпадения C и В. Результат зависит от требований конкретной задачи
                return new List<Point>();
            }
            if (C.X == A.X && A.Y == C.Y)
            {
                //Случай совпадения А и C. Результат зависит от требований конкретной задачи
                return new List<Point>();
            }

            Line AB = new Line(A, B);
            Line BC = new Line(B, C);

            if (AB.IsParallel(BC)) return new List<Point>(); //Случай совпадения прямых. Результат зависит от требований конкретной задачи

            Line contur1AB = AB.GetParallelLine(d);
            Line contur2AB = AB.GetParallelLine(-d);
            Line contur1BC = BC.GetParallelLine(d);
            Line contur2BC = BC.GetParallelLine(-d);

            Point N1 = BC.CrossPoint(contur1AB);
            Point N2 = BC.CrossPoint(contur2AB);
            Point M1 = AB.CrossPoint(contur1BC);
            Point M2 = AB.CrossPoint(contur2BC);

            Point realN;
            Point realM;
            double distAM1 = Math.Sqrt(Math.Pow(A.Y - M1.Y, 2) + Math.Pow(A.X - M1.X, 2));
            double distAM2 = Math.Sqrt(Math.Pow(A.Y - M2.Y, 2) + Math.Pow(A.X - M2.X, 2));

            double distCN1 = Math.Sqrt(Math.Pow(C.Y - N1.Y, 2) + Math.Pow(C.X - N1.X, 2));
            double distCN2 = Math.Sqrt(Math.Pow(C.Y - N2.Y, 2) + Math.Pow(C.X - N2.X, 2));

             realM = distAM1 > distAM2 ?  M1:M2;
             realN = distCN1 > distCN2 ?  N1:N2;
        

            List<Point> result = new List<Point>() { realN,realM};
            return result;
        }

    }
}
