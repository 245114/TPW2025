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
        private readonly List<DataAPI> balls = new();
        private readonly Random random = new();
        public ObservableCollection<Ball> BallsCollection { get; private set; }


        private readonly Timer timer;

        private int _width;
        private int _height;

        public int CanvasWidth { get; set; } = 800;
        public int CanvasHeight { get; set; } = 450;

        public BallLogic(int width, int height)
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

        public void SetCanvasSize(int width, int height)
        {
            _width = width;
            _height = height;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdatePositions();
        }

        public void StartTimer()
        {
            timer.Start();
        }

        public IReadOnlyList<DataAPI> Balls => balls.AsReadOnly();
        
        public void GenerateBalls(int count)
        {
            balls.Clear();
            for (int i = 0; i < count; i++)
            {
                Ball ball = new(random.NextDouble() * _width, random.NextDouble() * _height, 20,
                                                random.NextDouble() * 5 - 2.5, random.NextDouble() * 5 - 2.5);
                AddBallToCollection(ball);
                UpdatePositions();
                Debug.WriteLine("Hej");

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

        public event EventHandler<IEnumerable<DataAPI>> BallsUpdated;
    }
}
