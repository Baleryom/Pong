using System.Diagnostics;

namespace Pong
{
    public partial class PongForm : Form
    {
        Rectangle background = new Rectangle(0, 0, 1000, 1000);
        Rectangle paddle1 = new Rectangle(5, 200, 5, 45);
        Rectangle paddle2 = new Rectangle(790, 200, 5, 45);
        PlayerInput player1 = new PlayerInput((int)Keys.Up,(int)Keys.Down);
        PlayerInput player2 = new PlayerInput((int)Keys.W, (int)Keys.S);

        public PongForm()
        {
            InitializeComponent();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {        
            Graphics g = e.Graphics;
            Draw(g, background, GetBrushColor(Color.Black));
            Draw(g, paddle1, GetBrushColor(Color.White));
            Draw(g, paddle2, GetBrushColor(Color.White));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            HandleInput(e,ref paddle1,player1);
            HandleInput(e,ref paddle2,player2);
            base.OnKeyDown(e);
        }

        private void HandleInput(KeyEventArgs e,ref Rectangle paddle, PlayerInput input)
        {
            if (e.KeyValue == input.InputDown)
            {
                if (paddle.Y < 410)
                {
                    Debug.Write("Key Down\n");
                    paddle = new Rectangle(paddle.X, paddle.Y + 15, paddle.Width, paddle.Height);
                    Debug.Write(paddle.Y);
                    Refresh();
                }
            }
            if (e.KeyValue == input.InputUp)
            {
                if (paddle.Y > 5)
                {
                    Debug.Write("Key Up\n");
                    paddle = new Rectangle(paddle.X, paddle.Y - 15, paddle.Width, paddle.Height);
                    Debug.Write(paddle.Y);
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