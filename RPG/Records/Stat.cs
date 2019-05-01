using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Stat:Record
    {
        
        protected double Flat;
        protected double Percent;

        public Stat()
        {
            Copy(0, 0);
        }
        public Stat(Record type,double flat,double percent):base(type)
        {
            Copy(flat, percent);
        }
        public Stat(Stat cloned)
        {
            Copy(cloned);
        }
        public bool Copy(double flat,double percent)
        {
            return 
            Set_Flat(flat) &&
            Set_Percent(percent);
        }
        public bool Copy(Stat copied)
        {
            return base.Copy(copied) && Copy(copied.Get_Flat(), copied.Get_Percent());
        }
        public override ScriptObject Clone()
        {
            return new Stat(this);
        }
        public override bool Assign(Record copied)
        {
            return Copy((Stat)copied);
        }

        public virtual bool Modify_Current(double value)
        {
            return false;
        }
        public bool Modify_Flat(double value)
        {
            return Set_Flat(Flat + value);
        }
        public bool Modify_Percent(double value)
        {
           return Set_Percent(Percent + value);
        }
        public virtual bool Recalculate()
        {
            return false;
        }
        public virtual bool Try_Trigger(Entity source)
        {
            return false;
        }

        public Stat Get_Stat()
        {
            return this;
        }
        public double Get_Flat()
        {
            return Flat;
        }
        public double Get_Percent()
        {
            return Percent;
        }

        public bool Set_Stat(Stat stat)
        {
            Copy(stat);
            return true;
        }
        public bool Set_Flat(double value)
        {
            Flat = value;
            if (Flat < 0)
            {
                Flat = 0;
                return false;
            }
            Recalculate();
            return true;
        }
        public bool Set_Percent(double value)
        {
            Percent = value;
            if (Percent < 0)
            {
                Percent = 0;
                return false;
            }
            Recalculate();
            return true;
        }

        public override string ToString()
        {
            return base.ToString()+"/"+Flat + "," + Percent;
        }
        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Flat": return Set_Flat((double)value);
                case "Percent": return Set_Percent((double)value);
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Flat": return Get_Flat();
                case "Percent": return Get_Percent();
                default: return base.Get_Variable(name);
            }
        }
        public override object Call_Function(string name, object[] args)
        {
            switch (name)
            {
                case "Modify_Current":return Modify_Current((double)args[0]);
                case "Modify_Flat": return Modify_Flat((double)args[0]);
                case "Modify_Percent": return Modify_Percent((double)args[0]);
                case "Recalculate":return Recalculate();
                default: return base.Call_Function(name, args);
            }
        }
    }
}
