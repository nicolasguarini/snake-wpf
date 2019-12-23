using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_wpf
{
    class GameData
    {
        public int Score { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public GameData(int score, string date, string time)
        {
            Score = score;
            Date = date;
            Time = time;
        }
    }
}
