using System;
using NAudio.Wave;

namespace karaokeee.Audio
{
    public class CompareAudioFiles
    {
        private readonly string _pathFile;
        private readonly string _pathFileBase;
        private double _errorRate;

        public CompareAudioFiles(string pathFile, string pathFileBase)
        {
            _pathFile = pathFile;
            _pathFileBase = pathFileBase;
            _errorRate = 0;
        }

        public double RunCompare()
        {
            var fileByte = ReadFile(_pathFile);
            var fileBaseByte = ReadFile(_pathFileBase);

            Compare(fileByte, fileBaseByte);

            return 100 - Math.Floor(_errorRate);
        }

        private void Compare(byte[] file, byte[] fileBase)
        {
            var length = Math.Min(file.Length, fileBase.Length);
            for (var i = 0; i < length; i++)
            {
                var fileBaseValue = fileBase[i] == 0 ? 1 : fileBase[i];

                var difference =  (fileBase[i] - fileBaseValue)/fileBaseValue*100 ;
                _errorRate += difference < 0 ? difference * -1 : difference;
            }

            _errorRate /= length;
        }

        private byte[] ReadFile(string path)
        {
            var waveViewer = new WaveFileReader(path);

            byte[] buffer = new byte[waveViewer.Length];
            int read = waveViewer.Read(buffer, 0, buffer.Length);
            short[] sampleBuffer = new short[read / 2];
            Buffer.BlockCopy(buffer, 0, sampleBuffer, 0, read);

            return buffer;
        }
    }
}