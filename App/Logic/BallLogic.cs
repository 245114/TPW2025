using System;
using System.Collections.Generic;
using System.Drawing;
using Data;
using System.Timers;
using System.Windows;
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

        private readonly Logger logger;

        private DateTime _lastUpdateTime = DateTime.Now;

        public BallLogic(int width, int height)
        {
            _width = width;
            _height = height;

            BallsCollection = new ObservableCollection<Ball>();
            logger = new Logger();
            logger.Start();

            
            timer = new Timer(10);
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        public void AddBallToCollection(Ball ball)
        {
            BallsCollection.Add(ball);
            logger.BallConstructor(ball);
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
                var now = DateTime.Now;
                double deltaTime = (now - _lastUpdateTime).TotalSeconds * 60;
                _lastUpdateTime = now;

                UpdatePositions(deltaTime);
            }
        }

        public void StartTimer()
        {
            timer.Start();
        }

        public IReadOnlyList<DataAPI> Balls => balls.AsReadOnly();

        public void GenerateBalls(int count)
        {
            SetCanvasSize(1268, 763);
            lock (_lock)
            {
                BallsCollection.Clear();
                balls.Clear();
                for (int i = 0; i < count; i++)
                {
                    double radiusTemp = random.Next(20, 50);
                    double massTemp = radiusTemp * 0.1f;
                    Ball ball = new(random.NextDouble() * _width, random.NextDouble() * _height, radiusTemp, random.NextDouble() * 5 - 2.5, random.NextDouble() * 5 - 2.5, massTemp , $"#{random.Next(0x1000000):X6}");
                    AddBallToCollection(ball);
                }
            }
        }


        public void UpdatePositions(double deltaTime)
        {
            lock (_lock)
            {
                double test = 4; //?
                foreach (var ball in BallsCollection)
                {
                    ball.move(deltaTime);

                    if (ball.X - ball.Radius / 2 <= 0)      //Lewa - dobrze
                    {
                        ball.VelocityX = Math.Abs(ball.VelocityX);
                        ball.X = ball.Radius / 2;
                        logger.BallCollisionWithWall(ball, "left");
                    }
                    else if (ball.X + ball.Radius / 2  >= _width) //Prawa - źle
                    {
                        ball.X = _width - ball.Radius / 2;
                        ball.VelocityX = -ball.VelocityX;
                        logger.BallCollisionWithWall(ball, "right");
                    }

                    if (ball.Y - ball.Radius / 2 <= 0)  //Góra - dobrze
                    {
                        ball.VelocityY = Math.Abs(ball.VelocityY);
                        ball.Y = ball.Radius / 2;
                        logger.BallCollisionWithWall(ball, "up");
                    }
                    else if (ball.Y + ball.Radius / 2 >= _height) //Dół - źle
                    {
                        ball.VelocityY = -Math.Abs(ball.VelocityY);
                        ball.Y = _height - ball.Radius / 2;
                        logger.BallCollisionWithWall(ball, "down");
                    }


                }
                Collisions();
                BallsUpdated?.Invoke(this, balls);
            }
        }

        private void Collisions()
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


                        double overlap = 0.3 * (minDist - distance + 0.1);
                        a.X -= overlap * nx;
                        a.Y -= overlap * ny;
                        b.X += overlap * nx;
                        b.Y += overlap * ny;


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

                        logger.BallCollisionWithBall(a, b);
                    }
                }
            }
        }

        public void StopIt()
        {
            logger.Stop();
        }


        public event EventHandler<IEnumerable<DataAPI>> BallsUpdated;
    }
}
