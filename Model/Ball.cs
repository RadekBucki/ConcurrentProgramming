using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class Ball
    {
        public Ball()
        {
            Random r = new();
            this.XPos = r.Next(0, 561);
            this.YPos = r.Next(0, 561);
        }

        public int XPos { get; set; }
        public int YPos { get; set; }
    }
}
