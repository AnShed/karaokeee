using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Reflection;

namespace karaokeee
{
    class Lyrics
    {
        List<Line> lyrics;
        int lineNum;

        public Lyrics(string path)
        {
            lyrics = new List<Line>();
            lineNum = 0;
            prepareLyrics(path);
        }

        private void addLine(int time, string line)
        {
            try
            {
                lyrics[lineNum] = new Line(time, line);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Wrong execution time or string is null", "Inputing lyrics Error");
            }
        }

        private void prepareLyrics(string path)
        {
            int tempTime;
            string tempLine;

            StreamReader file = new StreamReader(path);
            
            while ((tempLine = file.ReadLine()) != null)
            {
                try
                {
                    tempTime = Int32.Parse(tempLine);
                    tempLine = file.ReadLine();
                    lyrics.Add(new Line(tempTime, insertProperChars(tempLine)));
                    lineNum++;
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Line time is null.", "Inputing lyrics Error");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Line time is not in the correct format.", "Inputing lyrics Error");
                }               
            }
            file.Close();
        }
        
        private string insertProperChars(string given)
        {
            given = given.Replace('1', 'ą');
            given = given.Replace('2', 'ć');
            given = given.Replace('3', 'ę');
            given = given.Replace('4', 'ł');
            given = given.Replace('5', 'ń');
            given = given.Replace('6', 'ó');
            given = given.Replace('7', 'ś');
            given = given.Replace('8', 'ź');
            given = given.Replace('9', 'ż');

            return given;
        }

        public int getLinesAmount()
        {
            return lineNum;
        }

        public Line getLine(int lineNumber)
        {
            return lyrics[lineNumber];
        }
    }
}
