using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Item_Stat
    {
        protected double flat;
        protected double percent;
        protected Stat target;

        public Item_Stat(double _flat=0
            ,double _percent=0
            ,Stat _target=null)
        {
            flat = _flat;
            percent = _percent;
            target = _target;
        }
        
        public Item_Stat Clone()
        {
            return new Item_Stat(flat, percent, target);
        }

        public void Apply(Stat stat)
        {
            if (target.Equals(stat))
            {
                stat.Modify_Flat(flat);
                stat.Modify_Percent(percent);
            }
        }
        public void Remove(Stat stat)
        {
            if (target.Equals(stat))
            {
                stat.Modify_Flat(-flat);
                stat.Modify_Percent(-percent);
            }
        }

        public void Load (string[] args,ref int iter)
        {
            for (string token = args[++iter]; token != "[END]"; token = args[++iter])
            {
                switch (token)
                {
                    case "[Flat]": flat = int.Parse(args[++iter]); break;
                    case "[Percent]": percent = int.Parse(args[++iter]); break;
                    case "[Stat]":target =(Stat) Database.Get(args[++iter]); break;
                }
            }
        }
        public string Write(string tab)
        {
            return tab + "    [Stat] " + target.Get_Identifier() + " [Flat] " + flat + " [Percent] " + percent + " [END]";
        }

        public double Get_Flat()
        {
            return flat;
        }
        public double Get_Percent()
        {
            return percent;
        }
        public string Get_Identifier()
        {
            return target.Get_Identifier();
        }

    }
}
