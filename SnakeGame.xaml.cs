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
using System.Windows.Threading;
using System.Diagnostics;

namespace snake_wpf
{
    public partial class SnakeGame : Page
    {
        List<Point> snakePartsCoordinates = new List<Point>(); //list that will contain the coordinates of every single snake part
        const int snakeSize = 10; //width in px of the snake's head
        string direction = "right";
        

        DispatcherTimer timer = new DispatcherTimer(); 
        public SnakeGame()
        {
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(150);
            SetInitialParts();

            InitializeComponent();
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (Point i in snakePartsCoordinates)
                PrintSnakePart(i);
            SnakeMove(direction);
        }

        private void SetInitialParts()
        {
            //setting the coordinates of the first 4 parts of the snake (the game starts with 4 snake parts)
            snakePartsCoordinates.Add(new Point(100, 100));

            for(int i = 0; i<4; i++)
                snakePartsCoordinates.Add(new Point(snakePartsCoordinates[i].X + 10, snakePartsCoordinates[i].Y));
        }

        private void PrintSnakePart(Point coordinates)
        {
            Rectangle rect = new Rectangle();
            rect.Fill = Brushes.Black;
            rect.Width = snakeSize;
            rect.Height = snakeSize;

            canvas.Children.Add(rect);

            Canvas.SetLeft(rect, coordinates.X);
            Canvas.SetTop(rect, coordinates.Y);

        }

        private void SnakeMove(string direction)
        {
            if(direction.Equals("right"))
            {
                Point lastPartCoordinates = snakePartsCoordinates[snakePartsCoordinates.Count - 1]; //saving the snake head's coordinates

                snakePartsCoordinates.Add(new Point(lastPartCoordinates.X + snakeSize, lastPartCoordinates.Y)); //adding a new part next to the head (the last part in the list) of the snake
                snakePartsCoordinates.Remove(snakePartsCoordinates[0]); //removing the last part of the snake (the tail)(the first in the list)
                
            }
        }
    }
}
