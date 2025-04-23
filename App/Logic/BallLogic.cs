using System;
using System.Collections.Generic;
using System.Drawing;
using Data;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace Logic
{
    public class BallLogic : BallLogicInterface
    {
        private readonly List<BallInterface> balls = new();
        private readonly Random random = new();
        public ObservableCollection<Ball> BallsCollection;

        private readonly Timer timer;

        private readonly double _width;
        private readonly double _height;

        public BallLogic(double width, double height)
        {
            _width = width;
            _height = height;

            BallsCollection = new ObservableCollection<Ball>();

            timer = new Timer(20);
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        public void AddBallToCollection(Ball ball)
        {
            BallsCollection.Add(ball);
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdatePositions();
        }

        public void StartTimer()
        {
            timer.Start();
        }

        public IReadOnlyList<BallInterface> Balls => balls.AsReadOnly();

        public void GenerateBalls(int count)
        {
            balls.Clear();
            for (int i = 0; i < count; i++)
            {
                double x = random.NextDouble() * (_width - 40);
                double y = random.NextDouble() * (_height - 40);
                double vx = random.NextDouble() * 5 - 2.5;
                double vy = random.NextDouble() * 5 - 2.5;

                balls.Add(new Ball(x, y, 20, vx, vy));
            }
        }

        public void UpdatePositions()
        {
            foreach (var ball in BallsCollection)
            {
                ball.X += ball.VelocityX;
                ball.Y += ball.VelocityY;

                if (ball.X < 0 || ball.X + ball.Radius * 2 > _width)
                    ball.VelocityX *= -1;

                if (ball.Y < 0 || ball.Y + ball.Radius * 2 > _height)
                    ball.VelocityY *= -1;
            }
            BallsUpdated?.Invoke(this, balls);
        }

        public event EventHandler<IEnumerable<BallInterface>> BallsUpdated;
    }
}
