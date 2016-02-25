using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LinerApp
{
    public static class RandomColorGenerator
    {
        public static Color RandomColor()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            byte red = (byte)r.Next(0, 255);
            byte green = (byte)r.Next(0, 255);
            byte blue = (byte)r.Next(0, 255);

            return Color.FromRgb(red, green, blue);
        }
    }

}
