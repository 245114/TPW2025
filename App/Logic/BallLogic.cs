using System;
using System.Collections.Generic;
using System.Drawing;
using Data;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Logic
{
    public class BallLogic : LogicAPI
    {
        private readonly object _lock = new();

        private readonly List<DataAPI> balls = new();
        private readonly Random random = new();
        public ObservableCollection<Ball> BallsCollection { get; private set; }


        private readonly Timer timer;

        private int _width;
        private int _height;


        public BallLogic(int width, int height)
        {
            _width = width;
            _height = height;

            BallsCollection = new ObservableCollection<Ball>();

            timer = new Timer(10);
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        public void AddBallToCollection(Ball ball)
        {
            BallsCollection.Add(ball);
        }

        public void SetCanvasSize(int width, int height)
        {
            _width = width;
            _height = height;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            lock (_lock)
            {
                UpdatePositions();
            }
        }

        public void StartTimer()
        {
            timer.Start();
        }

        public IReadOnlyList<DataAPI> Balls => balls.AsReadOnly();

        public void GenerateBalls(int count)
        {
            lock (_lock)
            {
                BallsCollection.Clear();
                balls.Clear();
                for (int i = 0; i < count; i++)
                {
                    Ball ball = new(random.NextDouble() * _width, random.NextDouble() * _height, 20, random.NextDouble() * 5 - 2.5, random.NextDouble() * 5 - 2.5, random.NextDouble() * 3.5 + 0.5, $"#{random.Next(0x1000000):X6}");
                    AddBallToCollection(ball);
                    UpdatePositions();
                }
            }
        }


        public void UpdatePositions()
        {
            lock (_lock)
            {
                foreach (var ball in BallsCollection)
                {
                    ball.X += ball.VelocityX;
                    ball.Y += ball.VelocityY;

                    if (ball.X + ball.Radius < 20 || ball.X + ball.Radius * 2 > _width - 10)
                        ball.VelocityX *= -1;

                    if (ball.Y + ball.Radius * 2 < 40 || ball.Y + ball.Radius * 2 > _height - 20)
                        ball.VelocityY *= -1;
                }
                Collisions();
                BallsUpdated?.Invoke(this, balls);
            }
        }

        private void Collisions()
        {
            lock (_lock)
            {
                for (int i = 0; i < BallsCollection.Count; i++)
                {
                    for (int j = i + 1; j < BallsCollection.Count; j++)
                    {
                        Ball a = BallsCollection[i];
                        Ball b = BallsCollection[j];

                        double distanceX = b.X - a.X;
                        double distanceY = b.Y - a.Y;
                        double distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
                        double minDist = a.Radius / 2 + b.Radius / 2;

                        if (distance < minDist)
                        {
                            if (distance == 0) distance = 0.01;

                            double nx = distanceX / distance;
                            double ny = distanceY / distance;

                            double tx = -ny;
                            double ty = nx;

                            /*
                            double overlap = 0.5 * (minDist - distance + 0.1);
                            a.X -= overlap * nx;
                            a.Y -= overlap * ny;
                            b.X += overlap * nx;
                            b.Y += overlap * ny;
                            */

                            double velocityTanA = a.VelocityX * tx + a.VelocityY * ty;
                            double velocityTanB = b.VelocityX * tx + b.VelocityY * ty;

                            double velocityNormA = a.VelocityX * nx + a.VelocityY * ny;
                            double velocityNormB = b.VelocityX * nx + b.VelocityY * ny;

                            double m1 = a.Mass;
                            double m2 = b.Mass;

                            double newVelocityA = (velocityNormA * (m1 - m2) + 2 * m2 * velocityNormB) / (m1 + m2);
                            double newVelocityB = (velocityNormB * (m2 - m1) + 2 * m1 * velocityNormA) / (m1 + m2);

                            a.VelocityX = tx * velocityTanA + nx * newVelocityA;
                            a.VelocityY = ty * velocityTanA + ny * newVelocityA;
                            b.VelocityX = tx * velocityTanB + nx * newVelocityB;
                            b.VelocityY = ty * velocityTanB + ny * newVelocityB;
                        }
                    }
                }
            }
        }





        public event EventHandler<IEnumerable<DataAPI>> BallsUpdated;
    }
}
