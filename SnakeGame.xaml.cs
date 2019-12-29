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
        
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer secondsTimer = new DispatcherTimer();
        int seconds = 0;

        Point currentPosition = new Point(100, 50);
        string currentDirection = "right";

        List<Rectangle> bodyParts = new List<Rectangle>();
        List<Point> bodyPartsCoordinates = new List<Point>();

        bool reloadBonus = false; //if true the position of the bonus will change
        Rectangle bonus = new Rectangle();
        Point bonusCoordinates;

        int score = 0;
        Label scoreLabel = new Label();
       

        bool flagEatBonus = false; //true when the snake eats a bonus. if true the program will increase the snake length

        public SnakeGame()
        {
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(50);

            secondsTimer.Tick += SecondsCounter;
            secondsTimer.Interval = TimeSpan.FromMilliseconds(1000);

            InitializeComponent();

            timer.Start();
            secondsTimer.Start();

            scoreLabel.Content = score;
            scoreLabel.FontSize = 250;
            scoreLabel.FontFamily = new FontFamily("Courier new");
            scoreLabel.Foreground = Brushes.DarkGray;
            
           

            for (int i = 0; i < 5; i++) 
            {
                AddBodyPart(currentPosition);

                PrintSnake();

                PrintScoreLabel();

                currentPosition.X += 10;
            }

            
        }

        void SecondsCounter(object sender, EventArgs e)
        {
            seconds++;
        }

        void PrintSnake()
        {
            canvas.Children.Clear();
            for (int i = 0; i < bodyParts.Count; i++)
            {
                canvas.Children.Add(bodyParts[i]);
                Canvas.SetTop(bodyParts[i], bodyPartsCoordinates[i].Y);
                Canvas.SetLeft(bodyParts[i], bodyPartsCoordinates[i].X);
            }
        }

        void PrintScoreLabel()
        {
            canvas.Children.Add(scoreLabel);
            Canvas.SetTop(scoreLabel, 82);
            Canvas.SetLeft(scoreLabel, 331);
            Canvas.SetZIndex(scoreLabel, -100);
        }

        public void AddBodyPart(Point coordinates)
        {
            
            Rectangle r = new Rectangle();
            r.Height = 10;
            r.Width = 10;
            r.Fill = Brushes.Green;

            bodyPartsCoordinates.Add(coordinates);
            bodyParts.Add(r);

        }

        public void RestrictTail()
        {
            bodyParts.RemoveAt(0);
            bodyPartsCoordinates.RemoveAt(0);
        }

        void timer_Tick(object sender, EventArgs e) //Main Loop
        {
            AddBodyPart(currentPosition); 
             
            UpdateCurrentPosition();

            if(flagEatBonus)
            {
                AddBodyPart(currentPosition);
                UpdateCurrentPosition();
                flagEatBonus = false;
            }

            RestrictTail();
            PrintSnake();

            PrintBonus();

            PrintScoreLabel();

            if (CheckSnakeEatBonus())
                reloadBonus = false;

            if (CheckSnakeTouchingHimself())
            {
                timer.Stop();
                secondsTimer.Stop();
                //scrivo su file il punteggio
               TimeSpan time = TimeSpan.FromSeconds(seconds);
               FileManagement.SaveGameData(new GameData(score, DateTime.Now.ToString(), time.ToString(@"hh\:mm\:ss")));
                YouLose();
                NavigationService.Navigate(new StartMenu());
            }
        }

        void YouLose()
        {
            List<GameData> gameDatas = FileManagement.ReadGameData();
            int max = 0;
            foreach(GameData i in gameDatas)
            {
                if (i.Score > max)
                    max = i.Score;
            }

            if (score > max)
                MessageBox.Show("NEW RECORD!! SCORE: {0}", score.ToString());
            else
                MessageBox.Show("YOU LOSE!! SCORE: {0}", score.ToString());
        }

        bool CheckSnakeEatBonus()
        {
            scoreLabel.Content = score;
            if (currentPosition.Equals(bonusCoordinates))
            {
                score++;
                flagEatBonus = true;
                return true;
            }
            else
                return false;
        }

        bool CheckSnakeTouchingHimself()
        {
            if (bodyPartsCoordinates.Contains(currentPosition))
                return true;
            else
                return false;
        }

        void UpdateCurrentPosition()
        {
            //Updating current position based on current direction
            if (currentDirection == "right")
            {
                currentPosition.X += 10;
            }
                
            if (currentDirection == "down")
            {
                currentPosition.Y += 10;
            }
                
            if (currentDirection == "left")
            {
                currentPosition.X -= 10;
            }
                
            if (currentDirection == "up")
            {
                currentPosition.Y -= 10;
            }
                
            
            //If the snake reaches the edge of the window it will come back from the other side
            if (currentPosition.X >= canvas.ActualWidth)
            {
                currentPosition.X = 0;
                return;
            }

            if (currentPosition.X <= 0)
            {
                currentPosition.X = canvas.ActualWidth;
                return;
            }
                

            if (currentPosition.Y >= canvas.ActualHeight)
            {
                currentPosition.Y = 0;
                return;
            }
                

            if (currentPosition.Y <= 0)
            {
                currentPosition.Y = canvas.ActualHeight;
                return;
            }
                
        }

        void PrintBonus()
        {
            if (!reloadBonus) //se è la prima volta genero la posizione del bonus
            {
                bonusCoordinates.X = new Random().Next(0, Convert.ToInt16(canvas.ActualWidth));
                while (bonusCoordinates.X % 10 != 0 || bonusCoordinates.Y % 10 != 0 || bonusCoordinates.X < 10|| bonusCoordinates.X > canvas.ActualWidth-10)
                    bonusCoordinates.X = new Random().Next(0, Convert.ToInt16(canvas.ActualWidth));

                
                while (bonusCoordinates.Y % 10 != 0 || bonusCoordinates.Y < 10 || bonusCoordinates.Y > canvas.ActualHeight - 10)
                    bonusCoordinates.Y = new Random().Next(0, Convert.ToInt16(canvas.ActualHeight));

                Trace.WriteLine(bonusCoordinates.X.ToString() + "    " + bonusCoordinates.Y.ToString() + "           " + bodyPartsCoordinates[bodyPartsCoordinates.Count-1].X.ToString() + "  " + bodyPartsCoordinates[bodyPartsCoordinates.Count-1].Y.ToString());
                reloadBonus = !reloadBonus;
            }

            bonus.Width = 10;
            bonus.Height = 10;
            bonus.Fill = Brushes.Red;

            canvas.Children.Add(bonus);

            Canvas.SetTop(bonus, bonusCoordinates.Y);
            Canvas.SetLeft(bonus, bonusCoordinates.X);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    if (currentDirection != "up")
                        currentDirection = "down";
                    break;
                case Key.Left:
                    if (currentDirection != "right")
                        currentDirection = "left";
                    break;
                case Key.Up:
                    if (currentDirection != "down")
                        currentDirection = "up";
                    break;
                case Key.Right:
                    if (currentDirection != "left")
                        currentDirection = "right";
                    break;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.KeyDown += new KeyEventHandler(Window_KeyDown);
        }

    }
}
