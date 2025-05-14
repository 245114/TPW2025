using Data;
using Logic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

namespace Model
{
    public class Model : ModelAPI
    {
        public ObservableCollection<Ball> Balls => _logic.BallsCollection;

        public int canvasWidth { get; } = 1300;
        public int canvasHeight { get; } = 800;

        private readonly LogicAPI _logic;

        public Model()
        {
            _logic = new BallLogic(canvasWidth, canvasHeight);
        }

        public void DrawBalls(int count)
        {
            _logic.GenerateBalls(count);
        }

        public void SetCanvasSize(int width, int height)
        {
            _logic.SetCanvasSize(width, height);
        }
    }
}


