using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    enum Relation { More='>', Less='<', Equal='='};

    class Trigger:ScriptObject
    {
        protected double Threshold;
        protected Relation Relation;
        protected Number Numeral;
        protected List<Skill> Skills;
        protected List<Script> Scripts;

        public Trigger()
        {
            Copy(0, Relation.Less, Number.Percent, new List<Skill>(),new List<Script>());
        }
        public Trigger(ScriptObject scriptobject, double threshold,Relation relation,Number numeral,List<Skill> skills,List<Script> scripts):base(scriptobject)
        {
            Copy(threshold, relation, numeral, skills,scripts);
        }
        public Trigger(Trigger cloned)
        {
            Copy(cloned);
        }
        public bool Copy(double threshold, Relation relation, Number numeral , List<Skill> skills,List<Script> scripts)
        {
            return Set_Threshold(threshold) &&
            Set_Relation(relation) &&
            Set_Numeral(numeral) &&
            Set_Skills(skills) &&
            Set_Scripts(scripts);
        }
        public bool Copy(Trigger copied)
        {
            return base.Copy(copied) && Copy(
                copied.Get_Threshold(), 
                copied.Get_Relation(), 
                copied.Get_Numeral(),
                copied.Get_Skills()==null?null:new List<Skill>(copied.Get_Skills()),
                copied.Get_Scripts()==null?null:new List<Script>(copied.Get_Scripts())
                ) ;
        }
        public override ScriptObject Clone()
        {
            return new Trigger(this);
        }
    
        public bool Add_Skill(Skill added)
        {
            if (Skills.Contains(added))
            {
                return false;
            }
            else
            {
                Skills.Add(added);
                return true;
            }
            
        }
        public bool Add_SkillS(List<Skill> added)
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
           return  Skills.Remove(removed);
        }
        public bool Remove_Skills(List<Skill> removed)
        {
            bool returned = true;
            foreach (Skill skill in removed)
            {
                if (!Remove_Skill(skill))
                {
                    returned = false;
                }

            }
            return returned;
        }

        public bool Do_Trigger(Entity source)
        {
            foreach (Skill iter in Skills)
            {
                if (iter.Can_Use(source, source.Get_Tile()))
                {
                    iter.Use(source, source.Get_Tile());
                }
            }
            foreach(Script script in Scripts)
            {
                object[] args = { source };
                script.Execute(args);
            }
            return true;
        }
        public bool Can_Trigger(double value_as_int, double value_as_percent, Entity source)
        {
            double actual_value = -1;
            switch (Numeral)
            {
                case Number.Flat: actual_value = value_as_int; break;
                case Number.Percent: actual_value = value_as_percent; break;
            }
            switch (Relation)
            {
                case Relation.More:
                    if (actual_value > Threshold)
                    {
                        return true;
                    }
                    break;
                case Relation.Less:
                    if (actual_value < Threshold)
                    {
                        return true;
                    }
                    break;
                case Relation.Equal:
                    if (actual_value == Threshold)
                    {
                        return true;
                    }
                    break;
            }
            return false;

        }
        public bool Try_Trigger(double value_as_int, double value_as_double, Entity source)
        {

            if (!Can_Trigger(value_as_int, value_as_double, source))
            {
                return false;
            }
            foreach (Script script in Scripts)
            {
                object[] args = { source };
                script.Execute(args);
            }
            foreach (Skill iter in Skills)
            {
                if (iter.Can_Use(source,source.Get_Tile()))
                {
                    iter.Use(source, source.Get_Tile());
                }
            }            return true;
        }

        public double Get_Threshold()
        {
            return Threshold;
        }
        public Relation Get_Relation()
        {
            return Relation;
        }
        public Number Get_Numeral()
        {
            return Numeral;
        }
        public List<Skill> Get_Skills()
        {
            return Skills;
        }
        public List<Script> Get_Scripts()
        {
            return Scripts;
        }

        public bool Set_Threshold(double threshold)
        {
            Threshold = threshold;
            if(Threshold < 0)
            {
                Threshold = 0;
                return false;
            }
            return true;
        }
        public bool Set_Relation(Relation relation)
        {
            Relation = relation;
            return true;
        }
        public bool Set_Numeral(Number type)
        {
            Numeral = type;
            return true;
        }
        public bool Set_Skills(List<Skill> skills)
        {
            Skills = skills;
            return true;
        }
        public bool Set_Scripts(List<Script> scripts)
        {
            Scripts = scripts;
            return true;
        }

        public override string ToString()
        {
            string returned = "value" + Relation + Threshold + Numeral+"/";
            foreach(Skill sk in Skills)
            {
                returned += ToString() + Environment.NewLine;
            }
            return returned;
        }
        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Threshold":return Set_Threshold((double)value);
                case "Relation":return Set_Relation((Relation)value);
                case "Numeral":return Set_Numeral((Number)value);
                case "Skills":return Set_Skills((Converter.Convert_Array<Skill>(value)));
                case "Scripts":return Set_Scripts(Converter.Convert_Array<Script>(value));
                default:return base.Set_Variable(name, value);
            }
        }
        public override int Compare(object compared)
        {
            if (((Trigger)compared).Equals(this)){
                return 0;
            }
            else{
                return -1;
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Threshold":return Get_Threshold();
                case "Numeral":return Get_Numeral();
                case "Relation": return Get_Relation();
                case "Skills":return Get_Skills();
                case "Scripts":return Get_Scripts();
                default:return base.Get_Variable(name);
            }
        }
        public override object Call_Function(string name, object[] args)
        {
            switch (name)
            {
                case "Add_Skill":return Add_Skill((Skill)args[0]);
                case "Add_Skills":return Add_SkillS((List<Skill>)args[0]);
                case "Remove_Skill":return Remove_Skill((Skill)args[0]);
                case "Remove_Skills":return Remove_Skills((List<Skill>)args[0]);
                case "Try_Trigger":return Try_Trigger((double)args[0], (double)args[1], (Entity)args[2]);
                case "Can_Trigger":return Can_Trigger((double)args[0], (double)args[1], (Entity)args[2]);
                case "Do_Trigger":return Do_Trigger((Entity)args[0]);
                default:return base.Call_Function(name, args);
            }
        }
    }
}
