using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

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
                    lyrics.Add(new Line(tempTime, tempLine));
                    lineNum++;
                }
                catch(InvalidOperationException)
                {
                    MessageBox.Show("Time cannot be less then 0 and string can't be empty.", "Inputing lyrics Error");
                }
                catch(FileNotFoundException)
                {
                    MessageBox.Show("Lyrics file not found.", "Inputing lyrics Error");
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
             
        public int getLinesAmount()
        {
            return lineNum;
        }

        public Line getLine(int lineNumber)
        {
            return lyrics[lineNumber];
        }

        public string getVersicle(int lineNumber)
        {
            return lyrics[lineNumber].getText();
        }

        public int getVersicleTime(int lineNumber)
        {
            return lyrics[lineNumber].getTime();
        }
    }
}
