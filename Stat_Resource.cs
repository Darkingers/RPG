using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Stat_Resource:Stat
    {
        protected double Default;
        protected double Max;
        protected double Current;

        protected List<Trigger> Triggers;

        public Stat_Resource(
            Mod source = null
           ,string name = ""
           ,string description = ""
           ,List<Tag> tags = null
           ,double flat = 0
           ,double percent = 0
           ,double @default=0
           ,double current=0
           ,List<Trigger> triggers=null
            ) : base(source, name, description, tags,flat,percent)
        {
            Set_Default(@default);
            Set_Current(current);
        }

        public override Type Clone()
        {
            if(Triggers!=null)
            return new Stat_Resource(Source, Name, Description, Tags,
                Flat, Percent,Default,Current, new List<Trigger>(Triggers));
            else
            return new Stat_Resource(Source, Name, Description, Tags,
                Flat, Percent, Default, Current,null);
        }

        public override void Load(string[] args, ref int iter, Mod _mod)
        {
            for (string token = args[++iter]; token != "[END]"; token = args[++iter])
            {
                switch (token)
                {
                    case "[Stat]": base.Load(args, ref iter, _mod); break;
                    case "[Default]":Set_Default( int.Parse(args[++iter])); break;
                    case "[Current]":Set_Current(int.Parse(args[++iter])); break;
                    default: throw new Exception("[Stat_Resource]:Invalid token: " + token);
                }
            }
        }
        public override string Save(string tab)
        {
            string temp =
            tab + "[Stat_Resource]" + Environment.NewLine +
            base.Save(tab + "  ") + Environment.NewLine +
            tab + "  [Default] " + Default + Environment.NewLine +
            tab + "  [Max] " + Max + Environment.NewLine +
            tab + "  [Current] " + Current + Environment.NewLine +
            tab + "[END]";
            return temp;
        }
        public override string ToString()
        {
            string temp =
                "[Stat_Resource]" + Environment.NewLine +
                base.Save("  ") + Environment.NewLine +
                "  [Default] " + Default + Environment.NewLine +
                "  [Max] " + Max + Environment.NewLine +
                "  [Current] " + Current + Environment.NewLine +
               "[END]";
            return temp;
        }
        public override string ToLine()
        {
            return Name + "/" + Flat + "/" + Percent+"/"+Max+"/"+Current;
        }

        public override void Modify_Current(double _value)
        {
            Current += _value;
            if (Current > Max)
            {
                Current = Max;
            }
            if (Current < 0)
            {
                Current = 0;
            }
            Recalculate();
        }
        public override void Recalculate()
        {
            Max = Flat * ((double)Percent/100+1);
            if (Max < 0)
            {
                Max = 0;
            }
            if (Max < Current)
            {
                Current = Max;
            }
        }
        public void Restore()
        {
            Recalculate();
            Current = Max;
        }

        public void Try_Trigger(Entity _source)
        {
            foreach (Trigger trigger in Triggers)
            {
                trigger.Try_Trigger(Current, Current / Max, _source);

            }
        }

        public double Get_Default()
        {
            return Default;
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

        public void Set_Default(double value)
        {
            Recalculate();
            if (value > Max) value = Max;
            if (value < 0) value = 0;
            Default = value;
        } 
        public void Set_Current(double value)
        {
            Recalculate();
            if (value > Max) value = Max;
            if (value < 0) value = 0;
            Current = value;
        }
        public void Set_Triggers(List<Trigger> triggers)
        {
            if (triggers != null) Triggers = triggers;
        }
    }
}
