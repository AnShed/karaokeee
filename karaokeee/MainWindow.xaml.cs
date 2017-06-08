using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace karaokeee
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// dogarnąc to wyświetlanie tekstu odpowiednio
        /// </summary>
        Lyrics pickedSong;
        SoundPlayer songPlayer;
        Thread soundThread;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            initializeSongLyrics("Lyrics\\wilczazamiec.txt");
            initializeSound("Sounds\\wilczazamiec.wav");
            runLyrics();
            soundThread = new Thread(playSong);            
            soundThread.Start();
        }

        private void initializeSongLyrics(string path)
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
        }

        private void runLyrics()
        {
            int versicle = 0;
            Line actual, next;

            actual = pickedSong.getLine(versicle);
            next = pickedSong.getLine(versicle + 1);

            lineActual.Content = actual.getText();
            lineNext.Content = next.getText();
        }
    }
}
