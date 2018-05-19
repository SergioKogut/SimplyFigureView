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



        public Figure(int x, int y, ConsoleColor color)
        {
            CoordX = x;
            CoordY = y;
            Color = color;

        }

        public void Print()
        {
            Console.SetCursorPosition(CoordX, CoordY);
            Console.ForegroundColor = Color;

        }
    }

    class Square : Figure
    {
        private int a;
        public Square(int x, int y, ConsoleColor color, int a) : base(x, y, color)
        {
            this.a = a;
        }

        public new void Print()
        {
            Console.ForegroundColor = Color;
            for (int i = 0; i < a; i++)
            {
                Console.SetCursorPosition(CoordX, CoordY + i);
                Console.WriteLine(new string('*', a));
            }
        }
    }

    class Rectangle : Figure
    {
        private int a, b;
        public Rectangle(int x, int y, ConsoleColor color, int a, int b) : base(x, y, color)
        {
            this.a = a;
            this.b = b;
        }

        public new void Print()
        {
            Console.ForegroundColor = Color;
            for (int i = 0; i < b; i++)
            {
                Console.SetCursorPosition(CoordX, CoordY + i);
                Console.WriteLine(new string('*', a));
            }
        }
    }

    class Triangle : Figure
    {
        private int a;
        public Triangle(int x, int y, ConsoleColor color, int a) : base(x, y, color)
        {
            this.a = a;
        }

        public new void Print()
        {
            Console.ForegroundColor = Color;
            for (int i = 0; i < a; i++)
            {
                Console.SetCursorPosition(CoordX, CoordY + i);
                Console.WriteLine(new string('*', 1 + i));
            }
        }
    }



    class FigureView
    {
        private Menu Menu1;// меню
        private HorizontalMenu MenuSize;
        private VerticalMenu MenuColor;
        private List<Figure> Figures;
        private bool ExitFlag = true; // флаг выхода из програмы

        public FigureView()
        {
            Menu1 = new Menu(1, 1, new IMenu

            {
                new ItemMenu(" Треугольник", new GetMethod(PrintTriangle)),
                new ItemMenu(" Квадрат", new GetMethod(PrintSquare)),
                new ItemMenu(" Прямоугольник", new GetMethod(PrintRectangle)),
                new ItemMenu(" Ромб", new GetMethod(PrintRomb)),
                new ItemMenu(" Трапеция", new GetMethod(PrintTrapeze)),
                new ItemMenu(" Многоугольник", new GetMethod(PrintNAngle)),
                new ItemMenu(" Помощь", new GetMethod(Help)),
                new ItemMenu(" Выход", new GetMethod(Exit))
            });

            MenuSize = new HorizontalMenu(10, 12, new List<string> { "2", "3", "4", "5", "6" });
            MenuColor = new VerticalMenu(10, 14, new List<string> { "Синий", "Зеленый", "Бирюзовый", "Красный", "Розовый", "Желтый", "Белый" });
        }

       


        private void Print()
        {
            foreach (var figure in Figures)
            {
                figure.Print();

            }

        }
        


        //помощь
        private void Help()
        {
            Console.SetCursorPosition(0, 15);
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
        

        private void PrintSquare()
        {
            SetFigure(1);
        }

        private void PrintRectangle()
        {
            SetFigure(2);
        }

        private void PrintTriangle()
        {
            SetFigure(3);
        }

        private void PrintRomb()
        {
            SetFigure(4);
        }
        private void PrintTrapeze()
        {
            SetFigure(5);
        }
        private void PrintNAngle()
        {
            SetFigure(6);
        }




        private void SetFigure(int type)
        {
            int x, y, a;
            int b = 2;
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
            Console.Write("Размер 1: ");
            a = Int32.Parse(MenuSize.Show());
            if (type == 2)
            {
                Console.SetCursorPosition(0, 13);
                Console.Write("Размер 2: ");
                MenuSize.SetY(13);
                b = Int32.Parse(MenuSize.Show());
            }

            Console.SetCursorPosition(0, 14);
            Console.Write("Цвет: ");
            ConsoleColor color = (ConsoleColor)(MenuColor.Show() + 9);
            ClearString(0, 10, 30, 12);

            switch (type)
            {
                case 1:
                    {
                        Figure one = new Square(x, y, color, a);
                      //  Figures.Add(one);
                        ((Square)one).Print();
                      
                        break;
                    }
                case 2:
                    {

                        Figure one = new Rectangle(x, y, color, a, b);
                        ((Rectangle)one).Print();
                        //Figures.Add(one);
                        break;
                    }
                case 3:
                    {

                        Figure one = new Triangle(x, y, color, a);
                        ((Triangle)one).Print();
                        //Figures.Add(one);
                        break;
                    }

                case 4:
                    {
                        //ромб

                        break;

                    }

                case 5:
                    {
                        //трапеція

                        break;

                    }
                case 6:
                    {
                        //багатокутник

                        break;

                    }



                default:
                    break;
            }
            
          //  Console.ReadKey();

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
