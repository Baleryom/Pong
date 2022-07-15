namespace Pong
{
    public class DrawableObject
    {
        public DrawableObject(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Rectangle Rect
        {
            get { return new Rectangle(X, Y, Width, Height); }
            set { Rect = value; }
        }
    }
}
