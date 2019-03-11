using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Entity:Type
    {
        protected Bitmap Graphics_Image;

        protected List<Stat_Regeneration> Regenerators;
        protected List<Stat_Resource> Resources;
        protected List<Stat_Modifier> Modifiers;
        protected List<Stat> Stats;

        protected Tile Position;
        protected Direction Orientation;
        protected MovementMode Movement;
        protected Cooldown Speed;
        protected int Sight;

        protected List<Skill> Skills=new List<Skill>();
        protected List<Item> Inventory;
        protected Dictionary<Slot, Item> Equipment;

        public Entity(
            Mod source = null
           ,string name = ""
           ,string description = ""
           ,List<Tag> tags = null
           ,Bitmap image=null
           ,MovementMode movement=MovementMode.Walk
           ,Cooldown speed=null
           ,int sight=1
           ,Tile position=null
           ,Direction orientation=Direction.Right
           ,List<Stat> stats=null
           ,List<Stat_Resource> resources = null
           ,List<Stat_Modifier> modifiers = null
           ,List<Stat_Regeneration> regenerators = null
           ,Dictionary<Slot, Item> equipment=null
           ,List<Item> inventory=null
           ) :base(source,name,description,tags)
        {
            Set_Speed(speed);
            Set_Sight(sight);
            Set_Position(position);
            Set_Stats(stats);
            Set_Orientation(orientation);
            Set_Image(image);
            Set_Resources(resources);
            Set_Modifiers(modifiers);
            Set_Regenerators(regenerators);
            Set_Equipment(equipment);
            Set_Inventory(inventory);
            Set_Movement_Mode(movement);
        }
        public Entity(Entity cloned) : base(cloned)
        {


        }
        public override Type Clone()
        {
            return new Entity(Source, Name, Description, Tags, Graphics_Image,Movement,Speed, Sight, Position, Orientation, Stats,Resources, Modifiers, Regenerators, Equipment, Inventory);
        }

        public override void Load(string[] args,ref int iter,Mod _mod)
        {
            for(string token = args[iter]; token != "[END]"; token = args[++iter])
            {
                switch (token)
                {
                    case "[Type]":base.Load(args,ref iter, _mod); break;
                    case "[Image]":Set_Image(Database.Get_Image(args[++iter])); break;
                    case "[Inventory]": for (string token2 = args[++iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            Inventory.Add((Item)Database.Get(token2).Clone());
                        }break;
                    case "[Equipment]":for (string token2 = args[++iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            Equipment.Add(Database.Get_Slot(token2),(Item)Database.Get(args[++iter]).Clone());
                        } break;
                    case "[Stat_Resource]": for (string token2 = args[++iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            Stat_Resource added = (Stat_Resource)Database.Get(token2).Clone();
                            added.Set_Flat(int.Parse(args[++iter]));
                            added.Set_Percent(int.Parse(args[++iter]));
                            added.Set_Current((int.Parse(args[++iter])));
                            Add_Stat_Resource(added);
                        }break;
                    case "[Stat_Modifier]": for (string token2 = args[++iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            Stat_Modifier added = (Stat_Modifier)Database.Get(token2).Clone();
                            added.Set_Flat(int.Parse(args[++iter]));
                            added.Set_Percent(int.Parse(args[++iter]));
                            Add_Stat_Modifier(added);
                        } break;
                    case "[Stat_Regeneration]": for (string token2 = args[++iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            Stat_Regeneration added=(Stat_Regeneration) Database.Get(token2).Clone();
                            added.Set_Flat(int.Parse(args[++iter]));
                            added.Set_Percent(int.Parse(args[++iter]));
                            Add_Stat_Regeneration(added);
                        } break;
                    case "[Sight]":Set_Sight(int.Parse(args[++iter]));break;
                    case "[Speed]":Set_Speed(double.Parse(args[++iter]));break;
                    case "[Movement_Mode]":Set_Movement_Mode(args[++iter]); break;
                    default: System.Console.Write("Invalid token:" + token); break;
                }

            }
            Calculate_Stats();
        }

        public void Equip(Item item)
        {
            Inventory.Remove(item);
            Item switched = Equipment[item.Get_Slot()];

            if (switched != null)
            {
                UnEquip(switched);
                Inventory.Add(switched);
            }
            Equipment[item.Get_Slot()] = item;
            foreach(Stat stat in Stats)
            {
                item.Apply(stat);
            }
        }
        public void UnEquip(Item item)
        {
            Equipment[item.Get_Slot()] = null;
            foreach(Stat stat in Stats)
            {
                item.Remove(stat);
            }
        }

        public void Calculate_Stats()
        {
            List<Item> items = Equipment.Values.ToList();
            foreach (Stat stat in Stats)
            {
                foreach(Item item in items)
                {
                    item.Apply(stat);
                }
            }
        }

        public void Add_Items(List<Item> items)
        {
            Inventory.AddRange(items);
        }
        public List<Item> Drop_Loot()
        {
            return Inventory;
        }

        public void Add_Stat_Regeneration(Stat_Regeneration added)
        {
            Regenerators.Add(added);
            Stats.Add(added);
            added.Bind(Resources);
        }
        public void Add_Stat_Resource(Stat_Resource added)
        {
            Resources.Add(added);
            Stats.Add(added);
        }
        public void Add_Stat_Modifier(Stat_Modifier added)
        {
            Modifiers.Add(added);
            Stats.Add(added);
        }
        
        public void Add_Skill(Skill added)
        {
            Skills.Add(added);
        }

        public External_Effect Send(External_Effect effect)
        {
            foreach(Stat_Modifier modifier in Modifiers)
            {
                modifier.Send(effect);
            }
            return effect;
        }
        public void Recieve(External_Effect effect)
        {
            foreach (Stat_Modifier modifier in Modifiers)
            {
                modifier.Recieve(effect);
            }
            foreach (Stat stat in Stats)
            {
                effect.Apply(stat);
            }
        }
        public void Move(Direction direction)
        {
            Tile next = Position.Get_Neighbour(direction);
            if (next.Can_Move(Movement) && Speed.Is_Done() && next!=null)
            {
                
                Position.Leave();
                next.Enter(this);
                Orientation = direction;
                Speed.Start();
            }
        }

        public void Draw(int _x, int _y, Graphics e)
        {
            Rectangle rect = new Rectangle(_x, _y,Tile.Get_Width(),Tile.Get_Height());
            //TODO Rotate image
            e.DrawImage(Graphics_Image, rect);
        }

        public override string Get_Details()
        {
            return base.Get_Details() + Environment.NewLine + "Speed: " + Speed.Get_Time()+"/"+Speed.Get_Time_Left() + Environment.NewLine + "Sight: " + Sight +
                Environment.NewLine + Get_Stat_List();
        }
        public double Get_Distance(Entity e)
        {
            return e.Get_Tile().Get_Distance(Position);
        }
        public Point Get_Position()
        {
            return Position.Get_Position();
        }
        public Tile Get_Tile()
        {
            return Position;
        }
        public int Get_Sight()
        {
            return Sight;
        }
        public List<Stat> Get_Stats()
        {
            return Stats;
        }
        public List<Stat_Resource> Get_Resources()
        {
            return Resources;
        }
        public List<Stat_Regeneration> Get_Regenerators()
        {
            return Regenerators;
        }
        public List<Stat_Modifier> Get_Modifiers()
        {
            return Modifiers;
        }
        public Dictionary<Slot,Item> Get_Equipment()
        {
            return Equipment;
        }
        public List<Item> Get_Inventory()
        {
            return Inventory;
        }
        public MovementMode Get_Movement_Mode()
        {
            return Movement;
        }
        public string Get_Stat_List()
        {
            string returned =
            "Stat "+Environment.NewLine;
            foreach(Stat stat in Stats)
            {
                returned +=stat.ToLine() + Environment.NewLine;
            }
            return returned;
        }
        public string Get_Skill_List()
        {
            string returned = "Skills" + Environment.NewLine;
            foreach(Skill skill in Skills)
            {
                returned += skill.ToLine() + Environment.NewLine;
            }
            return returned;
        }

        public void Set_Image(Bitmap image)
        {
            if(image!=null)Graphics_Image = image;
        }
        public void Set_Movement_Mode(string identifier)
        {
            switch (identifier)
            {
                case "Walk":Movement = MovementMode.Walk; break;
                case "Fly": Movement = MovementMode.Fly; break;
                case "Phase": Movement = MovementMode.Phase; break;
            }
        }
        public void Set_Movement_Mode(MovementMode mode)
        {
            Movement = mode;
        }
        public void Set_Speed(double speed)
        {
            if(Speed!=null) Speed.Set_Time(1 / speed);
            else Speed=new Cooldown((1 / speed)); 

        }
        public void Set_Speed(Cooldown speed)
        {
            if (speed != null) Speed = speed;
        }
        public void Set_Orientation(Direction direction)
        {
            Orientation = direction;
        }
        public void Set_Position(Tile tile)
        {
            Position = tile;
        }
        public void Set_Sight(int sight)
        {
            if (sight > 0) Sight = sight;
        }
        public void Set_Stats(List<Stat> stats)
        {
            if (stats != null) Stats = stats;
            else Stats = new List<Stat>();
        }
        public void Set_Resources(List<Stat_Resource> resources )
        {
            if (resources != null)
            {
                Resources = resources;
            }
            else Resources = new List<Stat_Resource>();
        }
        public void Set_Regenerators(List<Stat_Regeneration> regenerators)
        {
            if (regenerators != null)
            { 
               Regenerators = regenerators;
            }
            else Regenerators = new List<Stat_Regeneration>();
        }
        public void Set_Modifiers(List<Stat_Modifier> modifiers)
        {
            if (modifiers != null)
            {
                Modifiers = modifiers;
            }
            else Modifiers = new List<Stat_Modifier>();

        }
        public void Set_Equipment(Dictionary<Slot,Item> equipment)
        {
            if (equipment != null) Equipment = equipment;
            else Equipment = new Dictionary<Slot, Item>();
        }
        public void Set_Inventory(List<Item> inventory)
        {
            if (inventory != null) Inventory = inventory;
            else Inventory = new List<Item>();
        }

    }
}
