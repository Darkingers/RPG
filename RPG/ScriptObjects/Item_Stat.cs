using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Item_Stat:ScriptObject
    {
        protected double Flat;
        protected double Percent;
        protected Stat Target;

        public Item_Stat()
        {
            Copy(null, 0, 0);
        }
        public Item_Stat(ScriptObject scriptobject,Stat target,double flat,double percent):base(scriptobject)
        {
            Copy(target, flat, percent);
        }
        public Item_Stat(Item_Stat cloned)
        { 
            Copy(cloned);
        }
        public bool Copy(Item_Stat copied)
        {
           return base.Copy(copied)&&Copy(copied.Get_Target(),copied.Get_Flat(), copied.Get_Percent());
        }
        public bool Copy(Stat target,double flat,double percent)
        {
            return 
            Set_Target(target)&&
            Set_Flat(flat)&&
            Set_Percent(percent);
        }
        public override ScriptObject Clone()
        {
            return new Item_Stat(this);
        }

        public bool Apply(Stat stat)
        {
            if (Target.Equals(stat))
            {
                bool returned = true;
                if (!stat.Modify_Flat(Flat))
                {
                    returned = false;
                }
                if (!stat.Modify_Percent(Percent))
                {
                    returned = false;
                }
                return returned;
            }
            return false;
        }
        public bool Remove(Stat stat)
        {
            if (Target.Equals(stat))
            {
                bool returned = true;
                if (!stat.Modify_Flat(-Flat))
                {
                    returned = false;
                }
                if (!stat.Modify_Percent(-Percent))
                {
                    returned = false;
                }
                return returned;
            }
            return false;
        }

        public bool Set_Flat(double flat)
        {
            Flat = flat;
            return true;
        }
        public bool Set_Percent(double percent)
        {
            Percent = percent;
            return true;
        }
        public bool Set_Target(Stat target)
        {
            Target = target;
            return true;
        }

        public double Get_Flat()
        {
            return Flat;
        }
        public double Get_Percent()
        {
            return Percent;
        }
        public Stat Get_Target()
        {
            return Target;
        }

        public override string ToString()
        {
            return Target.Get_Identifier() + " Flat:" + Flat + " Percent:" + Percent;
        }
        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Flat": return Set_Flat((double)value);
                case "Percent": return Set_Percent((double)value);
                case "Target": return Set_Target((Stat)value);
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Flat": return Get_Flat();
                case "Percent": return Get_Percent();
                case "Target": return Get_Target();
                default: return base.Get_Variable(name);

            }
        }
        public override int Compare(object compared)
        {
            if (((Item_Stat)compared).Equals(this))
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
                case "Remove": return Remove((Stat)args[0]);
                case "Apply": return Apply((Stat)args[0]);
                default: return base.Call_Function(name, args);
            }
        }
    }
}
