using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PointWpf
{
    public class Line
    {
        public double k { get; set; }
        public double b { get; set; }
        public double x { get; set; }
        public bool isVertical { get; set; }
        public Line(Point A, Point B)
        {
            if (B.X == A.X)
            {
                k = 0;
                x = B.X;
                b = 0;
                isVertical = true;
            }
            else
            {
                k = (B.Y - A.Y) / (B.X - A.X);
                b = A.Y - (k * A.X);
                x = 0;
                isVertical = false;
            }
        }


        public Line(double k, double b)
        {
            this.k = k;
            this.b = b;
            isVertical = false;
            x = 0;
        }

        public Line(double x)
        {
            isVertical = true;
            k = 0;
            b = 0;
            this.x = x;
        }

        public Line GetParallelLine(double d)
        {
            if(isVertical)
            {
                return new Line(x - d);
            }
            else
            {
                double k2 = k;
                double b2 = b + d * Math.Sqrt(k * k + 1);
                return new Line(k2,b2);
            }
        }

        public Point CrossPoint(Line line)
        {
            if(line.isVertical)
            {
                if (isVertical) throw new Exception();
                else
                {
                    return new Point(line.x, k * line.x + b);
                }
            }
            else
            {
                if(isVertical) return new Point(x, line.k * x + line.b);
                else
                {
                    double pointX = (line.b - b) / (k - line.k);
                    double pointY = k * pointX + b;
                    return new Point(pointX,pointY);
                }
            }

        }

        public bool IsParallel(Line line)
        {
            if (line.isVertical && isVertical) return true;
            if (!line.isVertical && !isVertical && line.k == k) return true;
            return false;
        }
    }
}
