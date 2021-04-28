namespace API.Entities
{
    public class MyPoint
    {
        public MyPoint()
        {
        }

        public MyPoint(int y, int x)
        {
            Y = y;
            X = x;
        }

        int Y {get;set;}
        int X {get;set;}
        
        public int getX()
        {
            return X;
        }
        public int getY()
        {
            return Y;
        }
    }
}