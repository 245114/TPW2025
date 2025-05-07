using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Logic;

namespace Tests
{
    public class LogicTest
    {
        [Fact]
        public void GenerateBallsTest()
        {
            BallLogic logic = new BallLogic(800, 600);

            logic.GenerateBalls(5);

            Assert.Equal(5, logic.BallsCollection.Count);
        }

        [Fact]
        public void SetCanvasSizeTest()
        {
            BallLogic logic = new BallLogic(800, 600);

            logic.SetCanvasSize(1000, 700);

            logic.GenerateBalls(1);
            var ball = logic.BallsCollection.First();

            Assert.True(ball.X + ball.Radius * 2 <= 1000);
            Assert.True(ball.Y + ball.Radius * 2 <= 700);
        }

        [Fact]
        public void AddBallsToCollectionTest()
        {
            BallLogic logic = new BallLogic(800, 450);
            Ball ball = new Ball(10, 20, 30, 40, 50);

            logic.AddBallToCollection(ball);

            Assert.Contains(ball, logic.BallsCollection);
        }

        [Fact]
        public void UpdatePositionsTest()
        {
            BallLogic logic = new BallLogic(800, 450);
            Ball ball = new Ball(100, 100, 1, 2, 3);
            logic.AddBallToCollection(ball);

            double oldX = ball.X;
            double oldY = ball.Y;

            logic.UpdatePositions();

            Assert.Equal(oldX + 2, ball.X);
            Assert.Equal(oldY + 3, ball.Y);
        }

        [Fact]
        public void UpdatePositionsWallTest()
        {
            BallLogic logic = new BallLogic(100, 100);
            Ball ball = new Ball(95, 50, 5, 5, 0);
            logic.AddBallToCollection(ball);

            logic.UpdatePositions();

            Assert.True(ball.VelocityX < 0);
        }
    }
}
