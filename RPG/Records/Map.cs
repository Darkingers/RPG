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
            return base.Copy(copied) && Copy(copied.Get_Replacer(), copied.Get_Tiles(), copied.Get_Starting_Position());

        }
        public override object Clone()
        {
            return new Map(this);
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

        public override string ToString(string tab)
        {
            string temp =
                base.ToString(tab);
            return temp;
        }
        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Tiles": return Set_Tiles((List<List<int>>)value);
                case "Starting_Position": return Set_Starting_Position((Point)value);
                case "Replacer": return Set_Replacer(MyParser.Convert_Dictionary<int,Tile>(value));
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Tiles": return Tiles;
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
                default: return base.Call_Function(name, args);
            }
        }
    }
}
