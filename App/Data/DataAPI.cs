using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface DataAPI
    {
        double X { get; set; }
        double Y { get; set; }
        double Radius { get; set; }
        double VelocityX { get; set; }
        double VelocityY { get; set; }
        void move(double deltaTime);//double deltaTime);
    }
}
