using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Cost_Value:ScriptObject
    {
        protected Stat_Resource Target;
        protected double Flat_Cost;
        protected double Percent_Cost;

        public Cost_Value()
        {
            Copy(null, 0, 0);
        }
        public Cost_Value(ScriptObject scriptobject,Stat_Resource stat, double flat_cost, double percent_cost):base(scriptobject)
        {
            Copy(stat, flat_cost, percent_cost);
        }
        public Cost_Value(Cost_Value cloned)
        {
            Copy(cloned);
        }
        public bool Copy(Cost_Value copied)
        {
            return base.Copy(copied) && Copy(copied.Get_Target(), copied.Get_Flat_Cost(), copied.Get_Percent_Cost());
        }
        public bool Copy(Stat_Resource stat,double  flat_cost,double percent_cost)
        {
            return 
            Set_Target(Target)&&
            Set_Flat_Cost(flat_cost)&&
            Set_Percent_Cost(percent_cost);
        }
        public override ScriptObject Clone()
        {
            return new Cost_Value(this);
        }

        public bool Equals(Stat_Resource resource)
        {
            return Target.Equals(resource);
        }
        public bool Can_Pay(Stat_Resource resource)
        {
            double max = resource.Get_Max();
            if (resource.Get_Current() >= max * (Percent_Cost/100) + Flat_Cost)
            {
                return true;
            }
            else return false;
        }
        public bool Pay(Stat_Resource resource)
        {
            double max = resource.Get_Max();
            return resource.Modify_Current(-Flat_Cost + -(Percent_Cost/100) * max);
        }
        public bool Try_Pay(Stat_Resource resource)
        {
            if (!Can_Pay(resource))
            {
                return false;
            }
            else if (!Pay(resource))
            {
                return false;
            }
            else return true;
        }

        public Stat_Resource Get_Target()
        {
            return Target;
        }
        public double Get_Flat_Cost()
        {
            return Flat_Cost;
        }
        public double Get_Percent_Cost()
        {
            return Percent_Cost;
        }

        public bool Set_Target(Stat_Resource target)
        {
            Target = target;
            return true;
        }
        public bool Set_Flat_Cost(double flat)
        {
            Flat_Cost = flat;
            if (Flat_Cost < 0)
            {
                Flat_Cost = 0;
                return false;
            }
            return true;
        }
        public bool Set_Percent_Cost(double percent)
        {
            Percent_Cost = percent;
            if (Percent_Cost < 0)
            {
                Percent_Cost = 0;
                return false;
            }
            else if (Percent_Cost > 100)
            {
                Percent_Cost = 100;
                return false;
            }
            else return true;
        }

        public override string ToString()
        {
            return Target.Get_Identifier() + " Flat:" + Flat_Cost + " Percent:" + Percent_Cost;
        }
        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Target": return Set_Target((Stat_Resource)value);
                case "Flat": return Set_Flat_Cost((double) value);
                case "Percent":return Set_Percent_Cost((double)value);
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Target":return Get_Target();
                case "Flat":return Get_Flat_Cost();
                case "Percent":return Get_Percent_Cost();
                default: return base.Get_Variable(name);
            }
        }
        public override int Compare(object compared)
        {
            if (((Cost_Value)compared).Equals(this))
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        public override object Call_Function(string name, object[] args)
        {
            switch (name)
            {
                case "Can_Pay": return Can_Pay((Stat_Resource)args[0]);
                case "Pay": return Pay((Stat_Resource)args[0]);
                case "Try_Pay": return Try_Pay((Stat_Resource)args[0]);
                default: return base.Call_Function(name, args);
            }
        }
    }
}
