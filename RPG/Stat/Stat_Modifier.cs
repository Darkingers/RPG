using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Stat_Modifier:Stat
    {
        protected Modifier Intention;//Contains

        public Stat_Modifier()
        {
            Copy(Modifier.Decrease);
        }
        public Stat_Modifier(Stat_Modifier cloned)
        {
            Copy(cloned);
        }
        public Stat_Modifier( Stat stat,Modifier intention) : base(stat)
        {
            Copy(intention);
        }
        public bool Copy(Stat_Modifier copied)
        {
            return base.Copy(copied) && Copy(copied.Get_Intention());
        }
        public bool Copy(Modifier intention)
        {
            return 
            Set_Intention(intention);
        }
        public override object Clone()
        {
            return new Stat_Modifier(this);
        }

        public bool Send(External_Effect effect)
        {
            List<Tag> tags = effect.Get_Sender_Tags();
            if (!Contains_Tags(tags))
            {
                return false;
            }
            else
            {
                effect.Modify_Flat(Flat* (int)Intention);
                effect.Modify_Percent((Percent/100)* (int)Intention);
                return true;
            }
        }
        public bool Recieve(External_Effect effect)
        {
            List<Tag> tags = effect.Get_Reciever_Tags();
            if (!Contains_Tags(tags))
            {
                return false;
            }
            else
            {
                effect.Modify_Flat(Flat*(int)Intention);
                effect.Modify_Percent((Percent/100)* (int)Intention);
                return true;
            }
        }

        public Modifier Get_Intention()
        {
            return Intention;
        }

        public bool Set_Intention(Modifier intention)
        {
            Intention = intention;
            return true;
        }

        public override string ToString(string tab)
        {
            string temp =
                tab+base.ToString(tab) +
                tab+MyParser.Write(Intention, "Modifier", "Intention");
            return temp;
        }
        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Intention": return Set_Intention((Modifier)value);
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Intention": return Get_Intention();
                default: return base.Get_Variable(name);
            }
        }
        public override object Call_Function(string name, object[] args)
        {
            switch (name)
            {
                case "Send":return Send((External_Effect)args[0]);
                case "Recieve":return Recieve((External_Effect)args[0]);
                default: return base.Call_Function(name, args);
            }
        }
    }
}
