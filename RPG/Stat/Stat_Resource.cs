using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Stat_Resource:Stat
    {
        protected double Max;
        protected double Current;
        protected List<Trigger> Triggers;

        public Stat_Resource()
        {
            Copy(0, new List<Trigger>());
        }
        public Stat_Resource(Stat type,double current, List<Trigger> triggers):base(type)
        {
            Copy(current, triggers);
        }
        public Stat_Resource(Stat_Resource cloned)
        {
            Copy(cloned);
        }
        public bool Copy(Stat_Resource copied)
        {
            return base.Copy(copied) && Copy(copied.Get_Current(),new List<Trigger>(copied.Get_Triggers()));
        }
        public bool Copy(double current , List<Trigger> triggers)
        {
            return 
            Set_Current(current) &&
            Set_Triggers(triggers);
        }
        public override ScriptObject Clone()
        {
            return new Stat_Resource(this);
        }
        public override bool Assign(Record copied)
        {
            return Copy((Stat_Resource)copied);
        }

        public override bool Modify_Current(double value)
        {
            Set_Current(value + Current);
            return true;
        }
        public override bool Recalculate()
        {
            Max = Flat * (Percent/100+1);
            if (Max < 0)
            {
                Max = 0;
                return false;
            }
            if (Max < Current)
            {
                Current = Max;
                return false;
            }
            return true;
        }
        public bool Restore()
        {
            Recalculate();
            Current = Max;
            return true;
        }

        public bool Can_Trigger(Entity source)
        {
            bool returned = false;
            foreach (Trigger trigger in Triggers)
            {
                if (trigger.Can_Trigger(Current, Current / Max, source))
                {
                    returned = true;
                }
            }
            return returned;


        }
        public bool Try_Trigger(Entity source)
        {
            bool returned = false;
            foreach (Trigger trigger in Triggers)
            {
                if(trigger.Try_Trigger(Current, Current / Max, source))
                {
                    returned = true;
                }
            }
            return returned;
        }
        public bool Do_Trigger(Entity source)
        {
            foreach(Trigger iter in Triggers)
            {
                iter.Do_Trigger(source);
            }
            return true;
        }

        public double Get_Max()
        {
            return Max;
        }
        public double Get_Current()
        {
            return Current;
        }
        public List<Trigger> Get_Triggers()
        {
            return Triggers;
        }

        public bool Set_Current(double value)
        {
            
            Current = value;
            if (Current > Max)
            {
                Current = Max;
                return false;
            }
            else if (Current < 0)
            {
                Current = 0;
                return false;
            }
            else return true;
        }
        public bool Set_Triggers(List<Trigger> triggers)
        {
            Triggers = triggers;
            return true;
        }

        public override string ToString()
        {
            return Get_Identifier()+Max + "/" + Current;
        }
        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Current": return Set_Current((double)value);
                case "Triggers":return Set_Triggers((List<Trigger>)value);
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Current": return Get_Flat();
                case "Max":return Get_Max();
                case "Triggers":return Get_Triggers();
                default: return base.Get_Variable(name);
            }
        }
        public override object Call_Function(string name, object[] args)
        {
            switch (name)
            {
                case "Try_Trigger": return Try_Trigger((Entity)args[0]);
                case "Can_Trigger": return Can_Trigger((Entity)args[0]);
                case "Do_Trigger": return Do_Trigger((Entity)args[0]);
                case "Restore": return Restore();
                default: return base.Call_Function(name, args);
            }
        }
    }
} 
