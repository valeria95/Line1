using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

using System.Windows.Controls;

namespace LinerApp
{
    class ShapeLine
    {
       
        public Line line,lR,lL;
        public Canvas mainCanvas;
        public bool isDrawing = false;
        Point point;
        public Rectangle RectangleOne;
        public Rectangle RectangleTwo;
        private Color _myColor;
        RotateTransform HeadRotateTransform = new RotateTransform();
		
        public ShapeLine()
        {
            line = new Line();
            line.StrokeEndLineCap = PenLineCap.Triangle;
            line.StrokeThickness = 3;
            _myColor = RandomColorGenerator.RandomColor();
            line.Stroke = new SolidColorBrush(_myColor);
            lR = new Line();
            lL = new Line();
            lR.Stroke = new SolidColorBrush(_myColor);
            lL.Stroke = new SolidColorBrush(_myColor);
            lR.StrokeThickness = 3.00;
            lL.StrokeThickness = 3.00;
        }

        
        public ShapeLine(Brush brush) : this()
        {
            line.Stroke = brush;
        }


        public void onMouseDown(Object sender, MouseButtonEventArgs e)
        {
            point = e.GetPosition(mainCanvas);
            line.X1 = line.X2 = point.X;
            line.Y1 = line.Y2 = point.Y;
            DrawArrowHead(new Point(line.X2, line.Y2), mainCanvas);
            isDrawing = true;
        }

        public void onMouseMove(Object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                Point pointEnd = e.GetPosition(mainCanvas);
                line.X2 = pointEnd.X;
                line.Y2 = pointEnd.Y;
                ReDrawArrowHead(new Point(line.X2, line.Y2));
            }
        }

        public void reDrawingLine1(Object sender, MouseEventArgs e)
        {
            line.X1 = Canvas.GetLeft(RectangleOne) + RectangleOne.Width / 2;
            line.Y1 = Canvas.GetTop(RectangleOne) + RectangleOne.Height / 2;
        }

        public void reDrawingLine2(Object sender, MouseEventArgs e)
        {
            line.X2 = Canvas.GetLeft(RectangleTwo) + RectangleTwo.Width / 2;
            line.Y2 = Canvas.GetTop(RectangleTwo) + RectangleTwo.Height / 2;

            ReDrawArrowHead(new Point(line.X2, line.Y2));
        }

        private void ReDrawArrowHead(Point endPoint) 
        {
            DrawArrow(endPoint); 
            Double angleOfLine = new Double(); 
            angleOfLine = Math.Atan2((endPoint.Y - line.Y1), (endPoint.X - line.X1)) * 180 / Math.PI; 
            HeadRotateTransform.Angle = angleOfLine; 
            HeadRotateTransform.CenterX = endPoint.X;
            HeadRotateTransform.CenterY = endPoint.Y;
            lL.RenderTransform = HeadRotateTransform; 
            lR.RenderTransform = HeadRotateTransform;
        }

        private void DrawArrowHead(Point endPoint, Canvas mainCanvas) 
        {
            DrawArrow(endPoint);
            mainCanvas.Children.Add(lR);
            mainCanvas.Children.Add(lL);
        }

        private void DrawArrow(Point endPoint) 
        {
            
            lR.X1 = endPoint.X - 10.00;
            lR.X2 = endPoint.X;
            lR.Y1 = endPoint.Y + 10.00;
            lR.Y2 = endPoint.Y;

            lL.X1 = endPoint.X - 10.00;
            lL.X2 = endPoint.X;
            lL.Y1 = endPoint.Y - 10.00;
            lL.Y2 = endPoint.Y;
        }

    }

}