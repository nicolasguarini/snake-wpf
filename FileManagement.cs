using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace snake_wpf
{
    static class FileManagement
    {
        static string path = "./";
        static string fileName = "scores.dat";
        static int[] lengths = { 5, 19, 8 };

        public static void SaveGameData(GameData gameData)
        {
            try
            {
                string record = gameData.Score.ToString().PadRight(lengths[0], ' ') + gameData.Date + gameData.Time;
                StreamWriter sw = new StreamWriter(path + fileName, true);
                sw.WriteLine(record);
                sw.Close();
            }
            catch(Exception e)
            {
                Trace.WriteLine(e);
            }
        }

        public static List<GameData> ReadGameData()
        {
            List<GameData> gameDatas = new List<GameData>();
            string record;

            try
            {
                StreamReader sr = new StreamReader(path + fileName);

                while((record = sr.ReadLine()) != null)
                {
                    string[] fields = GetFields(record);
                    GameData gameData = new GameData(Convert.ToInt32(fields[0]), fields[1], fields[2]);
                    gameDatas.Add(gameData);
                }

                sr.Close();
            }
            catch(Exception e)
            {
                Trace.WriteLine(e);
            }

            return gameDatas;
        }

        public static void ResetFile()
        {
            File.WriteAllText(path + fileName, String.Empty);
        }

        static string[] GetFields(string record)
        {
            string[] fields = new string[lengths.Length];
            int i = 0;
            int position = 0;

            do
            {
                fields[i] = record.Substring(position, lengths[i]);
                fields[i] = fields[i].TrimEnd(' ');

                position += lengths[i];
                i++;
            } while (i < lengths.Length);

            return fields;
        }
    }
}
