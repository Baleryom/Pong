namespace Pong
{
    public partial class MenuFrm : Form
    {
        DrawableObject background = new DrawableObject(0, 0, 1000, 1000);
        private Color onePlayerColor = Color.LightGray;
        private Color twoPlayerColor = Color.Gray;

        public MenuFrm()
        {
            InitializeComponent();
        }

        // Enables double buffering for all the controls ( fixes screen flickering drawString )
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleparam = base.CreateParams;
                handleparam.ExStyle |= 0x02000000;
                return handleparam;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            background.Draw(g, background.Rect, Color.Black);
            g.DrawString("Pong", new Font("Times New Roman", 25.0f), background.GetBrushColor(Color.Gray), new PointF(350f, 50f));
            g.DrawString("  1P  ", new Font("Times New Roman", 25.0f), background.GetBrushColor(onePlayerColor), new PointF(350f, 100f));
            g.DrawString("  2P  ", new Font("Times New Roman", 25.0f), background.GetBrushColor(twoPlayerColor), new PointF(350f, 150f));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Down || e.KeyValue == (int)Keys.S)
            {
                SwapColors(ref onePlayerColor, ref twoPlayerColor);
                Refresh();
            }
            if (e.KeyValue == (int)Keys.Up || e.KeyValue == (int)Keys.W)
            {
                SwapColors(ref onePlayerColor, ref twoPlayerColor);
                Refresh();
            }
            if (e.KeyValue == (int)Keys.Enter)
            {
                if (twoPlayerColor == Color.LightGray)
                {
                    this.Hide();
                    new PongFrm().Show();
                }
            }
            base.OnKeyDown(e);
        }

        private void SwapColors(ref Color colorA, ref Color colorB)
        {
            var aux = colorA;
            colorA = colorB;
            colorB = aux;
        }
    }
}
