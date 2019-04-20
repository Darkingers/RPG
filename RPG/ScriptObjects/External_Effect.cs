using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    enum Number {Flat,Current,Percent}
    enum Modifier {Increase=1,Decrease=-1}
    class External_Effect:ScriptObject
    {
        protected double Flat;
        protected double Percent;
        protected Stat Target;
        protected List<Tag> Sender_Tags;
        protected List<Tag> Reciever_Tags;
        protected Number Target_Number;
        protected Modifier Intention;
        protected Cooldown Duration;

        External_Effect()
        {
            Copy(0, 0, null, new List<Tag>(), new List<Tag>(), Number.Current, Modifier.Decrease,new Cooldown(null,0,0,null));
        }
        External_Effect(ScriptObject scriptobject,double flat,double percent,Stat target,List<Tag> sender,List<Tag> reciever,Number target_number,Modifier intention,Cooldown duration) : base(scriptobject)
        {
            Copy(flat, percent, target, sender, reciever, target_number, intention, duration);
        }
        External_Effect(External_Effect cloned)
        {
            Copy(cloned);
        }
        public bool Copy(External_Effect copied)
        {
            return base.Copy(copied)&& Copy(
                copied.Get_Flat(),
                copied.Get_Percent(), 
                copied.Get_Target(), 
                new List<Tag>(copied.Get_Sender_Tags()),
                new List<Tag>(copied.Get_Reciever_Tags()), 
                copied.Get_Target_Number(),
                copied.Get_Intention(), 
                new Cooldown(copied.Get_Duration()));
        }
        public bool Copy(double flat, double percent, Stat target, List<Tag> sender, List<Tag> reciever, Number target_number, Modifier intention, Cooldown duration)
        {
            return
            Set_Flat(flat) &&
            Set_Percent(percent) &&
            Set_Target(target) &&
            Set_Sender_Tags(sender) &&
            Set_Reciever_Tags(reciever) &&
            Set_Target_Number(target_number) &&
            Set_Intention(intention)&&
            Set_Duration(duration);
        }
        public override object Clone()
        {
            return new External_Effect(this);
        }

        public bool Apply(Stat stat)
        {
            switch (Target_Number)
            {
                case Number.Percent:stat.Modify_Percent(Get_Value());break;
                case Number.Flat: stat.Modify_Flat(Get_Value());break;
                case Number.Current: stat.Modify_Current(Get_Value());break;
            }
            Target = stat;
            if (Duration.Get_Time()>0)
            {
                Duration.Set_EndFunction(Remove);
                Duration.Start();
            }
            return true;
        }  
        public object Remove(object arg=null)
        {
            if (Target == null)
            {
                return false;
            }
            else
            {
                switch (Target_Number)
                {
                    case Number.Percent: Target.Modify_Percent(-1*Get_Value()); break;
                    case Number.Flat: Target.Modify_Flat(-1*Get_Value()); break;
                    case Number.Current:Target.Modify_Current(-1*Get_Value()); break;
                }
                return true;
            }
        }
        public bool Modify_Percent(double value)
        {
            return Set_Percent(Percent + value);
        }
        public bool Modify_Flat(double value)
        {
            return Set_Flat(Flat + value);
        }

        public double Get_Value()
        {
            switch (Intention)
            {
                case Modifier.Decrease:return Flat * Percent * -1;
                case Modifier.Increase:return Flat * Percent;
                default:return 0;
            }
        }
        public List<Tag> Get_Sender_Tags()
        {
            return Sender_Tags;
        }
        public List<Tag> Get_Reciever_Tags()
        {
            return Reciever_Tags;
        }
        public double Get_Flat()
        {
            return Flat;
        }
        public double Get_Percent()
        {
            return Percent;
        }
        public Modifier Get_Intention()
        {
            return Intention;
        }
        public Number Get_Target_Number()
        {
            return Target_Number;
        }
        public Cooldown Get_Duration()
        {
            return Duration;
        }
        public Stat Get_Target()
        {
            return Target;
        }

        public bool Set_Percent(double value)
        {
            Percent = value;
            if (Percent < 0)
            {
                Percent = 0;
                return false;
            }
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
            return true;
        }
        public bool Set_Target(Stat target)
        {
            Target = target;
            return true;
        }
        public bool Set_Sender_Tags(List<Tag> tags)
        {
            Sender_Tags = tags;
            return true;
        }
        public bool Set_Reciever_Tags(List<Tag> tags)
        {
            Reciever_Tags = tags;
            return true;
        }
        public bool Set_Target_Number(Number val)
        {
            Target_Number = val;
            return true;
        }
        public bool Set_Intention(Modifier intention)
        {
            Intention = intention;
            return true;
        }
        public bool Set_Duration(Cooldown value)
        {
            Duration = value;
            return true;
        }

        public override string ToString(string tab)
        {
            string returned =
                base.ToString(tab) +
                tab + MyParser.Write(Flat, "Double", "Flat") +
                tab + MyParser.Write(Percent, "Double", "Percent") +
                tab + MyParser.Write(Target, "Stat", "Target") +
                tab + MyParser.Write(Sender_Tags, "Array<Tag>", "Sender_Tags") +
                tab + MyParser.Write(Reciever_Tags, "Array<Tag>", "Reciever_Tags") +
                tab + MyParser.Write(Target_Number, "Number", "Target_Number") +
                tab + MyParser.Write(Intention, "Modifier", "Intention") +
                tab + MyParser.Write(Duration, "Cooldown", "Duration");
            return returned;
        }
        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Stat": return Set_Target((Stat)value);
                case "Flat": return Set_Flat((double)value);
                case "Percent": return Set_Percent((double)value);
                case "Sender_Tags":return Set_Sender_Tags((List<Tag>)value);
                case "Reciever_Tags": return Set_Reciever_Tags((List<Tag>)value);
                case "Target_Number":return Set_Target_Number((Number)value);
                case "Intention":return Set_Intention((Modifier)value);
                case "Duration":return Set_Duration((Cooldown)value);
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Flat": return Get_Flat();
                case "Percent": return Get_Percent();
                case "Value":return Get_Value();
                case "Target": return Get_Target();
                case "Sender_Tags":return Get_Sender_Tags();
                case "Reciever_Tags":return Get_Reciever_Tags();
                case "Target_Number":return Get_Target_Number();
                case "Intention":return Get_Intention();
                case "Duration":return Get_Duration();
                default: return base.Get_Variable(name);
            }
        }
        public override int Compare(object compared)
        {
            if (((External_Effect)compared).Equals(this))
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
                case "Apply": return Apply((Stat)args[0]);
                case "Remove": return Remove((Stat)args[0]);
                case "Modify_Percent": return Modify_Percent((double)args[0]);
                case "Modify_Flat":return Modify_Flat((double)args[0]);
                default: return base.Call_Function(name, args);
            }
        }
    }
}
