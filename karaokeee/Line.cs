using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace karaokeee
{
    class Line
    {
        int time;
        string versicle;

        public Line(int timeAppearing, string text)
        {
            if (timeAppearing >= 0 || text != null)
            {
                this.time = timeAppearing;
                this.versicle = text;                
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        
        public int getTime()
        {
            return time;
        }

        public string getText()
        {
            return versicle;
        }

    }
}
