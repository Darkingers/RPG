using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Cost
    {
        

        protected List<Cost_Value> Costs;

        public Cost(List<Cost_Value> costs=null)
        {
            Set_Costs(costs);
        }
        public Cost(Cost cloned)
        {
            Set_Costs(new List<Cost_Value>(cloned.Get_Costs()));
        }

        public Cost Clone()
        {
            return new Cost(new List<Cost_Value>(Costs));
        }

        public void Load(string[] args,ref int iter)
        {
            for (string token = args[++iter]; token != "[END]"; token = args[++iter])
            {
                switch (token)
                {
                    case "[Stat_Cost]":Load_Cost(args, ref iter); break;
                    default: throw new Exception("[Cost]:Invalid token: " + token);
                }
            }
        }
        public string Save(string tab)
        {
            string returned =
            tab+"[Cost]" + Environment.NewLine;
            foreach (Cost_Value cost in Costs)
            {
                returned +=tab+"  "+cost.ToString() + Environment.NewLine;
            }
            returned +=
            tab+"[END]";
            return returned;


        }
        public override string ToString()
        {
            string returned = "[Cost]" + Environment.NewLine;
            foreach (Cost_Value cost in Costs)
            {
                returned += cost.ToString() + Environment.NewLine;
            }
            returned += "[END]";
            return returned;
        }

        public bool Can_Pay(Entity _source)
        {
            List<Stat_Resource> _resources = _source.Get_Resources();
            foreach(Cost_Value cost in Costs)
            {
                foreach(Stat_Resource resource in _resources)
                {
                    if (cost.Equals(resource) && cost.Can_Pay(resource))
                    {
                        break;
                    }
                    if (cost.Equals(resource) && !cost.Can_Pay(resource))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void Pay(Entity _source)
        {
            List<Stat_Resource> _resources = _source.Get_Resources();
            foreach (Cost_Value cost in Costs)
            {
               foreach(Stat_Resource resource in _resources)
                {
                    if (cost.Equals(resource))
                    {
                        cost.Pay_Cost(resource);
                        break;
                    }
                }
            }
        }
        public bool Try_Pay(Entity _source)
        {

            if (!Can_Pay(_source))
            {
                return false;
            }
            else
            {
                Pay(_source);
                return true;
            }
        }

        public void Add_Cost(Cost_Value added)
        {
            Costs.Add(added);
        }
        public void Add_Cost(string identifier,double flat,double percent)
        {
            Cost_Value added = new Cost_Value();
            added.Set_Stat(identifier);
            added.Set_Flat_Cost(flat);
            added.Set_Percent_Cost(percent);
            Costs.Add(added);
        }
        public void Load_Cost(string[] args,ref int iter)
        {
            Cost_Value added = new Cost_Value();
            added.Load(args, ref iter);
            Costs.Add(added);
        }
        public void Remove_Cost(string identifier)
        {
            Costs.Remove(Costs.Find((Cost_Value v) => { return v.Equals(identifier); }));
        }
        public void Remove_Cost( Cost_Value removed)
        {
            Costs.Remove(removed);
        }

        public List<Cost_Value> Get_Costs()
        {
            return Costs;
        }

        public void Set_Costs(List<Cost_Value> costs)
        {
            if (costs != null) Costs = costs;
            else Costs = new List<Cost_Value>();

        }
    }
}

