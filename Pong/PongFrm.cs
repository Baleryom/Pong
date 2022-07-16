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
        const int PADDLESIZE = 100;
        const int BALLSPEED = 2;
        DrawableObject background = new DrawableObject(0, 0, 1000, 1000);
        DrawableObject paddle1 = new DrawableObject(30, 200, 10, PADDLESIZE);
        DrawableObject paddle2 = new DrawableObject(770, 200, 10, PADDLESIZE);
        DrawableObject ball = new DrawableObject(400, 200, 15, 15);
        PlayerInput player2 = new PlayerInput((int)Keys.Up, (int)Keys.Down);
        PlayerInput player1 = new PlayerInput((int)Keys.W, (int)Keys.S);
        int score1 = 0;
        int score2 = 0;
        int ballSpeedX = BALLSPEED;
        int ballSpeedY = 0;
        bool Ai;

        public PongFrm(bool ai)
        {
            Ai = ai;
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
            ball = new DrawableObject(400, 200, 15, 15);
            paddle1 = new DrawableObject(30, 200, 10, PADDLESIZE);
            paddle2 = new DrawableObject(770, 200, 10, PADDLESIZE);
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
            if (Ai)
            {
                HandleAiInput(ref paddle2);
            }
            else
            {
                HandleInput(e, ref paddle2, player2);
            }

            base.OnKeyDown(e);
        }

        private void HandleInput(KeyEventArgs e, ref DrawableObject paddle, PlayerInput input)
        {
            if (e.KeyValue == input.InputDown)
            {
                if (paddle.Y < 370)
                {
                    Debug.Write($"Key Down\n {paddle.X},{paddle.Y}");
                    paddle = new DrawableObject(paddle.X, paddle.Y + 30, paddle.Width, paddle.Height);
                    // Async call
                    Refresh();
                }
            }
            if (e.KeyValue == input.InputUp)
            {
                if (paddle.Y > 15)
                {
                    Debug.Write($"Key Down\n {paddle.X},{paddle.Y}");
                    paddle = new DrawableObject(paddle.X, paddle.Y - 30, paddle.Width, paddle.Height);
                    // Async call
                    Refresh();
                }
            }
        }

        private void HandleAiInput(ref DrawableObject paddle)
        {
            paddle = new DrawableObject(paddle.X, ball.Y, paddle.Width, paddle.Height);
            Refresh();
        }

        private void BallDirection(int X, int Y)
        {
            ball = new DrawableObject(ball.X + X, ball.Y + Y, 15, 15);
        }

        private void HandlePhysics()
        {
            // if player 1 scores
            if (ball.X >= 782)
            {
                score1++;
                ResetGame();
            }
            // if player 2 scores
            if (ball.X <= 10)
            {
                score2++;
                ResetGame();
            }
            // if ball hits paddle 1
            if (ball.X <= paddle1.X && ball.Y <= paddle1.Y + PADDLESIZE && ball.Y >= paddle1.Y - PADDLESIZE)
            {
                ballSpeedX = BALLSPEED;
                if (ball.Y > paddle1.Y + PADDLESIZE / 2)
                    ballSpeedY += BALLSPEED;
                else if (ball.Y < paddle1.Y + PADDLESIZE / 2)
                    ballSpeedY -= BALLSPEED;
                else
                    ballSpeedY = 0;
            }
            // if ball hits paddle 2
            if (ball.X >= paddle2.X && ball.Y <= paddle2.Y + PADDLESIZE && ball.Y >= paddle2.Y - PADDLESIZE)
            {
                ballSpeedX = -BALLSPEED;
                if (ball.Y > paddle2.Y + PADDLESIZE / 2)
                    ballSpeedY = +BALLSPEED;
                else if (ball.Y < paddle2.Y + PADDLESIZE / 2)
                    ballSpeedY = -BALLSPEED;
                else
                    ballSpeedY = 0;
            }
            // if ball hits upper wall
            if (ball.Y <= 0)
                ballSpeedY = BALLSPEED * 2;
            // if ball hits lower wall
            if (ball.Y >= 440)
                ballSpeedY = -BALLSPEED * 2;
        }
    }
}