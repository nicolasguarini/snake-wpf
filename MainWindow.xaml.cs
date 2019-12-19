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

namespace snake_wpf
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string dir = "right";
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new StartMenu();
        }

        private void MenuItem_Click_StartMenu(object sender, RoutedEventArgs e)
        {
            Main.Content = new StartMenu();
        }

        private void MenuItem_Click_RestartGame(object sender, RoutedEventArgs e)
        {
            Main.Content = new SnakeGame();
        }

        private void mw_KeyDown(object sender, KeyEventArgs e)
        {
   //         MessageBox.Show("MW WORKING");
        }
    }
}
