using System;
using NAudio.Wave;

namespace karaokeee.Audio
{
    public class SaveAudio
    {
        private readonly IWaveIn _waveIn;
        private readonly WaveFileWriter _waveFile;

        public SaveAudio()
        {
            _waveIn = new WaveIn();
            _waveFile = new WaveFileWriter(@"Test0001.wav", _waveIn.WaveFormat);
        }

        public void Save()
        {
            _waveIn.WaveFormat = new WaveFormat(44100, 1);
            _waveIn.DataAvailable += DataAvailable;
            _waveIn.StartRecording();
        }

        private void DataAvailable(object sender, WaveInEventArgs e)
        {
            if (_waveFile != null)
            {
                _waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                _waveFile.Flush();
            }
        }

        
    }
}