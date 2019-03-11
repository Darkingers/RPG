using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Cost_Value
    {
        protected Stat Stat;
        protected double Flat_Cost;
        protected double Percent_Cost;

        public Cost_Value(string identifier
                         , double flat_cost = 0
                         , double percent_cost = 0)
        {
            Set_Stat(identifier);
            Set_Flat_Cost(flat_cost);
            Set_Percent_Cost(percent_cost);
        }
        public Cost_Value(Stat stat = null
                         , double flat_cost = 0
                         , double percent_cost = 0)
        {
            Set_Stat(stat);
            Set_Flat_Cost(flat_cost);
            Set_Percent_Cost(percent_cost);
        }
        public Cost_Value(Cost_Value cloned)
        {
            Set_Stat(cloned.Get_Stat());
            Set_Flat_Cost(cloned.Get_Flat_Cost());
            Set_Percent_Cost(cloned.Get_Percent_Cost());
        }
        public Cost_Value Clone()
        {
            return new Cost_Value(Stat, Flat_Cost, Percent_Cost);
        }

        public void Load(string[] args, ref int iter)
        {
            for (string token = args[++iter]; token != "[END]"; token = args[++iter])
            {
                switch (token)
                {
                    case "[Stat]": Set_Stat(args[++iter]); break;
                    case "[Flat]": Set_Flat_Cost(double.Parse(args[++iter])); break;
                    case "[Percent]": Set_Percent_Cost(double.Parse(args[++iter])); break;
                    default: throw new Exception("[Cost_Value]:Invalid token: " + token);
                }
            }
        }
        public string Save(string tab)
        {
            return tab + ToString();
        }
        public override string ToString()
        {
            return "[Stat_Cost]" + " [Stat] " + Stat.Get_Identifier() + " [Flat] " + Flat_Cost + " [Percent] " + Percent_Cost + " [END";
        }

        public bool Equals(string identifier)
        {
            return Stat.Equals(identifier);
        }
        public bool Equals(Stat_Resource resource)
        {
            return Stat.Equals(resource);
        }
        public bool Can_Pay(Stat_Resource resource)
        {
            double max = resource.Get_Max();
            if (resource.Get_Current() >= max * Percent_Cost + Flat_Cost)
            {
                return true;
            }
            else return false;
        }
        public void Pay_Cost(Stat_Resource resource)
        {
            double max = resource.Get_Max();
            resource.Modify_Current(-Flat_Cost + -Percent_Cost * max);
        }

        public Stat Get_Stat()
        {
            return Stat;
        }
        public double Get_Flat_Cost()
        {
            return Flat_Cost;
        }
        public double Get_Percent_Cost()
        {
            return Percent_Cost;
        }

        public void Set_Stat(string identifier)
        {
            if (identifier != "") Stat = (Stat)Database.Get(identifier);
        }
        public void Set_Stat(Stat stat)
        {
            if (stat != null) Stat = stat;
        }
        public void Set_Flat_Cost(double flat)
        {
            Flat_Cost = flat;
        }
        public void Set_Percent_Cost(double percent)
        {
            Percent_Cost = percent;
        }
    }
}
