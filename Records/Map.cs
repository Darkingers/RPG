using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Map:Record
    {
        protected Dictionary<int, Tile> Replacer;
        protected List<List<Tile>> Tiles;
        protected Point Starting_Position;

        public Map()
        {
            Copy(new Dictionary<int, Tile>(), new List<List<Tile>>(), new Point(0, 0));
        }
        public Map(Record type,Dictionary<int,Tile> replacer,List<List<Tile>> tiles,Point start_pos):base(type)
        {
            Copy(replacer, tiles, start_pos);
        }
        public Map(Map cloned)
        {
            Copy(cloned);
        }
        public bool Copy(Dictionary<int,Tile> replacer,List<List<Tile>> tiles,Point start_pos)
        {
            return Set_Replacer(replacer) && Set_Tiles(tiles) && Set_Starting_Position(start_pos);
        }
        public bool Copy(Map copied)
        {
            return base.Copy(copied) && Copy(copied.Get_Replacer(),new List<List<Tile>>( copied.Get_Tiles()), copied.Get_Starting_Position());

        }
        public override ScriptObject Clone()
        {
            return new Map(this);
        }
        public override bool Assign(Record copied)
        {
            return Copy((Map)copied);
        }

        public bool Add_Player(Player player)
        {
            return Tiles[Starting_Position.Y][Starting_Position.X].Enter(player);
        }
        public bool Link()
        {
            for(int i = 0; i < Tiles.Count; i++)
            {
                for(int j = 0; j < Tiles[0].Count; j++)
                {
                    Tiles[i][j].Set_Position(new Point(j,i));

                    if (i != 0)
                    {
                        Tiles[i][j].Set_Neighbour(Direction.Up, Tiles[i - 1][j]);
                    }
                    if (j != 0)
                    {
                        Tiles[i][j].Set_Neighbour(Direction.Left, Tiles[i][j-1]);
                    }
                    if (i != Tiles.Count - 1)
                    {
                        Tiles[i][j].Set_Neighbour(Direction.Down, Tiles[i + 1][j]);
                    }
                    if (j != Tiles[0].Count - 1)
                    {
                        Tiles[i][j].Set_Neighbour(Direction.Right, Tiles[i][j+1]);
                    }
                }
            }
            return true;
        }
        public Dictionary<Rectangle,Tile> RenderMap(Graphics scene,int FrameWidth,int FrameHeight,int Width,int Height,Entity observer)
        {
            int Sight;
            int XPos;
            int YPos;
            if (observer==null || observer.Get_Tile()==null)
            {
                Sight = Math.Min(Tiles.Count, Tiles[0].Count);
                Sight=Sight % 2 == 0 ?Sight / 2 - 1 : Sight / 2;
                XPos = Tiles.Count / 2;
                YPos = Tiles[0].Count / 4;
            }
            else
            {
                Sight = observer.Get_Sight();
                XPos = observer.Get_Position().X;
                YPos = observer.Get_Position().Y;
            }

            int TileAmount = Sight * 2 + 1;
            int TileWidth = (Width - 2 * FrameWidth) / TileAmount;
            int TileHeight = (Height - 2 * FrameHeight) / TileAmount;

           

            int Xoffset = FrameWidth;
            int Yoffset = FrameHeight;

            int rfrom;
            if(YPos - Sight< 0)
            {
                rfrom = 0;
                Yoffset += TileHeight * Math.Abs(YPos - Sight);
            }
            else
            {
                rfrom = YPos - Sight;
            }


            int rto;
            if(YPos + Sight >= Tiles.Count)
            {
                rto= Tiles.Count - 1;
            }
            else
            {
                rto= YPos + Sight; 
            }


            int cfrom;
            if (XPos - Sight < 0)
            {
                cfrom = 0;
                Xoffset += TileWidth * Math.Abs(XPos - Sight);
            }
            else
            {
                cfrom= XPos - Sight;
            }

            int cto;
            if (XPos + Sight >= Tiles[0].Count)
            {
                cto = Tiles[0].Count - 1;
            }
            else
            {
                cto= XPos + Sight;
            }

            Rectangle rect = new Rectangle(0, 0, TileWidth, TileHeight);
            Dictionary<Rectangle, Tile> returned = new Dictionary<Rectangle, Tile>();
            for (int i = rfrom,y=0; i <= rto; i++,y++)
            {
                rect.Y = TileHeight * y+Yoffset;
                for(int j = cfrom,x=0; j <= cto; j++,x++)
                {
                    rect.X = TileWidth * x+Xoffset;
                    Tiles[i][j].Draw(scene,rect);
                    returned.Add(new Rectangle(rect.X,rect.Y,rect.Width,rect.Height), Tiles[i][j]);
                }
            }
            return returned;
        }

        public bool Set_Starting_Position(Point start)
        {
            Starting_Position = start;
            return true;
        }
        public bool Set_Tiles(List<List<int>> tiles)
        {
            for(int i = 0; i < tiles.Count; i++)
            {
                Tiles.Add(new List<Tile>());
                for(int j = 0; j < tiles[i].Count; j++)
                {
                    Tiles[i].Add((Tile)Replacer[tiles[i][j]].Clone());
                }
            }
            Link();
            return true;
        }
        public bool Set_Tiles(List<List<Tile>> tiles)
        {
            Tiles = tiles;
            return true;
        }
        public bool Set_Tile(Tile tile,Point point)
        {
            Tiles[point.Y][point.X] = tile;
            return true;
        }
        public bool Set_Replacer(Dictionary<int,Tile> replacer)
        {
            Replacer = replacer;
            return true;
        }
        public bool Set_Entities(Dictionary<Point,Entity> entities) {
            List<Point> points = entities.Keys.ToList();
            foreach (Point p in points)
            {
                Tiles[p.Y][p.X].Spawn(entities[p]);
            }
            return true;
        }
        public Dictionary<int,Tile> Get_Replacer()
        {
            return Replacer;
        }
        public List<List<Tile>> Get_Tiles()
        {
            return Tiles;
        }
        public Point Get_Starting_Position()
        {
            return Starting_Position;
        }
        public Tile Get_Tile(int x,int y)
        {
            return Tiles[y][x];
        }

        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Tiles": return Set_Tiles(Converter.Convert_Grid<int>(value));
                case "Starting_Position": return Set_Starting_Position((Point)value);
                case "Replacer": return Set_Replacer(Converter.Convert_Dictionary<int,Tile>(value));
                case "Entities":return Set_Entities(Converter.Convert_Dictionary<Point, Entity>(value));
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Starting_Position": return Starting_Position;
                case "Replacer":return Replacer;
                default: return base.Get_Variable(name);
            }
        }
        public override object Call_Function(string name, object[] args)
        {
            switch (name)
            {
                case "Add_Player": return Add_Player((Player)args[0]);
                case "Link": return Link();
                case "Get_Tile":return Get_Tile((int)args[0], (int)args[1]);
                default: return base.Call_Function(name, args);
            }
        }
    }
}
