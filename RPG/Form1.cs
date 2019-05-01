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
    delegate Dictionary<Rectangle,Tile> RenderDelegate(Graphics dest,int frameheight,int framewidth,int width,int height,Entity observer);

    public partial class Form1 : Form
    {
        private List<Skill> Bindings = new List<Skill>()
        {
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        };
        private int Indexed = 0;

        private RenderDelegate Render;
        private RenderDelegate Previous_Render;

        private Map Active_Map;
        private Player Active_Player;
        private Dictionary<Rectangle, Tile> TileMap = new Dictionary<Rectangle, Tile>();
        private Tile Detailed;

        private static int FrameWidth;
        private static int FrameHeight;
        private static int FramePart=20;

        private ListViewItem SelectedItem=null;
        private ListView SelectedList=null;
        private Tile SelectedTile=null;
        private Button SelectedButton = null;

        private List<string> Messages = new List<string>();
        private Bitmap Background;


        private Bitmap FrameUL;
        private Bitmap FrameUR;
        private Bitmap FrameDL;
        private Bitmap FrameDR;

        private Bitmap FrameL;
        private Bitmap FrameU;
        private Bitmap FrameR;
        private Bitmap FrameD;


        public Form1()
        {
            Render = Dummy;
            InitializeComponent();
            Add_Message("Components initialized");
            GameTimer.Start(0.1);
            Database.Load();
            Add_Message( "Database Loaded");
            Initialize_Images();
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Add_Message("Graphics initialized");
            Active_Player = (Player)Database.Get("Base:TestPlayer");
            Active_Map = (Map)Database.Get("Base:Small_map");
            Active_Map.Add_Player(Active_Player); 
            Render = Active_Map.RenderMap;
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
            if (Detailed != null)
            {
                Details.Text = Detailed.Get_Details();
            }
            
            Update_Stats();
            base.OnPaint(e);
        }
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Draw_Frame(e.Graphics);
            TileMap=Render(e.Graphics, FrameWidth, FrameHeight, Canvas.Width, Canvas.Height, Active_Player);
        }
        protected void Draw_Frame(Graphics Scene)
        {
            FrameHeight = Canvas.Height / FramePart;
            FrameWidth = Canvas.Width / FramePart;
            int WidthOffset = Canvas.Width % FramePart;
            int HeightOffset = Canvas.Height % FramePart;
            Canvas.Height = Canvas.Height - HeightOffset;
            Canvas.Width = Canvas.Width - WidthOffset;

            Rectangle dest = new Rectangle(FrameWidth, FrameHeight, Canvas.Width - FrameWidth, Canvas.Height - FrameHeight);
            Scene.DrawImage(Background, dest);

            dest = new Rectangle(0, 0, FrameWidth, FrameHeight);

            Scene.DrawImage(FrameUL, dest);
            for (dest.X = FrameWidth; dest.X < Canvas.Width - FrameWidth; dest.X += FrameWidth)
            {
                Scene.DrawImage(FrameU, dest);
            }
            Scene.DrawImage(FrameUR, dest);
            for (dest.Y = FrameHeight; dest.Y < Canvas.Height - FrameHeight; dest.Y += FrameHeight)
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
        private Dictionary<Rectangle,Tile> Dummy(Graphics e, int Framewidth, int Frameheight, int width, int height, Entity observer)
        {
            return null;
        }

        public void Add_Message(string line)
        {
            MessageBuffer.Text = MessageBuffer.Text.Insert(0,line+ System.Environment.NewLine );
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: Active_Player.Move(Direction.Up); break;
                case Keys.A: Active_Player.Move(Direction.Left); break;
                case Keys.S: Active_Player.Move(Direction.Down); break;
                case Keys.D: Active_Player.Move(Direction.Right); break;

                case Keys.D0: Try_Skill(0); break;
                case Keys.D1: Try_Skill(1); break;
                case Keys.D2: Try_Skill(2); break;
                case Keys.D3: Try_Skill(3); break;
                case Keys.D4: Try_Skill(4); break;
                case Keys.D5: Try_Skill(5); break;
                case Keys.D6: Try_Skill(6); break;
                case Keys.D7: Try_Skill(7); break;
                case Keys.D8: Try_Skill(8); break;
                case Keys.D9: Try_Skill(9); break;

                case Keys.I: Toggle_Inventory(); break;
                case Keys.Delete:DeleteItem();break;
            }
            Canvas.Invalidate();
        }
        protected void Try_Skill(int index)
        {
            if (Bindings[index] == null)
            {
                return;
            }
            Bindings[index].Try_Use(Active_Player, SelectedTile);
        }
        protected void Update_Stats()
        {

            if (Active_Player != null)
            {
                Stats.Clear();
                Stats.Text = Active_Player.Get_Details();
                Stats.Text = Stats.Text.Replace('_', ' ');
            }
        }
        public void Tick(System.Object sender, ElapsedEventArgs e)
        { 
            Invalidate();
        }
        
        private void Inventory_Click(object sender, EventArgs e)
        {
            Toggle_Inventory();
            Canvas.Invalidate();
        }
        private void Toggle_Inventory()
        {
            InventoryPanel.Visible = !InventoryPanel.Visible;
            if (InventoryPanel.Visible && Active_Player != null)
            {
                SkillView.Visible = false;
                Refresh_Inventory();
            }
        }
        private void Refresh_Inventory()
        {
            List<Item> inventory = Active_Player.Get_Inventory();
            List<Item> equipment = Active_Player.Get_Equipment().Values.ToList();
            List<Slot> key = Active_Player.Get_Equipment().Keys.ToList();

            InventoryList.Items.Clear();
            EquipmentList.Items.Clear();

            ImageList iimages = new ImageList();
            ImageList eimages = new ImageList();

            iimages.ImageSize = new Size(50,50);
            eimages.ImageSize = new Size(50, 50);

            InventoryList.LargeImageList = iimages;
            EquipmentList.SmallImageList = eimages;

            for (int i = 0; i < inventory.Count; i++)
            {
                ListViewItem added = new ListViewItem();
                iimages.Images.Add(inventory[i].Get_Identifier(),inventory[i].Get_Image());
                added.Tag = inventory[i];
                added.ImageKey = inventory[i].Get_Identifier();
                added.ToolTipText = inventory[i].Get_Details();
                InventoryList.Items.Add(added);
            }
            eimages.Images.Add("Base:Empty_Slot",(Bitmap)Database.Get("Base:Empty_Slot"));
            for (int i = 0; i < equipment.Count; i++)
            {
                ListViewItem added = new ListViewItem();
                if (equipment[i] == null)
                {
                    added.ImageKey = "Base:Empty_Slot";
                    added.Text = key[i].Get_Identifier();
                }
                else
                {
                    eimages.Images.Add(equipment[i].Get_Identifier(),equipment[i].Get_Image());
                    added.ImageKey = equipment[i].Get_Identifier();
                    added.Text = equipment[i].Get_Slot().Get_Identifier();
                    added.ToolTipText = equipment[i].Get_Details();
                    added.Tag = equipment[i];
                }
                EquipmentList.Items.Add(added);
            }
        }
        private void EquipmentList_ItemDrag(object sender, ItemDragEventArgs e)
        {
            SelectedItem = (ListViewItem)e.Item;
            SelectedList = EquipmentList;
            EquipmentList.DoDragDrop(e.Item, DragDropEffects.Move);
        }
        private void InventoryList_ItemDrag(object sender, ItemDragEventArgs e)
        {
            SelectedItem = (ListViewItem)e.Item;
            SelectedList = InventoryList;
            InventoryList.DoDragDrop(e.Item, DragDropEffects.Move);
        }
        private void EquipmentList_DragEnter(object sender, DragEventArgs e)
        {
            if (SelectedList != EquipmentList && SelectedItem!=null)
            {
                e.Effect = DragDropEffects.Move;
                Item temp = (Item)SelectedItem.Tag;
                Active_Player.Equip(temp);
                Active_Player.Remove_Item(temp);
                InventoryList.Items.Remove(SelectedItem);
                if (!EquipmentList.SmallImageList.Images.ContainsKey(temp.Get_Identifier()))
                {
                    EquipmentList.SmallImageList.Images.Add(temp.Get_Identifier(), temp.Get_Image());
                }
                foreach (ListViewItem item in EquipmentList.Items)
                {
                    if (item.Text == temp.Get_Slot().Get_Identifier()) 
                    {
                        item.Tag = temp;
                        item.ImageKey = temp.Get_Identifier();
                        break;
                    }
                }
                SelectedItem = null;
            }
        }
        private void InventoryList_DragEnter(object sender, DragEventArgs e)
        {
            if (SelectedList != InventoryList &&  SelectedItem.Tag!=null)
            {
                e.Effect = DragDropEffects.Move;
                Item temp = (Item)SelectedItem.Tag;
                Active_Player.UnEquip(temp);
                Active_Player.Add_Item(temp);
                if (!InventoryList.LargeImageList.Images.ContainsKey(temp.Get_Identifier()))
                {
                    InventoryList.LargeImageList.Images.Add(temp.Get_Identifier(), temp.Get_Image());
                }
                ListViewItem Added = new ListViewItem();
                Added.Tag = temp;
                Added.ImageKey = temp.Get_Identifier();
                foreach(ListViewItem item in EquipmentList.Items)
                {
                    if(item.Text== temp.Get_Slot().Get_Identifier())
                    {
                        item.Tag = null;
                        item.ImageKey = "Base:Empty_Slot";
                        break;
                    }
                }
                InventoryList.Items.Add(Added);
                SelectedItem = null;
            }
        }
        private void InventoryList_ItemActivate(object sender, EventArgs e)
        {
            if (InventoryList.SelectedItems.Count == 0)
            {
                return;
            }
            SelectedItem = InventoryList.SelectedItems[0];
            SelectedList = InventoryList;
        }
        private void EquipmentList_ItemActivate(object sender, EventArgs e)
        {
            SelectedItem = EquipmentList.SelectedItems[0];
            SelectedList = EquipmentList;
        }
        private void DeleteItem()
        {
            if (SelectedItem != null)
            {
                Item temp = (Item)SelectedItem.Tag;
                if (SelectedList == InventoryList)
                {
                    Active_Player.Remove_Item(temp);
                    InventoryList.Items.Remove(SelectedItem);
                }
                else
                {
                    Active_Player.UnEquip(temp);
                    foreach(ListViewItem item in EquipmentList.Items)
                    {
                        if (item.Text == temp.Get_Slot().Get_Identifier())
                        {
                            item.Tag = null;
                            item.ImageKey = "Base:Empty_Slot";
                            break;
                        }
                    }
                }
                SelectedItem = null;
            }
            
        }

        private void Skill0_Click(object sender, EventArgs e)
        {
            SelectedButton = Skill0;
            Indexed = 0;
            ToggleSkillView();
        }
        private void Skill1_Click(object sender, EventArgs e)
        {
            SelectedButton = Skill1;
            Indexed = 1;
            ToggleSkillView();
        }
        private void Skill2_Click(object sender, EventArgs e)
        {
            SelectedButton = Skill2;
            Indexed = 2;
            ToggleSkillView();
        }
        private void Skill3_Click(object sender, EventArgs e)
        {
            SelectedButton = Skill3;
            Indexed = 3;
            ToggleSkillView();
        }
        private void Skill4_Click(object sender, EventArgs e)
        {
            SelectedButton = Skill4;
            Indexed = 4;
            ToggleSkillView();
        }
        private void Skill5_Click(object sender, EventArgs e)
        {
            SelectedButton = Skill5;
            Indexed = 5;
            ToggleSkillView();
        }
        private void Skill6_Click(object sender, EventArgs e)
        {
            SelectedButton = Skill6;
            Indexed = 6;
            ToggleSkillView();
        }
        private void Skill7_Click(object sender, EventArgs e)
        {
            SelectedButton = Skill7;
            Indexed = 7;
            ToggleSkillView();
        }
        private void Skill8_Click(object sender, EventArgs e)
        {
            SelectedButton = Skill8;
            Indexed = 8;
            ToggleSkillView();
        }
        private void Skill9_Click(object sender, EventArgs e)
        {
            SelectedButton = Skill9;
            Indexed = 9;
            ToggleSkillView();
        }
        private void ToggleSkillView()
        {
            SkillView.Visible = !SkillView.Visible;
            if (SkillView.Visible && Active_Player!=null)
            {
                InventoryPanel.Visible = false;
                ImageList img = new ImageList();
                img.ImageSize = new Size(50, 50);
                List<Skill> skills = Active_Player.Get_Skills();
                SkillView.LargeImageList = img;
                SkillView.Items.Clear();
                for(int i = 0; i <skills.Count; i++)
                {
                    ListViewItem added = new ListViewItem();
                    added.Tag = skills[i];
                    added.ImageKey = skills[i].Get_Identifier();
                    added.ToolTipText = skills[i].Get_Details();
                    img.Images.Add(skills[i].Get_Identifier(), skills[i].Get_Image());
                    SkillView.Items.Add(added);
                }
            }
        }

        private void SkillView_ItemActivate(object sender, EventArgs e)
        {
            SelectedButton.BackgroundImage= ((Skill)SkillView.SelectedItems[0].Tag).Get_Image();
            Bindings[Indexed] = (Skill)SkillView.SelectedItems[0].Tag;
            SelectedButton.Invalidate();
            SkillView.Visible = false;
        }
        private void Canvas_DoubleClick(object sender, EventArgs e)
        {
            


        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {
            
            List<Rectangle> keys = TileMap.Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                if (e.X>=keys[i].X && e.X<=keys[i].X+keys[i].Width && e.Y >= keys[i].Y && e.Y <= keys[i].Y + keys[i].Height)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (SelectedTile != null)
                        {
                            SelectedTile.UnSelect();
                        }
                        SelectedTile = TileMap[keys[i]];
                        SelectedTile.Select();
                        Canvas.Invalidate();
                        return;
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        Detailed=TileMap[keys[i]];
                    }
                    else
                    {

                    }
                }
            }
        }

        private void Maps_Click(object sender, EventArgs e)
        {
            ToggleMaps();
            Maps.Invalidate();
        }
        private void ToggleMaps()
        {
            Maps.Visible = !Maps.Visible;
            if (Maps.Visible)
            {




            }
        }
    }
}
