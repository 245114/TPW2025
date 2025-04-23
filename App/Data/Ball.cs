using System.ComponentModel;
using System.Drawing;

namespace Data
{
    public class Ball : BallInterface, INotifyPropertyChanged
    {
        private double _x;
        public double X
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged(nameof(X));
                }
            }
        }

        private double _y;
        public double Y
        {
            get => _y;
            set
            {
                if (_y != value)
                {
                    _y = value;
                    OnPropertyChanged(nameof(Y));
                }
            }
        }

        public double Radius { get; set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }

        public Ball(double x, double y, double radius, double velocityX, double velocityY)
        {
            X = x;
            Y = y;
            Radius = radius;
            VelocityX = velocityX;
            VelocityY = velocityY;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
