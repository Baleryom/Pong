using System.Diagnostics;

namespace Pong
{
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

    public partial class PongFrm : MenuFrm
    {
        const int PADDLESIZE_X = 100;
        const int BALL_SPEED = 2;
        DrawableObject background = new DrawableObject(0, 0, 1000, 1000);
        Paddle paddle1 = new Paddle(10, 200, 10, PADDLESIZE_X);
        Paddle paddle2 = new Paddle(780, 200, 10, PADDLESIZE_X);
        Ball ball = new Ball(400, 200, 15, 15);
        PlayerInput player2 = new PlayerInput((int)Keys.Up, (int)Keys.Down);
        PlayerInput player1 = new PlayerInput((int)Keys.W, (int)Keys.S);
        int score1 = 0;
        int score2 = 0;
        int ballSpeedX = BALL_SPEED;
        int ballSpeedY = 0;

        public PongFrm()
        {
            InitializeComponent();
        }

        public void GameLoop(object state)
        {
            Debug.Write($"Running {ball.X},{ball.Y}\n ");
            //Checks if ball collides with paddles and calculates ball direction
            HandlePhysics();
            //Sync call
            Invalidate();
        }

        private void ResetGame()
        {
            ball = new Ball(400, 200, 15, 15);
            paddle1 = new Paddle(10, 200, 10, PADDLESIZE_X);
            paddle2 = new Paddle(780, 200, 10, PADDLESIZE_X);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            background.Draw(g, background.Rect, Color.Black);
            paddle1.Draw(g, paddle1.Rect, Color.Blue);
            paddle2.Draw(g, paddle2.Rect, Color.Pink);
            BallDirection(ballSpeedX, ballSpeedY);
            ball.Draw(g, ball.Rect, Color.White);
            g.DrawString(score1.ToString() + ":" + score2.ToString(), new Font("Times New Roman", 25.0f), background.GetBrushColor(Color.Gray), new PointF(350f, 30f));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            HandleInput(e, ref paddle1, player1);
            HandleInput(e, ref paddle2, player2);
            base.OnKeyDown(e);
        }

        private void HandleInput(KeyEventArgs e, ref Paddle paddle, PlayerInput input)
        {
            if (e.KeyValue == input.InputDown)
            {
                if (paddle.Y < 370)
                {
                    Debug.Write($"Key Down\n {paddle.X},{paddle.Y}");
                    paddle = new Paddle(paddle.X, paddle.Y + 30, paddle.Width, paddle.Height);
                    // Async call
                    Refresh();
                }
            }
            if (e.KeyValue == input.InputUp)
            {
                if (paddle.Y > 15)
                {
                    Debug.Write($"Key Down\n {paddle.X},{paddle.Y}");
                    paddle = new Paddle(paddle.X, paddle.Y - 30, paddle.Width, paddle.Height);
                    // Async call
                    Refresh();
                }
            }
        }

        private void BallDirection(int X, int Y)
        {
            ball = new Ball(ball.X + X, ball.Y + Y, 15, 15);
        }

        private void HandlePhysics()
        {
            // if player 1 scores
            if (ball.X >= 800)
            {
                score1++;
                ResetGame();
            }
            // if player 2 scores
            if (ball.X <= 5)
            {
                score2++;
                ResetGame();
            }
            // if ball hits paddle 1
            if (ball.X <= paddle1.X && ball.Y <= paddle1.Y + PADDLESIZE_X)
            {
                ballSpeedX = BALL_SPEED;
                if (ball.Y > paddle1.Y + 35)
                    ballSpeedY += BALL_SPEED;
                else if (ball.Y < paddle1.Y + 35)
                    ballSpeedY -= BALL_SPEED;
                else
                    ballSpeedY = 0;
            }
            // if ball hits paddle 2
            if (ball.X >= paddle2.X && ball.Y <= paddle2.Y + PADDLESIZE_X)
            {
                ballSpeedX = -BALL_SPEED;
                if (ball.Y > paddle2.Y + PADDLESIZE_X / 2)
                    ballSpeedY = +BALL_SPEED;
                else if (ball.Y < paddle2.Y + PADDLESIZE_X / 2)
                    ballSpeedY = -BALL_SPEED;
                else
                    ballSpeedY = 0;
            }
            // if ball hits upper wall
            if (ball.Y <= 0)
                ballSpeedY = BALL_SPEED;
            // if ball hits lower wall
            if (ball.Y >= 440)
                ballSpeedY = -BALL_SPEED;
        }
    }
}