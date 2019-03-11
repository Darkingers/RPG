using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Item:Type
    {
        protected Slot slot;
        protected double rarity;
        protected double value;
        protected List<Skill> skills;
        protected List<Item_Stat> stats;

        public Item(Mod _mod = null
            , string _name = ""
            , string _description = ""
            , List<Tag> _tags = null
            , Slot _slot = null
            , double _rarity=0.1
            , double _value=100
            , List<Skill> _skills=null
            , List<Item_Stat> _stats = null
            ) :base(_mod,_name,_description,_tags)
        {
            slot =_slot;
            if (_rarity < 0)
            {
                rarity = 0.1;
            }
            else
            {
                rarity = _rarity;
            }
            if (_value < 0)
            {
                value = 100;
            }
            else
            {
                value = _value;
            }
            if (_skills == null)
            {
                skills = new List<Skill>();
            }
            else
            {
                skills = _skills;
            }
            if (_stats == null)
            {
                stats = new List<Item_Stat>();
            }
            else
            {
                stats = _stats;
            }
        }

        public override Type Clone()
        {
            return new Item(Source, Name, Description, Tags, slot, rarity, value, new List<Skill>(skills), new List<Item_Stat>(stats));
        }

        public void Apply(Stat modified)
        {
            foreach(Item_Stat stat in stats)
            {
                stat.Apply(modified);
            }
        }
        public void Remove(Stat modified)
        {
            foreach (Item_Stat stat in stats)
            {
                stat.Remove(modified);
            }
        }

        public override void Load(string[] args, ref int iter, Mod _mod)
        {
            for (string token = args[iter]; token != "[END]"; token = args[++iter])
            {
                switch (token)
                {
                    case "[Type]":base.Load(args, ref iter, _mod);break;
                    case "[Rarity]":rarity = double.Parse(args[++iter]);break;
                    case "[Slot]": slot = Database.Get_Slot(args[++iter]);break;
                    case "[Value]":value = double.Parse(args[++iter]); break;
                    case "[Item_Stats]": for (string token2 = args[iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            Item_Stat added=new Item_Stat();
                            added.Load(args, ref iter);
                            stats.Add(added);
                        }break;
                    case "[Skills]": for (string token2 = args[++iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            skills.Add((Skill)Database.Get(token2).Clone());
                        } break;
                    default: System.Console.Write("Invalid token:" + token); break;
                }
            }
        }
        public override string Save(string tab)
        {
            string temp =
            tab + "[Item]" + Environment.NewLine +
            base.Save(tab + "  ") + Environment.NewLine +
            tab + "  [Rarity] " + rarity + Environment.NewLine +
            tab + "  [Value] " + value + Environment.NewLine +
            tab + "  [Slot] " + slot.Get_Name() + Environment.NewLine +
            tab + "  [Skills]" + Environment.NewLine;
            foreach (Skill skill in skills)
            {
                temp += tab +"    " + skill.Get_Identifier() + Environment.NewLine;
            }
            temp +=tab+"  [END]"+ Environment.NewLine+
            tab + "  [Item_Stats]" + Environment.NewLine;
            foreach(Item_Stat stat in stats)
            {
                temp += stat.Write(tab+"    ")+ Environment.NewLine;
            }
            temp +=tab+ "  [END]"+ Environment.NewLine+tab+"[END]";
            return temp;
        }

        public Slot Get_Slot()
        {
            return slot;
        }
    }
}
