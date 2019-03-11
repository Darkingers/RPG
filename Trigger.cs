using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    enum RelationSign { More, Less, Equal,Equal_Or_More,Equal_Or_Less};
    enum Numeral { Integer, Rational };

    class Trigger
    {
        protected double Threshold;
        protected RelationSign Relation;
        protected Numeral Type;
        protected List<Skill> Skills;

        public Trigger(
            double threshold = 0
           ,RelationSign relation = RelationSign.Equal
           ,Numeral type = Numeral.Integer
           ,List<Skill> skills = null
            )
        {
            Set_Threshold(threshold);
            Set_Relation(relation);
            Set_Numeral(type);
            Set_Skills(skills);
        }
        public Trigger(Trigger cloned)
        {
            Set_Threshold(cloned.Get_Threshold());
            Set_Relation(cloned.Get_Relation());
            Set_Numeral(cloned.Get_Numeral());
            Set_Skills(new List<Skill>(cloned.Get_Skills()));
        }
        public Trigger Clone()
        {
            return new Trigger(Threshold, Relation, Type,new List<Skill>(Skills));
        }

        public void Load(string[] args, ref int iter)
        {
            for (string token = args[++iter]; token != "[END]"; token = args[++iter])
            {
                switch (token)
                {
                    case "[Trigger_Threshold]": Set_Threshold(double.Parse(args[++iter])); break;
                    case "[Relation_Type]":switch (args[++iter])
                        {
                            case "More": Set_Relation(RelationSign.More);break;
                            case "Less": Set_Relation(RelationSign.Less); break;
                            case "Equal_Or_More": Set_Relation(RelationSign.Equal_Or_More); break;
                            case "Equal_Or_Less": Set_Relation(RelationSign.Equal_Or_Less); break;
                            case "Equal": Set_Relation(RelationSign.Equal); break;
                            default: throw new Exception("[Trigger]:[Relation_Type]:Invalid token: " + args[iter]);
                        }break;
                    case "[Number_Type]":switch (args[++iter])
                        {
                            case "Integer": Set_Numeral(Numeral.Integer); break;
                            case "Percent": Set_Numeral(Type = Numeral.Rational); break;
                        }break;
                    case "[Skills]":for (string token2 = args[++iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            Add_Skill(token2);
                        }break;
                    default: System.Console.Write("[Trigger]:Invalid token:" + token); break;
                }
            }
        }
        public string Write(string tab)
        {
            string returned =
            tab+"[Trigger]" + Environment.NewLine +
            tab + "  [Trigger_Threshold] " + Threshold + Environment.NewLine +
            tab + "  [Relation_Type] ";
            switch (Relation)
            {
                case RelationSign.Equal: returned += "Equal"; break;
                case RelationSign.More: returned += "More"; break;
                case RelationSign.Less: returned += "Less"; break;
            }
            returned += Environment.NewLine +
            tab + "  [Number_Type] ";
            switch (Type)
            {
                case Numeral.Integer: returned += "Integer"; break;
                case Numeral.Rational: returned += "Percent"; break;
            }
            returned += Environment.NewLine +
            tab + "  [Skills] ";
            foreach (Skill iter in Skills)
            {
                returned += tab + "    " + iter.Get_Identifier() + Environment.NewLine;
            }
            returned +=
            tab + "  [END]" + Environment.NewLine +
            tab + "[END]";
            return returned;
        }
        public override string ToString()
        {
            string returned =
            "[Trigger]" + Environment.NewLine +
            "  [Trigger_Threshold] " + Threshold + Environment.NewLine +
            "  [Relation_Type] ";
            switch (Relation)
            {
                case RelationSign.Equal: returned += "Equal"; break;
                case RelationSign.More: returned += "More"; break;
                case RelationSign.Less: returned += "Less"; break;
            }
            returned += Environment.NewLine +
            "  [Number_Type] ";
            switch (Type)
            {
                case Numeral.Integer: returned += "Integer"; break;
                case Numeral.Rational: returned += "Percent"; break;
            }
            returned += Environment.NewLine +
            "  [Skills] ";
            foreach (Skill iter in Skills)
            {
                returned +="    "+iter.Get_Identifier() + Environment.NewLine;
            }
            returned += 
            "  [END]" + Environment.NewLine +
            "[END]";
            return returned;

        }

        public void Add_Skill(Skill _added)
        {
            Skills.Add(_added);
        }
        public void Add_Skill(string identifier)
        {
            Skills.Add((Skill)Database.Get(identifier).Clone());
        }
        public void Remove_Skill(Skill _removed)
        {
            Skills.Remove(_removed);
        }
        public void Remove_Skill(string identifier)
        {
            Skills.Remove((Skill)Database.Get(identifier).Clone());
        }

        public bool Can_Trigger(double _value_int, double _value_percent, Entity _source)
        {
            double actual_value = -1;
            switch (Type)
            {
                case Numeral.Integer: actual_value = _value_int; break;
                case Numeral.Rational: actual_value = _value_percent; break;
            }
            switch (Relation)
            {
                case RelationSign.More:
                    if (actual_value > Threshold)
                    {
                        return true;
                    }
                    break;
                case RelationSign.Less:
                    if (actual_value < Threshold)
                    {
                        return true;
                    }
                    break;
                case RelationSign.Equal:
                    if (actual_value == Threshold)
                    {
                        return true;
                    }
                    break;
            }
            return false;

        }
        public void Try_Trigger(double _value_int, double _value_percent, Entity _source)
        {
            if (!Can_Trigger(_value_int, _value_percent, _source))
            {
                return;
            }
            foreach (Skill iter in Skills)
            {
                if (iter.Can_Use(_source,_source))
                {
                    iter.Use(_source, _source);
                }
            }
        }

        public double Get_Threshold()
        {
            return Threshold;
        }
        public RelationSign Get_Relation()
        {
            return Relation;
        }
        public Numeral Get_Numeral()
        {
            return Type;
        }
        public List<Skill> Get_Skills()
        {
            return Skills;
        }

        public void Set_Threshold(double threshold)
        {
            Threshold = threshold;
        }
        public void Set_Relation(RelationSign relation)
        {
            Relation = relation;
        }
        public void Set_Numeral(Numeral type)
        {
            Type = type;
        }
        public void Set_Skills(List<Skill> skills)
        {
            Skills = skills;
        }

    }
}
