using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Item:Record
    {
        protected Slot Slot;
        protected double Rarity;
        protected double Worth;
        protected List<Skill> Skills;
        protected List<Item_Stat> Stats;

        public Item()
        {
            Copy(null, 0, 0, new List<Skill>(), new List<Item_Stat>());
        }
        public Item(Item cloned)
        {
            Copy(cloned);
        }
        public Item(Record type,Slot slot,double rarity,double value,List<Skill> skills,List<Item_Stat> stats) : base(type)
        {
            Copy(slot, rarity, value, skills, stats);
        }
        public bool Copy(Item copied)
        {
            return base.Copy(copied) && Copy(copied.Get_Slot(), copied.Get_Rarity(), copied.Get_Worth(), new List<Skill>(copied.Get_Skills()), new List<Item_Stat>(copied.Get_Stats()));
        }
        public bool Copy(Slot slot,double rarity,double value,List<Skill> skills,List<Item_Stat> stats)
        {
            return
            Set_Slot(slot)&&
            Set_Rarity(rarity)&&
            Set_Worth(value)&&
            Set_Skills(skills)&&
            Set_Stats(stats);
        }
        public override ScriptObject Clone()
        {
            return new Item(this);
        }
        public override bool Assign(Record copied)
        {
            return Copy((Item)copied);
        }

        public bool Apply(Stat modified)
        {
            bool returned = false;
            foreach(Item_Stat stat in Stats)
            {
                if (stat.Apply(modified))
                {
                    returned = true;
                }
            }
            return returned;
        }
        public bool Remove(Stat modified)
        {
            bool returned = false;
            foreach (Item_Stat stat in Stats)
            {
                if (stat.Remove(modified))
                {
                    returned = true;
                }
            }
            return returned;
        }

        public bool Set_Slot(Slot slot)
        {
            Slot = slot;
            return true;
        }
        public bool Set_Rarity(double rarity)
        {
            Rarity = rarity;
            if (Rarity < 0)
            {
                Rarity = 0;
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Set_Worth(double value)
        {
            Worth = value;
            if (Worth < 0)
            {
                Worth = 0;
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Set_Skills(List<Skill> skills)
        {
            Skills = skills;
            return true;
        }
        public bool Set_Stats(List<Item_Stat> stats)
        {
            Stats = stats;
            return true;
        }

        public Slot Get_Slot()
        {
            return Slot;
        }
        public double Get_Rarity()
        {
            return Rarity;
        }
        public double Get_Worth()
        {
            return Worth;
        }
        public List<Skill> Get_Skills()
        {
            return Skills;
        }
        public List<Item_Stat> Get_Stats()
        {
            return Stats;
        }

        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Rarity": return Set_Rarity((double)value);
                case "Worth": return Set_Worth((double)value);
                case "Skills": return Set_Skills(MyParser.Convert_Array<Skill>(value));
                case "Stats": return Set_Stats(MyParser.Convert_Array<Item_Stat>(value));
                case "Slot": return Set_Slot((Slot)value);
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Rarity": return Get_Rarity();
                case "Worth": return Get_Worth();
                case "Skills": return Get_Skills();
                case "Slot": return Get_Slot();
                case "Stats": return Get_Stats();
                default: return base.Get_Variable(name);
            }
        }
        public override object Call_Function(string name, object[] args)
        {
            switch (name)
            {
                case "Apply": return Apply((Stat)args[0]);
                case "Remove": return Remove((Stat)args[0]);
                default: return base.Call_Function(name, args);
            }
        }

    }
}
