using System;
using System.ComponentModel;
using System.Drawing;

namespace Data
{
    public class Ball : DataAPI, INotifyPropertyChanged
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
                    OnPropertyChanged(nameof(CanvasLeft));
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
                    OnPropertyChanged(nameof(CanvasTop));
                }
            }
        }

        public double Radius { get; set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }

        public double Mass { get; set; }

        public string Color { get; set; }

        public Ball(double x, double y, double radius, double velocityX, double velocityY, double mass, string color)
        {
            X = x;
            Y = y;
            Radius = radius;
            VelocityX = velocityX;
            VelocityY = velocityY;
            Mass = mass;
            Color = color;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public void move(double deltaTime)
        {
            this.X += this.VelocityX * deltaTime;
            this.Y += this.VelocityY * deltaTime;
        }
        

        public double CanvasLeft => X - Radius / 2;
        public double CanvasTop => Y - Radius / 2;
    }
}
