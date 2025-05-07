using Data;
using Logic;
using System.Threading;

namespace Tests
{

    public class DataTest
    {
        

        [Fact]
        public void ballGetterTest()
        {
            Ball ball = new(10, 20, 30, 40, 50);

            Assert.Equal(10, ball.X);
            Assert.Equal(20, ball.Y);
            Assert.Equal(30, ball.Radius);
            Assert.Equal(40, ball.VelocityX);
            Assert.Equal(50, ball.VelocityY);
        }

        [Fact]
        public void BallSetterTest()
        {
            Ball ball = new(10, 20, 30, 40, 50);

            ball.X = 1;
            ball.Y = 2;
            ball.Radius = 3;
            ball.VelocityX = 4;
            ball.VelocityY = 5;

            Assert.Equal(1, ball.X);
            Assert.Equal(2, ball.Y);
            Assert.Equal(3, ball.Radius);
            Assert.Equal(4, ball.VelocityX);
            Assert.Equal(5, ball.VelocityY);

        }
        
    }
}