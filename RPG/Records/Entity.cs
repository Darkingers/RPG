using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Entity:Record
    {
        protected Bitmap Image;

        protected List<Stat_Regeneration> Regenerators;
        protected List<Stat_Resource> Resources;
        protected List<Stat_Modifier> Modifiers;
        protected List<Stat> Stats;

        protected Tile Position;
        protected Direction Orientation;
        protected MovementMode Movement_Mode;
        protected Cooldown Speed;
        protected int Sight;

        protected List<Skill> Skills;
        protected List<Item> Inventory;
        protected Dictionary<Slot, Item> Equipment;

        public Entity()
        {
            Stats = new List<Stat>();
            Copy(null, MovementMode.Walk,new Cooldown(null,1,0,null), 2, null, Direction.Down, new List<Stat_Resource>(),
                new List<Stat_Modifier>(), new List<Stat_Regeneration>(), new Dictionary<Slot, Item>(), new List<Item>());
        }
        public Entity(Entity cloned)
        {
            Stats = new List<Stat>();
            Copy(cloned);
        }
        public Entity(Record type, Bitmap image, MovementMode movement,Cooldown speed, int sight, Tile position, Direction orientation, List<Stat_Resource> resources,  List<Stat_Modifier> modifiers , List<Stat_Regeneration> regenerators, Dictionary<Slot, Item> equipment, List<Item> inventory):base(type)
        {
            Stats = new List<Stat>();
            Copy(image, movement, speed, sight, position, orientation, resources, modifiers, regenerators, equipment, inventory);
        }
        public bool Copy(Bitmap image,MovementMode movement,Cooldown speed,int sight,Tile position,Direction orientation,List<Stat_Resource> resources ,List<Stat_Modifier> modifiers,List<Stat_Regeneration> regenerators,Dictionary<Slot, Item> equipment,List<Item> inventory)
        {
            return
            Set_Speed(speed)&&
            Set_Sight(sight)&&
            Set_Position(position)&&
            Set_Orientation(orientation)&&
            Set_Image(image)&&
            Set_Resources(resources)&&
            Set_Modifiers(modifiers)&&
            Set_Regenerators(regenerators)&&
            Set_Equipment(equipment)&&
            Set_Inventory(inventory)&&
            Set_Movement_Mode(movement);
        }
        public bool Copy(Entity copied)
        {
            return Copy(copied.Get_Image(), copied.Get_Movement_Mode(),new Cooldown(copied.Get_Speed()), copied.Get_Sight(),
                copied.Get_Tile(), copied.Get_Orientation(), 
                new List<Stat_Resource>(copied.Get_Resources()), 
                new List<Stat_Modifier>(copied.Get_Modifiers()), 
                new List<Stat_Regeneration>(copied.Get_Regenerators()),
                new Dictionary<Slot,Item>(copied.Get_Equipment()),
                new List<Item>( copied.Get_Inventory()));
        }
        public override object Clone()
        {
            return new Entity(this);
        }

        public bool Equip(Item item)
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
            return true;
        }
        public bool UnEquip(Item item)
        {
            Equipment[item.Get_Slot()] = null;
            foreach(Stat stat in Stats)
            {
                item.Remove(stat);
            }
            return true;
        }

        public bool Calculate_Stats()
        {
            List<Item> items = Equipment.Values.ToList();
            foreach (Stat stat in Stats)
            {
                foreach(Item item in items)
                {
                    item.Apply(stat);
                }
            }
            return true;
        }

        public bool Add_Item(Item added)
        {
            Inventory.Add(added);
            return true;
        }
        public bool Add_Items(List<Item> added)
        {
            bool returned = true;
            foreach(Item item in added)
            {
                if (!Add_Item(item))
                {
                    returned = false;
                }
            }
            return returned;
        }

        public bool Remove_Item(Item removed)
        {
            return Inventory.Remove(removed);
        }
        public bool Remove_Items(List<Item> removed)
        {
            bool returned = false;
            foreach(Item item in removed)
            {
                if (!Remove_Item(item))
                {
                    returned = false;
                }
            }
            return returned;
        }

        public List<Item> Drop_Loot()
        {
            List<Item> items = Inventory;
            items.AddRange(Equipment.Values.ToList());
            Inventory.Clear();
            Equipment.Clear();
            return items;
        }

        public bool Add_Stat_Regeneration(Stat_Regeneration added)
        {
            foreach(Stat_Regeneration regen in Regenerators)
            {
                if (regen.Equals(added))
                {
                    return false;
                }
            }
            Regenerators.Add(added);
            Stats.Add(added);
            added.Bind(Resources);
            return true;
        }
        public bool Add_Stat_Resource(Stat_Resource added)
        {
            foreach (Stat_Resource res in Resources)
            {
                if (res.Equals(added))
                {
                    return false;
                }
            }
            Resources.Add(added);
            Stats.Add(added);
            return true;
        }
        public bool Add_Stat_Modifier(Stat_Modifier added)
        {
            foreach(Stat_Modifier mod in Modifiers)
            {
                if (mod.Equals(added))
                {
                    return false;
                }
            }
            Modifiers.Add(added);
            Stats.Add(added);
            return true;
        }

        public bool Remove_Stat_Regeneration(Stat_Regeneration removed)
        {
            return Stats.Remove(removed) && Regenerators.Remove(removed);
        }
        public bool Remove_Stat_Resource(Stat_Resource removed)
        {
            return Stats.Remove(removed) && Resources.Remove(removed);
        }
        public bool Remove_Stat_Modifier(Stat_Modifier removed)
        {
            return Stats.Remove(removed) && Modifiers.Remove(removed);
        }

        public bool Add_Stats_Regeneration(List<Stat_Regeneration> added)
        {
            bool returned = true;
            foreach(Stat_Regeneration regen in added)
            {
                if (!Add_Stat_Regeneration(regen))
                {
                    returned = false;
                }
            }
            return returned;
        }
        public bool Add_Stats_Resource(List<Stat_Resource> added)
        {
            bool returned = true;
            foreach (Stat_Resource res in added)
            {
                if (!Add_Stat_Resource(res))
                {
                    returned = false;
                }
            }
            return returned;
        }
        public bool Add_Stats_Modifier(List<Stat_Modifier> added)
        {
            bool returned = true;
            foreach (Stat_Modifier mod in added)
            {
                if (!Add_Stat_Modifier(mod))
                {
                    returned = false;
                }
            }
            return returned;
        }

        public bool Remove_Stats_Regeneration(List<Stat_Regeneration> removed)
        {
            bool returned = true;
            foreach (Stat_Regeneration regen in removed)
            {
                if (!Remove_Stat_Regeneration(regen))
                {
                    returned = false;
                }
            }
            return returned;
        }
        public bool Remove_Stats_Modifier(List<Stat_Modifier> removed)
        {
            bool returned = true;
            foreach (Stat_Modifier mod in removed)
            {
                if (!Remove_Stat_Modifier(mod))
                {
                    returned = false;
                }
            }
            return returned;
        }
        public bool Remove_Stats_Resource(List<Stat_Resource> removed)
        {
            bool returned = true;
            foreach (Stat_Resource res in removed)
            {
                if (!Remove_Stat_Resource(res))
                {
                    returned = false;
                }
            }
            return returned;
        }

        public bool Add_Skill(Skill added)
        {
            foreach(Skill skill in Skills)
            {
                if (skill.Equals(added))
                {
                    return false;
                }
            }
            Skills.Add(added);
            return true;
        }
        public bool Add_Skills(List<Skill> added)
        {
            bool returned = true;
            foreach(Skill skill in added)
            {
                if (!Add_Skill(skill))
                {
                    returned = false;
                }
            }
            return returned;
        }

        public bool Remove_Skill(Skill removed)
        {
            return Skills.Remove(removed);
        }
        public bool Remove_Skills(List<Skill> removed)
        {
            bool returned = true;
            foreach(Skill skill in removed)
            {
                if (!Remove_Skill(skill))
                {
                    returned = false;
                }
            }
            return returned;
        }

        public External_Effect Send(External_Effect effect)
        {
            foreach(Stat_Modifier modifier in Modifiers)
            {
                modifier.Send(effect);
            }
            return effect;
        }
        public bool Recieve(External_Effect effect)
        {
            foreach (Stat_Modifier modifier in Modifiers)
            {
                modifier.Recieve(effect);
            }
            foreach (Stat stat in Stats)
            {
                effect.Apply(stat);
            }
            return true;
        }
        public bool Move(Direction direction)
        {
            Tile next = Position.Get_Neighbour(direction);
            if (next.Can_Move(Movement_Mode) && Speed.Get_Done() && next!=null)
            {
                
                Position.Leave();
                next.Enter(this);
                Orientation = direction;
                Speed.Start();
                return true;
            }
            else
            {
                return false;
            }
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
        public Bitmap Get_Image()
        {
            return Image;
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
            return Movement_Mode;
        }
        public Direction Get_Orientation()
        {
            return Orientation;
        }
        public Cooldown Get_Speed()
        {
            return Speed;
        }
        public List<Skill> Get_Skills()
        {
            return Skills;
        }

        public string Get_Stat_List()
        {
            string returned =
            "Stat "+Environment.NewLine;
          
            return returned;
        }
        public string Get_Skill_List()
        {
            string returned = "Skills" + Environment.NewLine;

            return returned;
        }

        public bool Set_Image(Bitmap image)
        {
            Image = image;
            return true;
        }
        public bool Set_Movement_Mode(MovementMode mode)
        {
            Movement_Mode = mode;
            return true;
        }
        public bool Set_Speed(Cooldown speed)
        {
            Speed = speed;
            return true;
        }
        public bool Set_Orientation(Direction direction)
        {
            Orientation = direction;
            return true;
        }
        public bool Set_Position(Tile tile)
        {
            Position = tile;
            return true;
        }
        public bool Set_Sight(int sight)
        {
            if (sight > 0) Sight = sight;
            return true;
        }
        public bool Set_Resources(List<Stat_Resource> resources )
        {
            return Add_Stats_Resource(resources);
        }
        public bool Set_Regenerators(List<Stat_Regeneration> regenerators)
        {

            return Add_Stats_Regeneration(regenerators);
        }
        public bool Set_Modifiers(List<Stat_Modifier> modifiers)
        {
            return Add_Stats_Modifier(modifiers);

        }
        public bool Set_Equipment(Dictionary<Slot,Item> equipment)
        {
            Equipment = equipment;
            return true;
        }
        public bool Set_Inventory(List<Item> inventory)
        {
            Inventory = inventory;
            return true;
        }
        public bool Set_Skills(List<Skill> skills)
        {
            Skills = skills;
            return true;
        }

        public override string ToString(string tab)
        {
            string temp =
            tab + base.ToString() +
            tab + MyParser.Write(Sight, "Int", "Sight")+
            tab + MyParser.Write(Speed, "Cooldown", "Speed")+
            tab + MyParser.Write(Position, "Tile", "Position")+
            tab + MyParser.Write(Image, "Image", "Image") +
            tab + MyParser.Write(Orientation, "Direction", "Orientation")+
            tab + MyParser.Write(Regenerators, "Array<Stat_Regeneration>", "Regenerators")+
            tab + MyParser.Write(Modifiers, "Array<Stat_Modifier>", "Modifiers")+
            tab + MyParser.Write(Resources, "Array<Stat_Resource>", "Resources") +
            tab + MyParser.Write(Movement_Mode, "MovementMode", "Movement_Mode") +
            tab + MyParser.Write(Skills, "Array<Skill>", "Skills") +
            tab + MyParser.Write(Inventory, "Array<Item>", "Inventory") +
            tab + MyParser.Write(Equipment, "Dictionary<Slot,Item>", "Equipment");
            return temp;
        }
        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Sight": return Set_Sight((int)value);
                case "Image":return Set_Image((Bitmap)value);
                case "Regenerators":return Set_Regenerators(MyParser.Convert_Array<Stat_Regeneration>(value));
                case "Resources": return Set_Resources(MyParser.Convert_Array<Stat_Resource>(value));
                case "Modifiers": return Set_Modifiers(MyParser.Convert_Array<Stat_Modifier>(value));
                case "Position":return Set_Position((Tile)value);
                case "Speed":return Set_Speed((Cooldown)value);
                case "Orientation":return Set_Orientation((Direction)value);
                case "Movement_Mode":return Set_Movement_Mode((MovementMode)value);
                case "Skills":return Set_Skills(MyParser.Convert_Array<Skill>(value));
                case "Equipment":return Set_Equipment(MyParser.Convert_Dictionary<Slot, Item>(value));
                case "Inventory":return Set_Inventory(MyParser.Convert_Array<Item>(value));
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Sight": return Get_Sight();
                case "Speed":return Get_Speed();
                case "Image":return Get_Image();
                case "Regenerators":return Get_Regenerators();
                case "Resources":return Get_Resources();
                case "Modifiers":return Get_Modifiers();
                case "Position":return Get_Position();
                case "Equipment":return Get_Equipment();
                case "Iventory":return Get_Inventory();
                case "Skills":return Get_Skills();
                case "Orientation":return Get_Orientation();
                case "Movement_Mode":return Get_Movement_Mode();
                default: return base.Get_Variable(name);
            }
        }
        public override object Call_Function(string name, object[] args)
        {
            switch (name)
            {
                case "Equip":return Equip((Item)args[0]);
                case "UnEquip":return UnEquip((Item)args[0]);
                case "Calculate_Stats":return Calculate_Stats();
                case "Add_Item":return Add_Item((Item)args[0]);
                case "Add_Items":return Add_Items((List<Item>)args[0]);
                case "Remove_Item": return Remove_Item((Item)args[0]);
                case "Remove_Items": return Remove_Items((List<Item>)args[0]);
                case "Add_Skill":return Add_Skill((Skill)args[0]);
                case "Add_Skills":return Add_Skills((List<Skill>)args[0]);
                case "Remove_Skill":return Remove_Skill((Skill)args[0]);
                case "Remove_Skills":return Remove_Skills((List<Skill>)args[0]);
                case "Add_Stat_Modifier":return Add_Stat_Modifier((Stat_Modifier)args[0]);
                case "Add_Stat_Regeneration":return Add_Stat_Regeneration((Stat_Regeneration)args[0]);
                case "Add_Stat_Resource":return Add_Stat_Resource((Stat_Resource)args[0]);
                case "Remove_Stat_Modifier":return Remove_Stat_Modifier((Stat_Modifier)args[0]);
                case "Remove_Stat_Regeneration":return Remove_Stat_Regeneration((Stat_Regeneration)args[0]);
                case "Remove_Stat_Resource":return Remove_Stat_Resource((Stat_Resource)args[0]);
                case "Add_Stats_Modifier":return Add_Stats_Modifier((List<Stat_Modifier>)args[0]);
                case "Add_Stats_Regeneration":return Add_Stats_Regeneration((List<Stat_Regeneration>)args[0]);
                case "Add_Stats_Resource":return Add_Stats_Resource((List<Stat_Resource>)args[0]);
                case "Remove_Stats_Modifier": return Remove_Stats_Modifier((List<Stat_Modifier>)args[0]);
                case "Remove_Stats_Regeneration": return Remove_Stats_Regeneration((List<Stat_Regeneration>)args[0]);
                case "Remove_Stats_Resource": return Remove_Stats_Resource((List<Stat_Resource>)args[0]);
                case "Send":return Send((External_Effect)args[0]);
                case "Recieve":return Recieve((External_Effect)args[0]);
                case "Move":return Move((Direction)args[0]);
                case "Drop_Loot":return Drop_Loot();
                
                default: return base.Call_Function(name, args);
            }
        }
    }
}
