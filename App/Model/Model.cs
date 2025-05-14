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

        public int canvasWidth { get; } = 800;
        public int canvasHeight { get; } = 450;

        private readonly LogicAPI _logic;

        public Model(LogicAPI logic)
        {
            _logic = new BallLogic(canvasWidth, canvasHeight);
        }

        public Model()
        {
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


