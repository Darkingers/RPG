using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Map:Type
    {

        protected List<List<Tile>> tiles;

        protected Point starting_position;

        protected static Bitmap FrameCornerUL;
        protected static Bitmap FrameCornerUR;
        protected static Bitmap FrameCornerDL;
        protected static Bitmap FrameCornerDR;
        protected static Bitmap FramePartL;
        protected static Bitmap FramePartR;
        protected static Bitmap FramePartU;
        protected static Bitmap FramePartD;
        protected static Bitmap Background;
        protected static int Map_X = 300;
        protected static int Map_Y = 50;
        protected static int Frame_Width = 50;
        protected static int Frame_Height = 50;
        protected static int Map_Width = 500;
        protected static int Map_Height = 500;

        public Map(Mod _mod = null
            , string _name = ""
            , string _description = ""
            , List<Tag> _tags = null
            , List<List<Tile>> _tiles=null
            ):base(_mod,_name,_description,_tags)
        {
            starting_position = new Point(1, 1);
            if (_tiles == null)
            {
                tiles = new List<List<Tile>>();
            }
            else
            {
                tiles = _tiles;
            }
            
        }
        public override void Load(string[] args, ref int iter, Mod _mod)
        {
            Dictionary<int, string> replacer = new Dictionary<int, string>();
            bool replacerexists = false;
            for (string token = args[iter]; token != "[END]"; token = args[++iter])
            {
                switch (token)
                {
                    case "[Type]": base.Load(args, ref iter, Source); break;
                    case "[Tiles]": int h = int.Parse(args[++iter]); int w = int.Parse(args[++iter]);for(int i = 0; i < h; i++)
                        {
                            tiles.Add(new List<Tile>());
                            for(int j = 0; j < w; j++)
                            {
                                Tile added = null;
                                if (!replacerexists)
                                {
                                    added = (Tile)(Database.Get(args[++iter])).Clone();
                                    
                                }
                                else
                                {
                                    added = (Tile)(Database.Get(replacer[int.Parse(args[++iter])])).Clone();
                                }
                                tiles[i].Add(added);
                            }
                        }break;
                    case "[Position]":starting_position.Y = int.Parse(args[++iter]);starting_position.X = int.Parse(args[++iter]); break;
                    case "[Replacer]": for(string token2 = args[++iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            string identifier = token2;
                            int id = int.Parse(args[++iter]);
                            replacer.Add(id, identifier);
                        }replacerexists = true; break;
                    default: System.Console.Write("Invalid token:" + token); break;
                }
            }
            Link();
        }

        public void Draw_Frame(Graphics e)
        {
            Rectangle rect = new Rectangle(Map_X,Map_Y, Map_Width, Map_Height);
            e.DrawImage(Background, rect);
            int x = Map_X-Frame_Width;
            int y = Map_Y-Frame_Height;
            
            rect = new Rectangle(x, y,Frame_Width,Frame_Height);
            e.DrawImage(FrameCornerUL, rect);
            for (int i = x+Frame_Width; i <= x+Map_Width; i += Frame_Width)
            {
                rect.X = i;
                e.DrawImage(FramePartU, rect);
            }
            rect.X += Frame_Width;
            e.DrawImage(FrameCornerUR, rect);
            
            for (int i = rect.Y+Frame_Height; i <=y+Map_Height; i +=Frame_Height)
            {
                rect.Y = i;
                e.DrawImage(FramePartR, rect);
            }
            rect.Y +=Frame_Height;
            e.DrawImage(FrameCornerDR, rect);
            for (int i = rect.X - Frame_Width; i > x; i -= Frame_Width)
            {
                rect.X = i;
                e.DrawImage(FramePartD, rect);
            }
            rect.X -= Frame_Width;
            e.DrawImage(FrameCornerDL, rect);
            for (int i = rect.Y -Frame_Height; i > y; i -=Frame_Height)
            {
                rect.Y = i;
                e.DrawImage(FramePartL, rect);
            }
        }

        public void Draw(Graphics e,Player player)
        {
            Draw_Frame(e);
            Draw_Map(e, player);
            


        }
        public void Draw_Map(Graphics e,Player player)
        {

            int drawable = Math.Min((Map_Width / Tile.Get_Width()-1)/2,( Map_Height / Tile.Get_Height()-1)/2);
            int tiles_drawn= (drawable<player.Get_Sight())?drawable: player.Get_Sight();

            int XOffset = (Map_Width - (tiles_drawn*2+1) * Tile.Get_Width())/2;
            int YOffset = (Map_Height - (tiles_drawn*2+1) * Tile.Get_Height())/2;
            if (XOffset < 0) XOffset = 0;
            if (YOffset < 0) YOffset = 0;
            int x = Map_X + XOffset;
            int y = Map_Y + YOffset;

            ///Calculating position in tile matrix
            int xfrom = player.Get_Position().X - tiles_drawn;
            if (xfrom < 0)
            {
                x += Math.Abs(xfrom) * Tile.Get_Width();
                xfrom = 0;
            }
            int xto = player.Get_Position().X + tiles_drawn;
            if (xto >= tiles[0].Count)
            {
                xto = tiles[0].Count - 1;
            }
            int yfrom = player.Get_Position().Y - tiles_drawn;
            if (yfrom < 0)
            {
                y += Math.Abs(yfrom) * Tile.Get_Height();
                yfrom = 0;
            }
            int yto = player.Get_Position().Y + tiles_drawn;
            if (yto >= tiles.Count)
            {
                yto = tiles.Count - 1;
            }

            ///Drawing tiles
            for (int i = yfrom, g = 0; i <= yto; i++, g++)
            {
                for (int j = xfrom, h = 0; j <= xto; j++, h++)
                {
                    int new_x = (int)(x + Tile.Get_Width() * (h));
                    int new_y = (int)(y + Tile.Get_Height() * (g));
                    tiles[i][j].Draw(new_x, new_y, e);
                }
            }


        }
        public void Add_Player(Player player)
        {
            double size_modifier = 5.0 / (player.Get_Sight() * 2 + 1);
            Map.Resize(size_modifier, size_modifier);

            tiles[starting_position.Y][starting_position.X].Enter(player);
        }
        public void Link()
        {
            for(int i = 0; i < tiles.Count; i++)
            {
                for(int j = 0; j < tiles[0].Count; j++)
                {
                    tiles[i][j].Set_Position(new Point(j,i));

                    if (i != 0)
                    {
                        tiles[i][j].Set_Neighbour(Direction.Up, tiles[i - 1][j]);
                    }
                    if (j != 0)
                    {
                        tiles[i][j].Set_Neighbour(Direction.Left, tiles[i][j-1]);
                    }
                    if (i != tiles.Count - 1)
                    {
                        tiles[i][j].Set_Neighbour(Direction.Down, tiles[i + 1][j]);
                    }
                    if (j != tiles[0].Count - 1)
                    {
                        tiles[i][j].Set_Neighbour(Direction.Right, tiles[i][j+1]);
                    }
                }
            }
        }

        public Tile Get(int x,int y)
        {
            return tiles[y][x];
        }

        static public int Get_Width()
        {
            return Map_Width;
        }
        static public int Get_Height()
        {
            return Map_Height;
        }
        static public void Move(int xmod,int ymod)
        {
            Map_X += xmod;
            Map_Y += ymod;
        }
        static public void Resize(int new_width, int new_height)
        {
            double horizontal = new_width/Map_Width;
            double vertical = new_height/Map_Height;
            double new_x = Map_X * horizontal;
            double new_y = Map_Y * vertical;
            Map_X = (int)new_x;
            Map_Y = (int)new_y;
            Map_Width = new_width;
            Map_Height = new_height;
            Tile.Resize(horizontal, vertical);
        }
        static public void Resize(double horizontal,double vertical)
        {
            double new_width =   Map_Width* horizontal;
            double new_height =  Map_Height* vertical;
            double new_fwidth =  Frame_Width* horizontal;
            double new_fheight =  Frame_Height* vertical;
            double new_x= Map_X * horizontal;
            double new_y = Map_Y * vertical;
            Map_X = (int)new_x;
            Map_Y = (int)new_y;
            Map_Width = (int) new_width;
            Map_Height =(int) new_height;
            Frame_Width = (int)new_fwidth;
            Frame_Height = (int)new_fheight;
            Tile.Resize(horizontal, vertical);
        }
        static public void Load_Frame()
        {
            FrameCornerDL = Database.Get_Image("Test:FrameCornerDL.png");
            FrameCornerDR = Database.Get_Image("Test:FrameCornerDR.png");
            FrameCornerUL = Database.Get_Image("Test:FrameCornerUL.png");
            FrameCornerUR = Database.Get_Image("Test:FrameCornerUR.png");

            FramePartD = Database.Get_Image("Test:FramePartD.png");
            FramePartU = Database.Get_Image("Test:FramePartU.png");
            FramePartL = Database.Get_Image("Test:FramePartL.png");
            FramePartR = Database.Get_Image("Test:FramePartR.png");

            Background = Database.Get_Image("Test:Background.png");
        }
    }
}
