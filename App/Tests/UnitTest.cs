using Data;
using Logic;
using System.Threading;

namespace Tests
{

    public class UnitTest
    {
        [Fact]
        public void ballTest()
        {
            Ball ball = new Ball(10, 20, 30);

            Assert.Equal(10, ball.X);
        }

        [Fact]
        public void logicTest()
        {

            Ball ball = new(10, 20, 30);
            
            Assert.Equal(1, 1);
            Logic.Logic logic = new Logic.Logic();
            Assert.True(logic.isLogicAvailable());
        }
        
    }
}