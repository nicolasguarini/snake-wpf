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
    /// Logica di interazione per Scores.xaml
    /// </summary>
    public partial class Scores : Page
    {
        public Scores()
        {
            InitializeComponent();
            List<GameData> gameDatas = FileManagement.ReadGameData();
            foreach(GameData i in gameDatas)
            {
                ScoresList.Items.Add(i.Score + "\t\t\t" + i.Date + "\t\t" + i.Time);
            }
        }

        private void BtnClickBackToStartMenu(object sender, EventArgs e)
        {
            NavigationService.Navigate(new StartMenu());
        }

        private void BtnClickResetScores(object sender, EventArgs e)
        {
            FileManagement.ResetFile();
            NavigationService.Navigate(new Scores());
        }
    }
}
