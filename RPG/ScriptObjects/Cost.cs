using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Cost:ScriptObject
    {
        

        protected List<Cost_Value> Costs;

        public Cost()
        {
            Copy(new List<Cost_Value>());
        }
        public Cost(ScriptObject scriptobject,List<Cost_Value> costs):base(scriptobject)
        {
            Copy(costs);
        }
        public Cost(Cost cloned)
        {
            Copy(cloned);
        }
        public bool Copy(List<Cost_Value> cost)
        {
             return Set_Costs(cost);
        }
        public bool Copy(Cost copied)
        {
           return base.Copy(copied)&& Copy(new List<Cost_Value>(copied.Get_Costs()));
        }
        public override object Clone()
        {
            return new Cost(this);
        }

        public bool Can_Pay(Entity source)
        {
            List<Stat_Resource> _resources = source.Get_Resources();
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
        public bool Pay(Entity source)
        {
            List<Stat_Resource> _resources = source.Get_Resources();
            foreach (Cost_Value cost in Costs)
            {
               foreach(Stat_Resource resource in _resources)
                {
                    if (cost.Equals(resource))
                    {
                        cost.Pay(resource);
                        break;
                    }
                }
            }
            return true;
        }
        public bool Try_Pay(Entity source)
        {

            if (!Can_Pay(source))
            {
                return false;
            }
            else
            {
                Pay(source);
                return true;
            }
        }

        public bool Add_Cost(Cost_Value added)
        {
            if (Costs.Contains(added))
            {
                return false;
            }
            else
            {
                Costs.Add(added);
                return true;
            }
            
        }
        public bool Remove_Cost( Cost_Value removed)
        {
            return Costs.Remove(removed);
        }

        public List<Cost_Value> Get_Costs()
        {
            return Costs;
        }
        public Cost_Value Get_Cost(string identifier)
        {
            foreach (Cost_Value cost in Costs)
            {
                if (cost.Get_Target().Get_Identifier() == identifier)
                {
                    return cost;
                }
            }
            return null;

        }

        public bool Set_Costs(List<Cost_Value> costs)
        {
            Costs = costs;
            return true;
        }
        public bool Set_Cost(string identifier, Cost_Value value)
        {
           for(int i = 0; i < Costs.Count; i++)
           {
                if (Costs[i].Get_Target().Get_Identifier() == identifier)
                {
                    Costs[i] = value;
                    return true;
                }
           }
            return false;
        }

        public override string ToString(string tab)
        {
            string returned=
                base.ToString(tab)+
                tab+ MyParser.Write(Costs, "Array<Cost_Value>", "Costs");
            return returned;
        }
        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Costs":return Set_Costs((List<Cost_Value>)value);
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Costs":return Get_Costs();
                default:return base.Get_Variable(name);
            }
        }
        public override int Compare(object compared)
        {
            if (((Cost)compared).Equals(this))
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
                case "Can_Pay": return Can_Pay((Entity)args[0]);
                case "Pay":return Pay((Entity)args[0]);
                case "Try_Pay": return Try_Pay((Entity)args[0]);
                case "Add_Cost":return Add_Cost((Cost_Value)args[0]);
                case "Remove_Cost":return Remove_Cost((Cost_Value)args[0]);
                default: return base.Call_Function(name, args);
            }
        }
    }
}

