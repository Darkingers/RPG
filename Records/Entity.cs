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
            Copy(
                null,
                MovementMode.Walk,
                new Cooldown(new ScriptObject(),1,0,null),
                2, 
                null, 
                Direction.Right,
                new List<Stat_Resource>(),
                new List<Stat_Modifier>(), 
                new List<Stat_Regeneration>(), 
                new Dictionary<Slot, Item>(), 
                new List<Item>(),
                new List<Skill>()
                );
        }
        public Entity(Entity cloned)
        {
            Stats = new List<Stat>();
            Copy(cloned);
        }
        public Entity(
            Record type,
            Bitmap image,
            MovementMode movement,
            Cooldown speed, 
            int sight,
            Tile position,
            Direction orientation,
            List<Stat_Resource> resources,  
            List<Stat_Modifier> modifiers , 
            List<Stat_Regeneration> regenerators, 
            Dictionary<Slot, Item> equipment, 
            List<Item> inventory,
            List<Skill> skills
        ):base(type)
        {
            Stats = new List<Stat>();
            Copy(image, movement, speed, sight, position, orientation, resources, modifiers, regenerators, equipment, inventory,skills);
        }
        public bool Copy(
            Bitmap image,
            MovementMode movement,
            Cooldown speed,
            int sight,
            Tile position,
            Direction orientation,
            List<Stat_Resource> resources ,
            List<Stat_Modifier> modifiers,
            List<Stat_Regeneration> regenerators,
            Dictionary<Slot, Item> equipment,
            List<Item> inventory,
            List<Skill> skills
            )
        {
            return
            Set_Speed(speed) &&
            Set_Sight(sight) &&
            Set_Position(position) &&
            Set_Orientation(orientation) &&
            Set_Image(image) &&
            Set_Resources(resources) &&
            Set_Modifiers(modifiers) &&
            Set_Regenerators(regenerators) &&
            Set_Equipment(equipment) &&
            Set_Inventory(inventory) &&
            Set_Movement_Mode(movement) &&
            Set_Skills(skills);
        }
        public bool Copy(Entity copied)
        {
            return
                 base.Copy(copied)
                 &&
                 Copy(
                 copied.Get_Image(), 
                 copied.Get_Movement_Mode(),
                 new Cooldown(copied.Get_Speed()), 
                 copied.Get_Sight(),
                 copied.Get_Tile(), 
                 copied.Get_Orientation(), 
                 new List<Stat_Resource>(copied.Get_Resources()), 
                 new List<Stat_Modifier>(copied.Get_Modifiers()), 
                 new List<Stat_Regeneration>(copied.Get_Regenerators()),
                 new Dictionary<Slot,Item>(copied.Get_Equipment()),
                 new List<Item>( copied.Get_Inventory()),
                 new List<Skill>(copied.Get_Skills())
                 );
        }
        public override ScriptObject Clone()
        {
            return new Entity(this);
        }
        public override bool Assign(Record copied)
        {
            return Copy((Entity)copied);
        }
        public void RenderItems(Graphics e, int Framewidth, int Frameheight, int width, int height, Entity observer)
        {
            Region region = new Region(new Rectangle(Framewidth,Frameheight,width-2*Framewidth,height-2*Frameheight));
            SolidBrush brush = new SolidBrush(Color.FromArgb(158,114,84));
            e.FillRegion(brush, region);


            
        }
        public void RenderInventory()
        {
           
        }
        public void RenderEquipment()
        {
            
        }

        public bool Equip(Item item)
        {
            if (item != null)
            {
                Inventory.Remove(item);
                Item switched = Equipment[item.Get_Slot()];

                if (switched != null)
                {
                    UnEquip(switched);
                    Inventory.Add(switched);
                }
                Equipment[item.Get_Slot()] = item;
                foreach (Stat stat in Stats)
                {
                    item.Apply(stat);
                }
                foreach (Skill skill in item.Get_Skills())
                {
                   Add_Skill(skill);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UnEquip(Item item)
        {
            if (item != null)
            {
                Equipment[item.Get_Slot()] = null;
                foreach (Stat stat in Stats)
                {
                    item.Remove(stat);
                }
                foreach (Skill skill in item.Get_Skills())
                {
                    Remove_Skill(skill);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool Kill()
        {
            if (Position != null)
            {
                Position.Leave();
                Position = null;
                return true;
            }
            else return false;
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

        public Effect Send(Effect effect)
        {
            foreach(Stat_Modifier modifier in Modifiers)
            {
                modifier.Send(effect);
            }
            return effect;
        }
        public bool Recieve(Effect effect)
        {
            foreach (Stat_Modifier modifier in Modifiers)
            {
                modifier.Recieve(effect);
            }
            foreach (Stat stat in Stats)
            {
                if (effect.Apply(stat))
                {
                    stat.Try_Trigger(this);
                }
            }
            return true;
        }
        public bool Move(Direction direction)
        {
            if (Position == null)
            {
                return false;
            }
            Tile next = Position.Get_Neighbour(direction);
            Set_Orientation(direction);
            if (next != null && next.Can_Move(Movement_Mode) && Speed.Get_Done())
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
        public string Get_Details()
        {
            string returned = Name+Environment.NewLine+Description+Environment.NewLine;
            foreach(Stat stat in Stats)
            {
                returned += stat.ToString() + Environment.NewLine;
            }
            return returned;
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
            
            if (Image != null)
            {
                switch (Orientation)
                {
                    case Direction.Up:
                        switch (direction)
                        {
                            case Direction.Up: break;
                            case Direction.Down:Image.RotateFlip(RotateFlipType.Rotate180FlipNone); break;
                            case Direction.Left: Image.RotateFlip(RotateFlipType.Rotate270FlipNone); break;
                            case Direction.Right: Image.RotateFlip(RotateFlipType.Rotate90FlipNone); break;
                        }
                        break;
                    case Direction.Down:
                        switch (direction)
                        {
                            case Direction.Up: Image.RotateFlip(RotateFlipType.Rotate180FlipNone); break;
                            case Direction.Down: break;
                            case Direction.Left: Image.RotateFlip(RotateFlipType.Rotate90FlipNone); break;
                            case Direction.Right: Image.RotateFlip(RotateFlipType.Rotate270FlipNone); break;
                        }
                        break;
                    case Direction.Left:
                        switch (direction)
                        {
                            case Direction.Up: Image.RotateFlip(RotateFlipType.Rotate90FlipNone); break;
                            case Direction.Down: Image.RotateFlip(RotateFlipType.Rotate270FlipNone); break;
                            case Direction.Left: break;
                            case Direction.Right: Image.RotateFlip(RotateFlipType.Rotate180FlipNone); break;
                        }
                        break;
                    case Direction.Right:
                        switch (direction)
                        {
                            case Direction.Up: Image.RotateFlip(RotateFlipType.Rotate270FlipNone); break;
                            case Direction.Down: Image.RotateFlip(RotateFlipType.Rotate90FlipNone); break;
                            case Direction.Left: Image.RotateFlip(RotateFlipType.Rotate180FlipNone); break;
                            case Direction.Right: break;
                        }
                        break;
                }
            }

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
            Resources = resources;
            if (resources != null)
            {
                foreach(Stat_Resource added in resources)
                {
                    Stats.Add(added);
                }
            }
            return true;
        }
        public bool Set_Regenerators(List<Stat_Regeneration> regenerators)
        {

            Regenerators = regenerators;
            if (regenerators != null)
            {
                foreach (Stat_Regeneration added in regenerators)
                {
                    Stats.Add(added);
                    added.Bind(Resources);
                }
            }
            return true;
        }
        public bool Set_Modifiers(List<Stat_Modifier> modifiers)
        {
            Modifiers = modifiers;
            if (modifiers != null)
            {
                foreach (Stat_Modifier added in modifiers)
                {
                    Stats.Add(added);
                }
            }
            return true;

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

        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Sight": return Set_Sight((int)value);
                case "Image":return Set_Image((Bitmap)value);
                case "Regenerators":return Set_Regenerators(Converter.Convert_Array<Stat_Regeneration>(value));
                case "Resources": return Set_Resources(Converter.Convert_Array<Stat_Resource>(value));
                case "Modifiers": return Set_Modifiers(Converter.Convert_Array<Stat_Modifier>(value));
                case "Position":return Set_Position((Tile)value);
                case "Speed":return Set_Speed((Cooldown)value);
                case "Orientation":return Set_Orientation((Direction)value);
                case "Movement_Mode":return Set_Movement_Mode((MovementMode)value);
                case "Skills":return Set_Skills(Converter.Convert_Array<Skill>(value));
                case "Equipment":return Set_Equipment(Converter.Convert_Dictionary<Slot, Item>(value));
                case "Inventory":return Set_Inventory(Converter.Convert_Array<Item>(value));
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
                case "Tile": return Get_Tile();
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
                case "Kill":return Kill();
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
                case "Send":return Send((Effect)args[0]);
                case "Recieve":return Recieve((Effect)args[0]);
                case "Move":return Move((Direction)args[0]);
                case "Drop_Loot":return Drop_Loot();
                
                default: return base.Call_Function(name, args);
            }
        }
    }
}
