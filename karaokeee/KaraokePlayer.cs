using System.IO;
using System.Media;
using System.Threading;
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

        public string getSingleLine(int index)
        {
            return pickedSong.getVersicle(index);
        }

        public int getLinesTime(int index)
        {
            return pickedSong.getVersicleTime(index);
        }

        public int getLinesAmount()
        {
            return pickedSong.getLinesAmount();
        }

    }
}
