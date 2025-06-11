using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Data;

namespace Logic
{
    public interface LogicAPI
    {
        void GenerateBalls(int count);
        void UpdatePositions(double deltaTime);
        event EventHandler<IEnumerable<DataAPI>> BallsUpdated;
        IReadOnlyList<DataAPI> Balls { get; }
        void AddBallToCollection(Ball ball);
        void SetCanvasSize(int width, int height);
        ObservableCollection<Ball> BallsCollection { get; }
        //void move(Ball ball, double deltaTime);
        void StopIt();
    }
}
