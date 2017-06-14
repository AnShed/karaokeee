using System;
using NAudio.Dsp;

namespace karaokeee.Audio
{
    public class SampleAggregator
    {

        private Complex[] fftBuffer;
   
        private int fftPos = 0;
        private int fftLength;
        private int m;

        public Complex[] GetFftBuf()
        {
            return fftBuffer;
        }

        public SampleAggregator(int fftLength)
        {
            if (!IsPowerOfTwo(fftLength))
            {
                throw new ArgumentException("FFT Length must be a power of two");
            }
            this.m = (int)Math.Log(fftLength, 2.0);
            this.fftLength = fftLength;
            this.fftBuffer = new Complex[fftLength];
          
        }

        bool IsPowerOfTwo(int x)
        {
            return (x & (x - 1)) == 0;
        }

        public void Add(float value)
        {
            fftBuffer[fftPos].X = (float)(value * FastFourierTransform.HammingWindow(fftPos, fftLength));
                fftBuffer[fftPos].Y = 0; 
                fftPos++;
                if (fftPos >= fftLength)
                {
                    fftPos = 0;
                    FastFourierTransform.FFT(true, m, fftBuffer);
                }     
        }
    }

}