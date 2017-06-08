using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
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

namespace karaokeee
{
    public partial class MainWindow : Window
    {
        /*Lyrics pickedSong;
        SoundPlayer songPlayer;
        Thread soundThread;*/

        KaraokePlayer karaoke;

        int currentTime, nextVersicleTime, actualVersicleTime;
        int currentVersicle;
        bool isIntro;
        TimeSpan ts;

        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0);
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                ts = sw.Elapsed;
                currentTime = ts.Seconds;
                runLyrics();
            }
        }

        private void buttonQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            karaoke = new KaraokePlayer("Sounds\\wilczazamiec.wav","Lyrics\\wilczazamiec.txt");
            //initializeSongLyrics("Lyrics\\wilczazamiec.txt");
            currentVersicle = 0;
            actualVersicleTime = karaoke.getLyrics().getLine(currentVersicle).getTime();
            nextVersicleTime = karaoke.getLyrics().getLine(currentVersicle + 1).getTime();
            lineNext.Content = karaoke.getLyrics().getVersicle(currentVersicle);
            isIntro = true;
            karaoke.playSong();
            //initializeSound("Sounds\\wilczazamiec.wav");
            //soundThread = new Thread(playSong);            
            //soundThread.Start();
            sw.Start();
            dt.Start();
        }

        /*private void initializeSongLyrics(string path)
        {
            pickedSong = new Lyrics(path);
        }

        private void initializeSound(string path)
        {
            songPlayer = new SoundPlayer(path);
            songPlayer.Load();
        }

        private void playSong()
        {            
            songPlayer.Play();            
        }*/

        private void runLyrics()
        {            
            if (currentTime >= actualVersicleTime && currentTime < nextVersicleTime)
            {
                lineActual.Content = karaoke.getLyrics().getVersicle(currentVersicle);
                lineNext.Content = karaoke.getLyrics().getVersicle(currentVersicle + 1);
                isIntro = false;
            }
            else if(!isIntro)
            {
                currentVersicle++;
                actualVersicleTime = karaoke.getLyrics().getLine(currentVersicle).getTime();
                nextVersicleTime = karaoke.getLyrics().getLine(currentVersicle + 1).getTime();
            }
        }
    }
}
