using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RPG
{
    public partial class Form1 : Form
    {
        private Map active_map;
        private Player active_player;
        private Bitmap background;

        private static Tile selector;
        private static int zoom_speed = 10;
        private int prev_width=0;
        private int prev_height=0;
        private Skill test;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Width = 1200;
            Height = 800;
            GameTimer.Start(1);
            Database.Load();
            Map.Load_Frame();
            background = Database.Get_Image("Test:Form.png");
            active_map = (Map)Database.Get("Test:Testmap");
            active_player = (Player)Database.Get("Test:Player").Clone();
            active_map.Add_Player(active_player);
            selector = active_map.Get(0, 0);
            selector.Select();
            this.MouseWheel += Form1_Zoom;
            Update_Stats();
            prev_height = Height;
            prev_width = Width;
            test =(Skill) Database.Get("Test:TestSkill");
            active_player.Add_Skill(test);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Update_Stats();
            Update_Details();
            Update_Skills();
            base.OnPaint(e);
            
            e.Graphics.DrawImage(background,0, 0, Width, Height);
            active_map.Draw(e.Graphics, active_player);
            Focus_Selector();
        }
        private void Update_Stats()
        {
            StatWatcher.Text = "";
            List <Stat> stats = active_player.Get_Stats();
            StatWatcher.Text += Environment.NewLine + active_player.Get_Stat_List();
        }
        private void Update_Details()
        {
            Details.Text = "Details" + Environment.NewLine + selector.Get_Details();
        }
        private void Update_Skills()
        {
            Skills.Text = active_player.Get_Skill_List();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: active_player.Move(Direction.Up); break;
                case Keys.A: active_player.Move(Direction.Left); break;
                case Keys.S: active_player.Move(Direction.Down); break;
                case Keys.D: active_player.Move(Direction.Right); break;
                case Keys.NumPad8: Move_Selector(Direction.Up); break;
                case Keys.NumPad2: Move_Selector(Direction.Down); break;
                case Keys.NumPad6: Move_Selector(Direction.Right); break;
                case Keys.NumPad4: Move_Selector(Direction.Left); break;
                case Keys.NumPad5: test.Try_Use(active_player, active_player); break;
            }

            Invalidate();
        }
        private void Move_Selector(Direction direction)
        {
            Tile next = selector.Get_Neighbour(direction);
            if (next != null)
            {
                selector.UnSelect();
                selector = next;
                next.Select();
                Invalidate();
            }
        }
        private void Focus_Selector()
        {
            if (selector.Get_Position().X < active_player.Get_Position().X - active_player.Get_Sight() ||
               selector.Get_Position().X > active_player.Get_Position().X + active_player.Get_Sight() ||
               selector.Get_Position().Y < active_player.Get_Position().Y - active_player.Get_Sight() ||
               selector.Get_Position().Y > active_player.Get_Position().Y + active_player.Get_Sight()
                )
            {
                selector.UnSelect();
                selector = active_player.Get_Tile();
                selector.Select();
                Invalidate();
            }
        }
        private void Form1_Zoom(object sender, MouseEventArgs e)
        {
            int modifier = (int)(e.Delta / 120);
            switch (modifier)
            {
                case -1: Tile.Resize(Tile.Get_Width() + zoom_speed, Tile.Get_Height() + zoom_speed); break;//Zoom
                case 1: Tile.Resize(Tile.Get_Width() - zoom_speed, Tile.Get_Height() - zoom_speed); break; 
            }
            Invalidate();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (prev_width > 0 && prev_height>0)
            {
                double vertical = Height /(double)prev_height;
                double horizontal = Width /(double)prev_width;
                Map.Resize(horizontal,vertical);
            }
            Invalidate();
            prev_width = Width;
            prev_height = Height;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
   
}
