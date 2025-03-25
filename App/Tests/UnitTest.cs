using Data;
using Logic;
using System.Threading;

namespace Tests
{

    public class UnitTest
    {
        

        [Fact]
        public void Test()
        {

            Ball ball = new(10, 20, 30);
            
            Assert.Equal(1, 1);
        }
        
    }
}