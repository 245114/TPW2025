using Data;
using Logic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace Model
{
    public class Model
    {
        public ObservableCollection<Ball> Balls { get; set; }
        public int canvasWidth { get; } = 800;
        public int canvasHeight { get; } = 450;

        private readonly BallLogicInterface _logic;

        public Model()
        {
            Balls = new ObservableCollection<Ball>();
            _logic = new BallLogic(canvasWidth, canvasHeight);
        }

        public void DrawBalls(int count)
        {
            Balls.Clear();
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                Ball ball = new(random.NextDouble() * canvasWidth, random.NextDouble() * canvasHeight, 20,
                                random.NextDouble() * 5 - 2.5, random.NextDouble() * 5 - 2.5);
                Balls.Add(ball);
                _logic.AddBallToCollection(ball);
                _logic.UpdatePositions();
            }
        }
    }
}


