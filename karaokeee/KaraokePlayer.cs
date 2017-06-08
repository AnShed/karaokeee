using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace karaokeee
{
    class KaraokePlayer
    {
        Lyrics pickedSong;
        SoundPlayer songPlayer;
        Thread soundThread;

        public KaraokePlayer(string songPath, string lyricsPath)
        {
            initializeSound(songPath);
            initializeSongLyrics(lyricsPath);
        }
        
        private void initializeSongLyrics(string path)
        {
            pickedSong = new Lyrics(path);
        }

        private void initializeSound(string path)
        {
            try
            {
                songPlayer = new SoundPlayer(path);
                songPlayer.Load();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Lyrics file not found.", "Inputing lyrics Error");
            }
        }
                
        public void playSong()
        {
            soundThread = new Thread(songPlayer.Play);
            soundThread.Start();
        }

        public Lyrics getLyrics()
        {
            return pickedSong;
        }

    }
}
