using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Stat:Type
    {
        
        protected double Flat;
        protected double Percent;

        public Stat(
            Mod source = null
           ,string name = ""
           ,string description = ""
           ,List<Tag> tags = null
           ,double flat = 0
           ,double percent=0
            ):base(source,name,description,tags)
        {
            Set_Flat(flat);
            Set_Percent(percent);
        }
        public Stat(Stat cloned):base((Type)cloned)
        {
            Set_Flat(cloned.Get_Flat());
            Set_Percent(cloned.Get_Percent());
        }
        public override Type Clone()
        {
            return new Stat(Source, Name, Description,Tags, Flat, Percent);
        }

        public override void Load(string[] args,ref int iter,Mod _mod)
        {
            for(string token = args[++iter]; token != "[END]"; token = args[++iter])
            {
                switch (token)
                {
                    case "[Type]":base.Load(args,ref iter, _mod); break;
                    case "[Flat]":Flat = int.Parse(args[++iter]); break;
                    case "[Percent]":Percent = int.Parse(args[++iter]); break;
                    default: throw new Exception("[Stat]:Invalid token: " + token);
                }
            }
        }
        public override string Save(string tab)
        {
            string temp =
            tab + "[Stat]" + Environment.NewLine +
            base.Save(tab +"  ") + Environment.NewLine +
            tab + "  [Flat] " + Flat + Environment.NewLine +
            tab + "  [Percent] " + Percent + Environment.NewLine +
            tab + "[END]";
            return temp;
        }
        public override string ToString()
        {
            string temp =
            "[Stat]" + Environment.NewLine +
            base.ToString()+ Environment.NewLine +
            "  [Flat] " + Flat + Environment.NewLine +
            "  [Percent] " + Percent + Environment.NewLine +
            "[END]";
            return temp;
        }
        public virtual string ToLine()
        {
            return Name + "/" + Flat + "/" + Percent;
        }

        public virtual void Modify_Current(double value)
        {
            return;
        }
        public void Modify_Flat(double value)
        {
            Flat += value;
            Recalculate();
        }
        public void Modify_Percent(double value)
        {
            Percent += value;
            Recalculate();
        }
        public virtual void Recalculate()
        {
            ;
        }

        public double Get_Flat()
        {
            return Flat;
        }
        public double Get_Percent()
        {
            return Percent;
        }

        public void Set_Flat(double value)
        {
            Flat = value;
            Recalculate();
        }
        public void Set_Percent(double value)
        {
            Percent = value;
            Recalculate();
        }


    }
}
