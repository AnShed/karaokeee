using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace karaokeee
{
    public partial class MainWindow : Window
    {
        KaraokePlayer karaoke;
        TimeSpan ts;
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();

        int currentTime;
        int currentVersicle;
        bool isIntro;
                
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
            if (sw.IsRunning)
            {
                sw.Stop();
                dt.Stop();
            }
            playKaraoke();            
        }
        
        private void playKaraoke()
        {
            karaoke = new KaraokePlayer("Sounds\\wilczazamiec.wav", "Lyrics\\wilczazamiec.txt");

            initialValues();

            karaoke.playSong();
            sw.Start();
            dt.Start();
        }

        private void initialValues()
        {
            currentVersicle = 0;
            lineNext.Content = karaoke.getSingleLine(currentVersicle);
            isIntro = true;
        }

        private void runLyrics()
        {            
            if (currentTime >= karaoke.getLinesTime(currentVersicle) && currentTime < karaoke.getLinesTime(currentVersicle + 1))
            {
                lineActual.Content = karaoke.getSingleLine(currentVersicle);
                if (currentVersicle < karaoke.getLinesAmount())
                {
                    lineNext.Content = karaoke.getSingleLine(currentVersicle + 1);
                }
                else
                {
                    lineNext.Content = "";
                }

                isIntro = false;
            }
            else if(!isIntro)
            {
                if (currentVersicle < karaoke.getLinesAmount())
                {
                    currentVersicle++;
                }

            }

        }

    }

}
