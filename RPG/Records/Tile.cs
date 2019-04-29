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
    class Tile:Record
    {
        protected Bitmap Image; 
        protected Entity Entity;
        protected Point Position;
        protected List<MovementMode> Travel_Modes;
        Dictionary<Direction, Tile> Neighbours;

        List<Script> On_Tick;
        List<Script> On_Enter;
        List<Script> On_Leave;

        public Tile()
        {
            Copy(
                null,
                null,
                new Point(0,0),
                new List<Script>(),
                new List<Script>(),
                new List<Script>(),
                new Dictionary<Direction, Tile>(), 
                new List<MovementMode>()
                );
        }
        public Tile(Record type,Bitmap image,Entity entity ,Point position,List<Script> on_leave ,List<Script> on_enter,List<Script> on_tick,Dictionary<Direction,Tile> neighbours,List<MovementMode> travel_mods):base(type)
        {
            Copy(image, entity, position, on_leave, on_enter, on_tick, neighbours, travel_mods);
        }
        public Tile(Tile cloned)
        {
            Copy(cloned);
        }
        public bool Copy(Tile copied)
        {
            return 
                base.Copy(copied)
                &&
                Copy(
                copied.Get_Image(),
                copied.Get_Entity()==null?null:new Entity(copied.Get_Entity()), 
                new Point(copied.Get_Position().X, copied.Get_Position().Y),
                new List<Script>(copied.Get_On_Leave()), 
                new List<Script>(copied.Get_On_Enter()),
                new List<Script>(copied.Get_On_Tick()), 
                new Dictionary<Direction,Tile>(copied.Get_Neighbour_Map()), 
                new List<MovementMode>(copied.Get_Travel_Modes())
                );

        }
        public bool Copy(Bitmap image ,Entity entity,Point position,  List<Script> on_leave , List<Script> on_enter , List<Script> on_tick ,  Dictionary<Direction, Tile> neighbours ,List<MovementMode> travel_mods)
        {
            return 
            Set_Image(image) &&
            Set_Entity(entity) &&
            Set_Position(position) &&
            Set_On_Leave(on_leave) &&
            Set_On_Enter(on_enter) &&
            Set_On_Tick(on_tick) &&
            Set_Neighbours(neighbours) &&
            Set_Travel_Modes(travel_mods);
        }
        public override ScriptObject Clone()
        {
            return new Tile(this);
        }
        public override bool Assign(Record copied)
        {
            return Copy((Tile)copied);
        }

        public bool Enter(Entity entity)
        {
            if (entity == null)
            {
                return false;
            }
            Set_Entity(entity);
            Entity.Set_Position(this);
            foreach(Script iter in On_Enter)
            {
                object[] args = { entity };
                iter.Execute(args);
            }
            return true;
        }
        public bool Leave()
        {
            if (Entity == null)
            {
                return false;
            }
            foreach (Script script in On_Leave)
            {
                object[] args = { Entity };
                script.Execute(args);
            }
            Entity = null;
            return true;
        }
        public bool Tick()
        {
            if (Entity == null)
            {
                return false;
            }
            foreach (Script script in On_Tick)
            {
                object[] args = { Entity };
                script.Execute(args);
            }
            return true;
        }
        public bool Spawn(Entity entity)
        {
            if (entity == null)
            {
                return false;
            }
            else
            {
                Enter((Entity)entity.Clone());
                return true;
            }
            
        }

        public void Draw(Graphics scene, Rectangle dest)
        {
            scene.DrawImage(Image, dest);
            if (Entity != null)
            {
                scene.DrawImage(Entity.Get_Image(), dest);
            }
        }

        public bool Can_Move(MovementMode mode)
        {
            return (Travel_Modes.Contains(mode) && Entity == null);
        }

        public bool Add_On_Enter(Script script)
        {
            if (On_Enter.Contains(script))
            {
                return false;
            }
            else
            {
                On_Enter.Add(script);
                return true;
            }
            
        }
        public bool Remove_On_Enter(Script script)
        {
            return On_Enter.Remove(script);
        }

        public bool Add_On_Tick(Script script)
        {
            if (On_Tick.Contains(script))
            {
                return false;
            }
            else
            {
                On_Tick.Add(script);
                return true;
            }
            
        }
        public bool Remove_On_Tick(Script script)
        {
            if (On_Tick.Contains(script))
            {
                return false;
            }
            else
            {
                On_Tick.Remove(script);
                return true;
            }
            
        }
        public bool Add_On_Leave(Script script)
        {
            if (On_Leave.Contains(script))
            {
                return false;
            }
            else
            {
                On_Leave.Add(script);
                return true;
            }
           
        }
        public bool Remove_On_Leave(Script script)
        {
           return  On_Leave.Remove(script);
        }

        public bool Add_Travel_Mode(MovementMode mode)
        {
            if (Travel_Modes.Contains(mode))
            {
                return false;
            }
            else
            {
                Travel_Modes.Add(mode);
                return true;
            }
            
        }
        public void Remove_Travel_Mode(MovementMode mode)
        {
            Travel_Modes.Remove(mode);
        }

        public Bitmap Get_Image()
        {
            return Image;
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
            return Travel_Modes;
        }
        public List<Tile> Get_Neighbours(List<Tag> filter=null)
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
        public Dictionary<Direction,Tile> Get_Neighbour_Map()
        {
            return Neighbours;
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

        public bool Set_Image(Bitmap image)
        {
            Image = image;
            return true;
        }
        public bool Set_Entity(Entity entity)
        {
            Entity = entity;
            return true;
        }
        public bool Set_Position(Point position)
        {
            if (position.X < 0 || position.Y < 0)
            {
                throw new Exception("Tile:Set_Position:Invalid position: " + position.X + "," + position.Y);
            }
            else
            {
                Position = position;
                return true;
            }
        }
        public bool Set_Position(int x,int y)
        {
            if(x>0 && y > 0)
            {
                Position.X = x;
                Position.X = y;
                return true;
            }
            else
            {
                throw new Exception("Tile:Set_Position:Invalid position: " +x + "," + y);
            }
            
        }
        public bool Set_Travel_Modes(List<MovementMode> travel_modes)
        {
            Travel_Modes = travel_modes;
            return true;
        }
        public bool Set_Neighbours(Dictionary<Direction,Tile> neighbours)
        {
            Neighbours = neighbours;
            return true;
        }
        public bool Set_Neighbour(Direction direction,Tile neighbour)
        {
            Neighbours.Add(direction, neighbour);
            return true;
        }
        public bool Set_On_Enter(List<Script> on_enter)
        {
            On_Enter = on_enter;
            return true;
        }
        public bool Set_On_Tick(List<Script> on_tick)
        {
            On_Tick = on_tick;
            return true;
        }
        public bool Set_On_Leave(List<Script> on_leave)
        {
            On_Leave = on_leave;
            return true;
        }

        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Image": return Set_Image((Bitmap)value);
                case "Entity": return Set_Entity((Entity)value);
                case "Position": return Set_Description((string)value);
                case "Travel_Modes": return Set_Travel_Modes(MyParser.Convert_Array<MovementMode>(value));
                case "On_Tick":return Set_On_Tick((List<Script>)value);
                case "On_Enter": return Set_On_Enter((List<Script>)value);
                case "On_Leave": return Set_On_Leave((List<Script>)value);
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Image": return Get_Image();
                case "Entity": return Get_Entity();
                case "Position": return Get_Position();
                case "Travel_Modes": return Get_Travel_Modes();
                case "On_Tick": return Get_On_Tick();
                case "On_Enter": return Get_On_Enter();
                case "On_Leave": return Get_On_Leave();
                default: return base.Get_Variable(name);
            }
        }
        public override object Call_Function(string name, object[] args)
        {
            switch (name)
            {
                case "Spawn":return Spawn((Entity)args[0]);
                case "Can_Move":return Can_Move((MovementMode)args[0]);
                case "Add_On_Enter":return Add_On_Enter((Script)args[0]);
                case "Add_On_Tick":return Add_On_Tick((Script)args[0]);
                case "Add_On_Leave":return Add_On_Leave((Script)args[0]);
                case "Remove_On_Enter":return Remove_On_Enter((Script)args[0]);
                case "Remove_On_Tick":return Remove_On_Tick((Script)args[0]);
                case "Remove_On_Leave":return Remove_On_Leave((Script)args[0]);
                case "Set_Neighbour":return Set_Neighbour((Direction)args[0], (Tile)args[1]);
                case "Enter":return Enter((Entity)args[0]);
                case "Leave":return Leave();
                case "On_Tick":return Tick();
                default: return base.Call_Function(name, args);
            }
        }
    }
}
