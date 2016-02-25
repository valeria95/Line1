using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinerApp
{
    class ShapeRectangle : UIElement
    {
        public Rectangle rect;
        public Canvas mainCanvas;
        protected double _beginX, _beginY, _currX, _currY;
        protected bool _IsMouseDown = false;
        Random rnd = new Random();

        
        public ShapeRectangle()
        {
             rect = new Rectangle();
             rect.Height = rnd.Next(40, 50);
             rect.Width = rnd.Next(80, 100);
             rect.StrokeThickness = 1;
             rect.MouseLeftButtonDown += onMouseDown;
             rect.MouseUp += onMouseUp;
             rect.MouseMove += onMouseMove;
            rect.Fill = new SolidColorBrush(RandomColorGenerator.RandomColor());
            rect.Stroke = new SolidColorBrush(Colors.Gray);
        }

        
        public ShapeRectangle(Brush fillColorBrush, Brush strokeColorBrush) : this()
        {
            rect.Stroke = strokeColorBrush;
            rect.Fill = fillColorBrush;
        }

        public ShapeRectangle(Canvas cvs) : this()
        {
            Canvas.SetLeft(rect, rnd.Next(0, (int)cvs.ActualWidth - (int)rect.Width));
            Canvas.SetTop(rect, rnd.Next(0, (int)cvs.ActualHeight - (int)rect.Height));
        }

        protected void onMouseDown(Object sender, MouseButtonEventArgs e)
        {
                Rectangle rect = sender as Rectangle;
                _beginX = e.GetPosition(mainCanvas).X;
                _beginY = e.GetPosition(mainCanvas).Y;
                _IsMouseDown = true;
                rect.CaptureMouse();
        }

        protected void onMouseMove(Object sender, MouseEventArgs e)
        {
            if (_IsMouseDown)
            {
                Rectangle rect = sender as Rectangle;
                _currX = e.GetPosition(mainCanvas).X;
                _currY = e.GetPosition(mainCanvas).Y;
                rect.SetValue(Canvas.LeftProperty, _currX - rect.Width /2);
                rect.SetValue(Canvas.TopProperty, _currY - rect.Height /2);
            }
        }
        protected void onMouseUp(Object sender, MouseButtonEventArgs e)
        {
            _beginY = _currY;
            _beginX = _currX;
            Rectangle rect = sender as Rectangle;
            _IsMouseDown = false;
            rect.ReleaseMouseCapture();
        }
    }
}
