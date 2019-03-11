using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG
{
    enum Direction { Up,Down,Left,Right}
    enum MovementMode {Walk,Fly,Phase}
    class Tile:Type
    {
        protected static int Graphics_Tile_Width=100;
        protected static int Graphics_Tile_Height=100;
        protected Bitmap Graphics_Image;
        protected bool Selected;

        protected Entity Entity;
        protected Point Position;
        protected List<MovementMode> Travel_mods;
        Dictionary<Direction, Tile> Neighbours;

        List<Script> On_Tick;
        List<Script> On_Enter;
        List<Script> On_Leave;

        public Tile(
            Mod source = null
           ,string name = ""
           ,string description = ""
           ,List<Tag> tags = null
           ,Bitmap image = null
           ,Entity entity=null
           ,int x=0
           ,int y=0
           ,List<Script> on_leave=null
           ,List<Script> on_enter=null
           ,List<Script> on_tick=null
           ,Dictionary<Direction,Tile> neighbours=null
           ,List<MovementMode> travel_mods=null
            ) : base(source,name, description, tags)
        {
            Set_Image(image);
            Set_Entity(entity);
            Set_Position(x, y);
            Set_On_Leave(on_leave);
            Set_On_Enter(on_enter);
            Set_On_Tick(On_Tick);
            Set_Neighbours(neighbours);
            Set_Travel_Modes(travel_mods);
        }
        public Tile(Tile cloned):base((Type)cloned)
        {
            Set_Image(cloned.Get_Image());
            Set_Entity((Entity)cloned.Get_Entity().Clone());
            Set_Travel_Modes(cloned.Get_Travel_Modes());
            Set_On_Enter(new List<Script>(cloned.Get_On_Enter()));
            Set_On_Tick(new List<Script>(cloned.Get_On_Tick()));
            Set_On_Leave(new List<Script>(cloned.Get_On_Leave()));
        }
        public override Type Clone()
        {
            if (Entity == null)
            {
                return new Tile(Source, Name, Description, Tags,
                    Graphics_Image, null, 0, 0, new List<Script>(On_Tick), new List<Script>(On_Leave), new List<Script>(On_Enter), null,new List<MovementMode>(Travel_mods));
            }
            else
            {
                return new Tile(Source, Name, Description, Tags,
                    Graphics_Image, (Entity)Entity.Clone(),0, 0,new List<Script>(On_Tick), new List<Script>(On_Leave), new List<Script>(On_Enter), null, new List<MovementMode>(Travel_mods));
            }
           
        }

        public override void Load(string[] args, ref int iter, Mod _mod)
        {
            for(string token = args[++iter]; token != "[END]"; token = args[++iter])
            {
                switch (token)
                {
                    case "[Travel_Modes]": for (string token2 = args[++iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            Add_Travel_Mode(token2);
                        } break;
                    case "[Image]":Set_Image(Database.Get_Image(args[++iter])); break;
                    case "[Type]":base.Load(args,ref iter, Source);break;
                    case "[On_Enter]": for (string token2 = args[++iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            Add_On_Enter(token2);
                        }break;
                    case "[On_Leave]": for (string token2 = args[++iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            Add_On_Leave(token2);
                        }break;
                    case "[On_Tick]": for (string token2 = args[++iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            Add_On_Tick(token2);
                        }break;
                    default: throw new Exception("[Tile]:Invalid token: " + token);
                }
            }
        }
        //TODO write
        //TODO tostring

        public void Spawn(Entity entity)
        {
           Set_Entity((Entity) entity.Clone());
        }
        public void Enter(Entity entity)
        {
            Set_Entity(entity);
        }
        public void Leave()
        {
            
            foreach (Script script in On_Leave)
            {
                script.Execute(Entity, Entity);
            }
            Entity = null;
        }
        public void Tick()
        {
            foreach (Script script in On_Tick)
            {
                script.Execute(Entity, Entity);
            }
        }

        public bool Can_Move(MovementMode mode)
        {
            return (Travel_mods.Contains(mode) && Entity == null);
        }
        public bool Can_Move(List<MovementMode> modes)
        {
            foreach(MovementMode mode in modes)
            {
                if (!Can_Move(mode))
                {
                    return false;
                }
            }
            return true;
        }

        public void Draw(int x,int y,Graphics e)
        {
            Rectangle rect = new Rectangle(x, y, Graphics_Tile_Width, Graphics_Tile_Height);
            e.DrawImage(Graphics_Image,rect);
            if (Entity != null)
            {
                Entity.Draw(x, y, e);
            }
            if (Selected)
            {
                rect.Width -= 1;
                rect.Height -= 1;
                e.DrawRectangle(new Pen(Color.Black, 1), rect);
            }
        }
        public void Draw(Point p, Graphics e)
        {
            int x = p.X;
            int y = p.Y;
            Rectangle rect = new Rectangle(x, y, Graphics_Tile_Width, Graphics_Tile_Height);
            e.DrawImage(Graphics_Image, rect);
            if (Entity != null)
            {
                Entity.Draw(x, y, e);
            }
            if (Selected)
            {
                rect.Width -= 1;
                rect.Height -= 1;
                e.DrawRectangle(new Pen(Color.Black, 1), rect);
            }
        }

        public void Add_On_Enter(string identifier)
        {
            Add_On_Enter((Script)Database.Get(identifier).Clone());
        }
        public void Add_On_Enter(Script script)
        {
            On_Enter.Add(script);
        }
        public void Remove_On_Enter(string identifier)
        {
            Remove_On_Enter((Script)Database.Get(identifier).Clone());
        }
        public void Remove_On_Enter(Script script)
        {
            On_Enter.Remove(script);
        }

        public void Add_On_Tick(string identifier)
        {
            Add_On_Tick((Script)Database.Get(identifier).Clone());
        }
        public void Add_On_Tick(Script script)
        {
            On_Tick.Add(script);
        }
        public void Remove_On_Tick(string identifier)
        {
            Remove_On_Tick((Script)Database.Get(identifier).Clone());
        }
        public void Remove_On_Tick(Script script)
        {
            On_Tick.Remove(script);
        }

        public void Add_On_Leave(string identifier)
        {
            Add_On_Leave((Script)Database.Get(identifier).Clone());
        }
        public void Add_On_Leave(Script script)
        {
            On_Leave.Add(script);
        }
        public void Remove_On_Leave(string identifier)
        {
            Remove_On_Leave((Script)Database.Get(identifier).Clone());
        }
        public void Remove_On_Leave(Script script)
        {
            On_Leave.Remove(script);
        }


        public void Add_Travel_Mode(MovementMode mode)
        {
            Travel_mods.Add(mode);
        }
        public void Add_Travel_Mode(string identifier)
        {
            switch (identifier)
            {
                case "Walk": Add_Travel_Mode(MovementMode.Walk); break;
                case "Fly": Add_Travel_Mode(MovementMode.Fly); break;
                case "Phase": Add_Travel_Mode(MovementMode.Phase); break;
                default: throw new Exception("[Type]:Invalid token: " + identifier);
            }
        }
        public void Remove_Travel_Mode(MovementMode mode)
        {
            Travel_mods.Remove(mode);
        }
        public void Remove_Travel_Mode(string identifier)
        {
            switch (identifier)
            {
                case "Walk": Remove_Travel_Mode(MovementMode.Walk); break;
                case "Fly": Remove_Travel_Mode(MovementMode.Fly); break;
                case "Phase": Remove_Travel_Mode(MovementMode.Phase); break;
            }
        }

        public void Select()
        {
            Selected = true;
        }
        public void UnSelect()
        {
            Selected = false;
        }

        static public bool Resize(int new_width, int new_height)
        {
            if (new_width < 0 || new_height < 0 || new_width > Map.Get_Width() || new_height > Map.Get_Height()) return false;
            Graphics_Tile_Width = new_width;
            Graphics_Tile_Height = new_height;
            return true;
        }
        static public bool Resize(double horizontal, double vertical)
        {
            double new_width = horizontal * Graphics_Tile_Width;
            double new_height = vertical * Graphics_Tile_Height;
            if (new_width < 0 || new_height < 0 || new_width > Map.Get_Width() || new_height > Map.Get_Height()) return false;
            Graphics_Tile_Width = (int)new_width;
            Graphics_Tile_Height = (int)new_height;
            return true;
        }

        static public int Get_Width()
        {
            return Graphics_Tile_Width;
        }
        static public int Get_Height()
        {
            return Graphics_Tile_Height;
        }
        public Bitmap Get_Image()
        {
            return Graphics_Image;
        }
        public Entity Get_Entity()
        {
            return Entity;
        }
        public Point Get_Position()
        {
            return Position;
        }
        public int Get_X()
        {
            return Position.X;
        }
        public int Get_Y()
        {
            return Position.Y;
        }
        public double Get_Distance(Tile from)
        {
            return Math.Sqrt((Get_X() - from.Get_X()) ^ 2 +(Get_Y() - from.Get_Y()) ^ 2);
        }
        public List<MovementMode> Get_Travel_Modes()
        {
            return Travel_mods;
        }
        public List<Tile> Get_Neighbours(List<Tag> filter)
        {
            List<Tile> neighbours = Neighbours.Values.ToList();
            return neighbours.FindAll((Tile t) =>
            {
                foreach (Tag tag in t.Get_Tags())
                {
                    if (filter.Contains(tag))
                    {
                        return false;
                    }
                }
                return true;
            });
        }
        public Tile Get_Neighbour(Direction direction)
        {
            return Neighbours[direction];
        }
        public List<Script> Get_On_Enter()
        {
            return On_Enter;
        }
        public List<Script> Get_On_Tick()
        {
            return On_Tick;
        }
        public List<Script> Get_On_Leave()
        {
            return On_Leave;
        }
        public override string Get_Details()
        {
            if (Entity != null)
            {
                return Entity.Get_Details();
            }
            return base.Get_Details();
        }


        static public void Set_Width(int width)
        {
            if (width > 0) Graphics_Tile_Width = width;
        }
        static public void Set_Height(int height)
        {
            if (height > 0) Graphics_Tile_Height = height;
        }
        public void Set_Image(Bitmap image)
        {
            if (image != null) Graphics_Image = image;
        }
        public void Set_Entity(Entity entity)
        {
            Entity = entity;
            if(Entity!=null)Entity.Set_Position(this);
            if (On_Tick == null) return;
            foreach (Script script in On_Tick)
            {
                script.Execute(entity, entity);
            }
        }
        public void Set_Position(Point position)
        {
            Position = position;
        }
        public void Set_Position(int x,int y)
        {
            if(x>0 && y > 0)
            {
                Position.X = x;
                Position.X = y;
            }
        }
        public void Set_Travel_Modes(List<MovementMode> travel_modes)
        {
            if (travel_modes != null) Travel_mods = travel_modes;
            else Travel_mods = new List<MovementMode>();
        }
        public void Set_Neighbours(Dictionary<Direction,Tile> neighbours)
        {
            if (neighbours != null) Neighbours = neighbours;
            else Neighbours =new Dictionary<Direction, Tile>();
        }
        public void Set_Neighbour(Direction direction,Tile neighbour)
        {
            Neighbours.Add(direction, neighbour);
        }
        public void Set_On_Enter(List<Script> on_enter)
        {
            if (on_enter != null) On_Enter = on_enter;
            else On_Enter = new List<Script>();
        }
        public void Set_On_Tick(List<Script> on_tick)
        {
            if (on_tick != null) On_Tick = on_tick;
            else On_Tick = new List<Script>();
        }
        public void Set_On_Leave(List<Script> on_leave)
        {
            if (on_leave != null) On_Leave = on_leave;
            else On_Leave = new List<Script>();
        }
    }
}
