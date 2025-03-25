namespace Model
{
    public class Ball
    {
        public double X;
        public double Y;

        public Ball(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double getX() { 
            return X;  
        }

        public double getY()
        {
            return Y;
        }
    }
}
