using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace LinerApp
{
    class BorderedRectangle : ShapeRectangle
    {
        public BorderedRectangle() : base()
        {

        }

        public BorderedRectangle(Canvas cvs) : base(cvs)
        {
            rect.Fill = new SolidColorBrush(RandomColorGenerator.RandomColor());
            rect.StrokeDashArray = new DoubleCollection{ 4.0,2.0 };
            rect.Stroke = new SolidColorBrush(Colors.Black);

        }
    }
}
