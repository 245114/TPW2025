using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface BallInterface
    {
        double X { get; set; }
        double Y { get; set; }
        double Radius { get; set; }
        double VelocityX { get; set; }
        double VelocityY { get; set; }
    }
}
