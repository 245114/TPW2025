using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public interface BallLogicInterface
    {
        void GenerateBalls(int count);
        void UpdatePositions();
        event EventHandler<IEnumerable<BallInterface>> BallsUpdated;
        IReadOnlyList<BallInterface> Balls { get; }
        void AddBallToCollection(Ball ball);


    }
}
