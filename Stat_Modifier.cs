using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Stat_Modifier:Stat
    {
        protected bool Change_Sign;
        public Stat_Modifier(
            Mod source = null
           ,string name = ""
           ,string description = ""
           ,List<Tag> tags = null
           ,double flat = 0
           ,double percent = 0
           ,bool change_sign=false
            ) : base(source, name, description, tags,flat,percent)
        {
            Set_Change_Sign(change_sign);
        }

        public override Type Clone()
        {
            return new Stat_Modifier(Source, Name, Description, Tags,
                Flat, Percent);
        }

        public override void Load(string[] args, ref int iter, Mod _mod)
        {
            for (string token = args[++iter]; token != "[END]"; token = args[++iter])
            {
                switch (token)
                {
                    case "[Stat]": base.Load(args, ref iter, _mod); break;
                    case "[Can_Change_Sign]": Set_Change_Sign(bool.Parse(args[++iter])); break;
                    default: throw new Exception("[Stat_Modifier]:Invalid token: " + token);
                }
            }
        }
        public override string Save(string tab)
        {
            string temp =
                tab + "[Stat_Modifier]" + Environment.NewLine +
                base.Save(tab + "  ") + Environment.NewLine +
                tab + "  [Can_Change_Sign] "+Change_Sign+Environment.NewLine+ 
                tab + "[END]";
            return temp;
        }
        public override string ToString()
        {
            string temp =
                "[Stat_Modifier]" + Environment.NewLine +
                base.Save("  ") + Environment.NewLine +
                "  [Can_Change_Sign] " + Change_Sign + Environment.NewLine +
                "[END]";
            return temp;
        }
        public override string ToLine()
        {
            return Name + "/" + Flat + "/" + Percent;
        }

        public void Send(External_Effect effect)
        {
            List<Tag> _tags = effect.Get_Sender_Tags();
            if (!Contains_Tags(_tags))
            {
                return;
            }
            else
            {
                effect.Modify_Value(Flat);
                effect.Modify_Percent(1 + (Percent/100));
                if(Change_Sign)effect.Sign_Changeable();
            }
        }
        public void Recieve(External_Effect effect)
        {
            List<Tag> _tags = effect.Get_Reciever_Tags();
            if (!Contains_Tags(_tags))
            {
                return;
            }
            else
            {
                effect.Modify_Value(Flat);
                effect.Modify_Percent((Percent/100)+1);
            }
        }

        public bool Get_Change_Sign()
        {
            return Change_Sign;
        }

        public void Set_Change_Sign(bool sign)
        {
            Change_Sign = sign;
        }
    }
}
