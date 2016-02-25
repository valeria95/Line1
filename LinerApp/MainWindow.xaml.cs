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

namespace LinerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ShapeLine> listLine = new List<ShapeLine>();
        Object firstRect; // Переменная для сравнения второго выбранного прямоугольника с типом первого

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonRectangle_Click(object sender, RoutedEventArgs e)
        {
            ShapeRectangle rectangle = new ShapeRectangle(this.mainCanvas);
            rectangle.mainCanvas = this.mainCanvas;
            mainCanvas.Children.Add(rectangle.rect);
            mainCanvas.Children.Add(rectangle);
        }

        public Rectangle tryGetRectangle(MouseButtonEventArgs e) //Метод для получения прямоугольника по нажатию мыши
        {
            bool IsHaveRectangle = false;

            double mouseX = e.GetPosition(mainCanvas).X;
            double mouseY = e.GetPosition(mainCanvas).Y;
            Rectangle mouseRect = new Rectangle();
            for (int i = 0; i < mainCanvas.Children.Count; i++)
            {
                if (typeof(ShapeRectangle) == (mainCanvas.Children[i].GetType())) // Проверка на простой прямоугольник
                {
                    ProverkaSR(i, IsHaveRectangle, mouseX, mouseY);
                   
                }

                if (typeof(BorderedRectangle) == (mainCanvas.Children[i].GetType())) // Проверка на прямоугольник с границей
                {
                    ProverkaBR(i, IsHaveRectangle, mouseX, mouseY);
                    
                }

            }
            if (IsHaveRectangle)
            {
                return mouseRect; //Возврат того или иного прямоугольника
            }
            else
            {
                return null;
            }
        }
        private void ProverkaSR(int i,bool IsHaveRectangle,double mouseX, double mouseY)
        {

            double rLeft = Canvas.GetLeft(((ShapeRectangle)mainCanvas.Children[i]).rect);
            double rTop = Canvas.GetTop(((ShapeRectangle)mainCanvas.Children[i]).rect);
            double rWidht = ((ShapeRectangle)mainCanvas.Children[i]).rect.Width;
            double rHeight = ((ShapeRectangle)mainCanvas.Children[i]).rect.Height;

            if (mouseX >= rLeft && mouseX <= rLeft + rWidht && mouseY >= rTop && mouseY <= rTop + rHeight)
            {
                Rectangle mouseRect = new Rectangle();
                mouseRect = ((ShapeRectangle)mainCanvas.Children[i]).rect;
                IsHaveRectangle = true;
            }
        }

        private void ProverkaBR(int i, bool IsHaveRectangle, double mouseX, double mouseY)
        {

            double rLeft = Canvas.GetLeft(((BorderedRectangle)mainCanvas.Children[i]).rect);
            double rTop = Canvas.GetTop(((BorderedRectangle)mainCanvas.Children[i]).rect);
            double rWidht = ((BorderedRectangle)mainCanvas.Children[i]).rect.Width;
            double rHeight = ((BorderedRectangle)mainCanvas.Children[i]).rect.Height;

            if (mouseX >= rLeft && mouseX <= rLeft + rWidht && mouseY >= rTop && mouseY <= rTop + rHeight)
            {
                Rectangle mouseRect = new Rectangle();
                mouseRect = ((BorderedRectangle)mainCanvas.Children[i]).rect;
                IsHaveRectangle = true;
            }
        }
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            mainCanvas.Children.Clear();
        }

        private void mainCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
                ShapeLine myLine = null;
                myLine = new ShapeLine();
                myLine.RectangleOne = tryGetRectangle(e);
                mainCanvas.MouseMove += myLine.onMouseMove;
                 firstRect = tryGetShape(e); //Присвоение переменной для дальнейшего сравнения типов
                if (tryGetRectangle(e) != null && tryGetShape(e).GetType() != typeof(ShapeRectangle))
                {
                    mainCanvas.MouseMove += myLine.reDrawingLine1;                   
                }
                myLine.mainCanvas = this.mainCanvas;
                myLine.onMouseDown(sender, e);
                mainCanvas.Children.Add(myLine.line);
                listLine.Add(myLine);
        }

        private void mainCanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
           
                foreach (ShapeLine line in listLine)
                {
                    if (line.isDrawing)
                    {
                        line.RectangleTwo = tryGetRectangle(e);
                        Method(line, e);
                       
                    line.isDrawing = false;
                    }
                }
    }
        private void Method(ShapeLine line, MouseButtonEventArgs e)
        {
            if (tryGetRectangle(e) != null &&
                           line.RectangleOne != null &&
                           tryGetShape(e).GetType() == typeof(ShapeRectangle) &&
                           line.RectangleOne != line.RectangleTwo &&
                           tryGetShape(e).GetType() != firstRect.GetType())
            {
                mainCanvas.MouseMove += line.reDrawingLine2;
            }
            else
            {
                if (mainCanvas.Children.Count > 0)
                {
                    for (int i = 3; i > 0; i--)
                    {
                        mainCanvas.Children.RemoveAt(this.mainCanvas.Children.Count - 1); //Удаление последних трех элементов, так как линия теперь из 3 линий, для имитации стрелки
                    }
                }
            }
        }

        private void btnBorderedRect_Click(object sender, RoutedEventArgs e) // Создание прямоугольника с границей
        {
            BorderedRectangle rectangle = new BorderedRectangle(mainCanvas);
            rectangle.mainCanvas = this.mainCanvas;
            mainCanvas.Children.Add(rectangle.rect);
            mainCanvas.Children.Add(rectangle); // Добавление элемента на канвас, для отлова его типо в дальнейшем
        }


        public Object tryGetShape(MouseButtonEventArgs e) // Метод получения типа элемента, для сравнения типов и работы с ограничением рисования
        {
            bool IsHaveRectangle = false;

            double mouseX = e.GetPosition(mainCanvas).X;
            double mouseY = e.GetPosition(mainCanvas).Y;
            Object mouseRect = new Object();
            for (int i = 0; i < mainCanvas.Children.Count; i++)
            {
                if (typeof(ShapeRectangle) == (mainCanvas.Children[i].GetType()))
                {
                    //Получение переменных для проверки условия попадания мыши в прямоугольник
                    double rLeft = Canvas.GetLeft(((ShapeRectangle)mainCanvas.Children[i]).rect);
                    double rTop = Canvas.GetTop(((ShapeRectangle)mainCanvas.Children[i]).rect);
                    double rWidht = ((ShapeRectangle)mainCanvas.Children[i]).rect.Width;
                    double rHeight = ((ShapeRectangle)mainCanvas.Children[i]).rect.Height;

                    if (mouseX >= rLeft && mouseX <= rLeft + rWidht && mouseY >= rTop && mouseY <= rTop + rHeight) // Проверка условия попадания
                    {
                        mouseRect = (ShapeRectangle)mainCanvas.Children[i];
                        IsHaveRectangle = true;
                    }
                }
                if (typeof(BorderedRectangle) == (mainCanvas.Children[i].GetType())) // Аналогично верхнему, только для другого типа
                {
                    double rLeft = Canvas.GetLeft(((BorderedRectangle)mainCanvas.Children[i]).rect);
                    double rTop = Canvas.GetTop(((BorderedRectangle)mainCanvas.Children[i]).rect);
                    double rWidht = ((BorderedRectangle)mainCanvas.Children[i]).rect.Width;
                    double rHeight = ((BorderedRectangle)mainCanvas.Children[i]).rect.Height;

                    if (mouseX >= rLeft && mouseX <= rLeft + rWidht && mouseY >= rTop && mouseY <= rTop + rHeight)
                    {
                        mouseRect = (BorderedRectangle)mainCanvas.Children[i];
                        IsHaveRectangle = true;
                    }
                }

            }
            if (IsHaveRectangle)
            {
                return mouseRect; //Возврат объекта с канваса, для получения его типа для дальнейшего сравнения
            }
            else
            {
                return null;
            }
        }
    }


}