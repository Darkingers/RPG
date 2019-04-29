using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;


namespace RPG
{
    delegate void RenderDelegate(Graphics dest,int frameheight,int framewidth,int width,int height,Entity observer);

    public partial class Form1 : Form
    {
        private RenderDelegate Render;

        private Map Active_Map;
        private Player Active_Player;

        static int FrameWidth;
        static int FrameHeight;
        static int FramePart=20;

        List<string> Messages = new List<string>();
        Bitmap Background;
        

        Bitmap FrameUL;
        Bitmap FrameUR;
        Bitmap FrameDL;
        Bitmap FrameDR;

        Bitmap FrameL;
        Bitmap FrameU;
        Bitmap FrameR;
        Bitmap FrameD;


        public Form1()
        {
            Render = DummyRender;
            InitializeComponent();
            Messages.Add("Components initialized");
            GameTimer.Start(0.1);
            Database.Load();
            Messages.Add("Database Loaded");
            Initialize_Images();
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Messages.Add("Graphics initialized");
            Active_Player = (Player)Database.Get("Base:TestPlayer");
            Active_Map = (Map)Database.Get("Base:Small_map");
            Active_Map.Add_Player(Active_Player); 
            Render = Active_Map.Draw;
            GameTimer.Add(Tick);   
        }
        protected void Initialize_Images()
        {
            Background = (Bitmap)Database.Get("Base:Background");

            FrameUL = (Bitmap)Database.Get("Base:FrameUL");
            FrameUR = (Bitmap)Database.Get("Base:FrameUR");
            FrameDL = (Bitmap)Database.Get("Base:FrameDL");
            FrameDR = (Bitmap)Database.Get("Base:FrameDR");

            FrameU = (Bitmap)Database.Get("Base:FrameU");
            FrameL = (Bitmap)Database.Get("Base:FrameL");
            FrameD = (Bitmap)Database.Get("Base:FrameD");
            FrameR = (Bitmap)Database.Get("Base:FrameR");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Update_Stats();
            Update_Messages();
            base.OnPaint(e);
        }
        protected void Update_Stats()
        {
            
            if (Active_Player != null)
            {
                Stats.Clear();
                Stats.Text = Active_Player.Get_Details();
                Stats.Text= Stats.Text.Replace('_', ' ');
            }
        }
        protected void Update_Messages()
        {
            MessageBuffer.Clear();
            if (Messages.Count > 30)
            {
                Messages.RemoveRange(0, Messages.Count - 20);
            }
            for(int i = Messages.Count-1; i >= 0; i--)
            {
                MessageBuffer.Text += Messages[i] + Environment.NewLine;
            }
            MessageBuffer.Text=MessageBuffer.Text.Replace('_', ' '); 
        }
        public void Tick(System.Object sender, ElapsedEventArgs e)
        {
            Invalidate();
        }
        protected void Draw_Frame(Graphics Scene)
        {
            FrameHeight = Canvas.Height /FramePart;
            FrameWidth = Canvas.Width / FramePart;
            int WidthOffset = Canvas.Width % FramePart;
            int HeightOffset = Canvas.Height % FramePart;
            Canvas.Height = Canvas.Height - HeightOffset;
            Canvas.Width = Canvas.Width - WidthOffset;

            Rectangle dest = new Rectangle(FrameWidth, FrameHeight, Canvas.Width-FrameWidth, Canvas.Height-FrameHeight);
            Scene.DrawImage(Background,dest);

            dest = new Rectangle(0, 0, FrameWidth, FrameHeight);

            Scene.DrawImage(FrameUL, dest);
            for(dest.X =FrameWidth; dest.X < Canvas.Width - FrameWidth;dest.X+=FrameWidth)
            {
                Scene.DrawImage(FrameU, dest);
            }
            Scene.DrawImage(FrameUR, dest);
            for (dest.Y = FrameHeight; dest.Y < Canvas.Height - FrameHeight ; dest.Y += FrameHeight)
            {
                Scene.DrawImage(FrameR, dest);
            }
            Scene.DrawImage(FrameDR, dest);
            for (dest.X = dest.X - FrameWidth; dest.X >= FrameWidth; dest.X -= FrameWidth)
            {
                Scene.DrawImage(FrameD, dest);
            }
            Scene.DrawImage(FrameDL, dest);
            for (dest.Y = dest.Y - FrameHeight; dest.Y >= FrameHeight; dest.Y -= FrameHeight)
            {
                Scene.DrawImage(FrameL, dest);
            }
        }
        private void DummyRender(Graphics e,int Framewidth,int Frameheight,int width,int height,Entity observer)
        {

        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: Active_Player.Move(Direction.Up); break;
                case Keys.A: Active_Player.Move(Direction.Left); break;
                case Keys.S: Active_Player.Move(Direction.Down); break;
                case Keys.D: Active_Player.Move(Direction.Right); break;
            }
            Canvas.Invalidate();
        }

        private void Inventory_Click(object sender, EventArgs e)
        {

        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Draw_Frame(e.Graphics);
            Render(e.Graphics, FrameWidth, FrameHeight, Canvas.Width, Canvas.Height, Active_Player);
        }
    }
   
}
