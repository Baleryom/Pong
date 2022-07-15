namespace Pong
{
    public class Ball : DrawableObject
    {
        private Paddle Paddle1;
        private Paddle Paddle2;
        public Ball(int x, int y, int width, int height) : base(x, y, width, height)
        {
        }
    }
}
