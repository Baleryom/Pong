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
        int score1 = 0;
        int score2 = 0;
        int ballSpeedX = 15;
        int ballSpeedY = 0;

        public PongForm()
        {
            InitializeComponent();
        }

        private void BallDirectionX(int X, int Y)
        {
            ball = new Rectangle(ball.X + X, ball.Y + Y, 15, 15);
        }

        public void GameLoop(object state)
        {
            Debug.Write($"Running {ball.X},{ball.Y}\n ");
            //Checks if ball collides with paddles and calculates ball direction
            HandlePhysics();

            //Sync call
            Invalidate();
        }

        private void HandlePhysics()
        {
            // if ball hits paddle 1
            if (ball.X == paddle1.X && ball.Y <= paddle1.Y + 70)
            {
                ballSpeedX = 15;
                if (ball.Y > paddle1.Y + 35)
                {
                    ballSpeedY += 15;
                }
                else if (ball.Y < paddle1.Y + 35)
                {
                    ballSpeedY -= 15;
                }
                else
                {
                    ballSpeedY = 0;
                }
            }
            // if ball hits paddle 2
            if (ball.X == paddle2.X && ball.Y <= paddle2.Y + 70)
            {
                ballSpeedX = -15;
                if (ball.Y > paddle2.Y + 35)
                    ballSpeedY = +15;
                else if (ball.Y < paddle2.Y + 35)
                    ballSpeedY = -15;
                else
                    ballSpeedY = 0;
            }
            // if ball hits upper wall
            if (ball.Y <= 0)
                ballSpeedY = 30;
            // if ball hits lower wall
            if (ball.Y >= 440)
                ballSpeedY = -30;
            // if player 1 scores
            if (ball.X > 800)
            {
                ball = new Rectangle(400, 200, 15, 15);
                score1++;
            }
            // if player 2 scores
            if (ball.X < 2)
            {
                ball = new Rectangle(400, 200, 15, 15);
                score2++;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Draw(g, background, GetBrushColor(Color.Black));
            Draw(g, paddle1, GetBrushColor(Color.Blue));
            Draw(g, paddle2, GetBrushColor(Color.Pink));
            BallDirectionX(ballSpeedX, ballSpeedY);
            Draw(g, ball, GetBrushColor(Color.White));
            g.DrawString(score1.ToString() + ":" + score2.ToString(), new Font("Times New Roman", 25.0f), GetBrushColor(Color.Gray), new PointF(350f, 30f));
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