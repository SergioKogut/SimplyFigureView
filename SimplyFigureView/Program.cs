using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IMenu = System.Collections.Generic.List<System.Tuple<string, GetMethod>>;
using ItemMenu = System.Tuple<string, GetMethod>;
using MenuSpace;
public delegate void GetMethod();



namespace SimplyFigureView
{

    abstract class Figure
    {
        protected int CoordX;
        protected int CoordY;
        protected ConsoleColor Color;
        protected int Size;


        public Figure(int x, int y, ConsoleColor color, int size)
        {
            CoordX = x;
            CoordY = y;
            Color = color;
            Size = size;
        }

        protected void Print()
        {
            Console.SetCursorPosition(CoordX, CoordY);
            Console.ForegroundColor = Color;

        }
    }

    class Square : Figure
    {

        public Square(int x, int y, ConsoleColor color, int size) : base(x, y, color, size)
        { }

        public new void Print()
        {

            Console.ForegroundColor = Color;
            for (int i = 0; i < Size; i++)
            {
                Console.SetCursorPosition(CoordX, CoordY + i);
                Console.WriteLine(new string('*', Size));
            }

        }





    }



    class FigureView
    {
        private Menu Menu1;// меню
        private HorizontalMenu MenuSize;
        private VerticalMenu MenuColor;

        private bool ExitFlag = true; // флаг выхода из програмы

        public FigureView()
        {
            Menu1 = new Menu(1, 1, new IMenu

            {
                new ItemMenu(" Треугольник", new GetMethod(Print)),
                new ItemMenu(" Квадрат", new GetMethod(Print)),
                new ItemMenu(" Прямоугольник", new GetMethod(Print)),
                new ItemMenu(" Ромб", new GetMethod(Print)),
                new ItemMenu(" Трапеция", new GetMethod(Print)),
                new ItemMenu(" Многоугольник", new GetMethod(Print)),
                new ItemMenu(" Помощь", new GetMethod(Help)),
                new ItemMenu(" Выход", new GetMethod(Exit))
            });

            MenuSize = new HorizontalMenu(10, 12, new List<string> { "2", "3", "4", "5", "6" });
            MenuColor = new VerticalMenu(10, 13, new List<string> { "Синий", "Зеленый", "Бирюзовый", "Красный", "Розовый", "Желтый", "Белый" });
        }

        //помощь
        private void Help()
        {
            string text = "Мы все сможем!";
            Console.WriteLine(text);
            Console.ReadKey();
            Console.Clear();
        }
        private void ClearString(int x, int y, int size, int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(new string(' ', size));
            }

        }
        private void Print()
        {
            int x, y, size;
            Console.SetCursorPosition(0, 10);
            do
            {
                Console.Write("Введите координату X: ");
            } while (!Int32.TryParse(Console.ReadLine(), out x));

            Console.SetCursorPosition(0, 11);
            do
            {
                Console.Write("Введите координату Y: ");
            } while (!Int32.TryParse(Console.ReadLine(), out y));

            Console.SetCursorPosition(0, 12);
            Console.Write("Размер: ");
            size = Int32.Parse(MenuSize.Show());
            Console.SetCursorPosition(0, 13);
            Console.Write("Цвет: ");

            ConsoleColor color = (ConsoleColor)(MenuColor.Show() + 9);


            ClearString(0, 10, 30, 10);

            Square one = new Square(x, y, color, size);

            one.Print();
            Console.ReadKey();

        }

        //выход
        private void Exit()
        {
            Console.WriteLine("Выход\n Спасибо за пользование програмой!");

            ExitFlag = false;
        }

        public void Run()
        {
            do
            {
                Menu1.Show();
            } while (ExitFlag);

        }




    }




    class Program
    {
        static void Main(string[] args)
        {

            FigureView figureviewer = new FigureView();
            try
            {
                figureviewer.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            Console.ReadKey();

        }
    }
}
