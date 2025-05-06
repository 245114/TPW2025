using Data;

namespace Logic
{
    public interface LogicAPI
    {
        void GenerateBalls(int count);
        void UpdatePositions();
        event EventHandler<IEnumerable<DataAPI>> BallsUpdated;
        IReadOnlyList<DataAPI> Balls { get; }
        void AddBallToCollection(Ball ball);
        void SetCanvasSize(int width, int height);
    }
}
