using System.Diagnostics;

namespace Pong
{
    public partial class PongForm : Form
    {
        Rectangle background = new Rectangle(0, 0, 1000, 1000);
        Rectangle paddle1 = new Rectangle(10, 200, 10, 70);
        Rectangle paddle2 = new Rectangle(790, 200, 10, 70);
        Rectangle ball = new Rectangle(400, 200, 15, 15);
        PlayerInput player2 = new PlayerInput((int)Keys.Up, (int)Keys.Down);
        PlayerInput player1 = new PlayerInput((int)Keys.W, (int)Keys.S);
        int num = new Random().Next(1, 3);
        int ballSpeed = 15;

        public PongForm()
        {
            InitializeComponent();
        }

        private void BallDirectionX(int X)
        {
            ball = new Rectangle(ball.X + X, ball.Y, 15, 15);
        }

        public void GameLoop(object state)
        {
            Debug.Write($"Running {ball.X},{ball.Y}\n ");

            if (ball.X == paddle1.X && ball.Y <= paddle1.Y + 70)
            {
                ballSpeed = 15;
            }
            if (ball.X == paddle2.X && ball.Y <= paddle2.Y + 70)
            {
                ballSpeed = -15;
            }

            //Sync call
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Draw(g, background, GetBrushColor(Color.Black));
            Draw(g, paddle1, GetBrushColor(Color.Blue));
            Draw(g, paddle2, GetBrushColor(Color.Pink));
            BallDirectionX(ballSpeed);
            Draw(g, ball, GetBrushColor(Color.White));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            HandleInput(e, ref paddle1, player1);
            HandleInput(e, ref paddle2, player2);
            base.OnKeyDown(e);
        }

        private void HandleInput(KeyEventArgs e, ref Rectangle paddle, PlayerInput input)
        {
            if (e.KeyValue == input.InputDown)
            {
                if (paddle.Y < 410)
                {
                    Debug.Write($"Key Down\n {paddle.X},{paddle.Y}");
                    paddle = new Rectangle(paddle.X, paddle.Y + 30, paddle.Width, paddle.Height);
                    // Async call
                    Refresh();
                }
            }
            if (e.KeyValue == input.InputUp)
            {
                if (paddle.Y > 5)
                {
                    Debug.Write($"Key Down\n {paddle.X},{paddle.Y}");
                    paddle = new Rectangle(paddle.X, paddle.Y - 30, paddle.Width, paddle.Height);
                    // Async call
                    Refresh();
                }
            }
        }

        private static SolidBrush GetBrushColor(Color color)
        {
            return new SolidBrush(color);
        }

        private static void Draw(Graphics g, Rectangle rect, Brush brush)
        {
            g.FillRectangle(brush, rect);
        }

        public struct PlayerInput
        {
            public PlayerInput(int Up, int Down)
            {
                InputUp = Up;
                InputDown = Down;
            }

            public int InputUp { get; }
            public int InputDown { get; }
        }
    }
}