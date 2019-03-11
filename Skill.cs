using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Skill : Type
    {
        Cost Cost;
        Cooldown Cooldown;
        List<Script> Effects;
        public Skill(
             Mod source = null
            ,string name = ""
            ,string description = ""
            ,List<Tag> tags = null
            ,Cost cost=null
            ,Cooldown cooldown=null
            ,List<Script> effects=null
            ):base(source,name,description,tags)
        {
            Set_Cost(cost);
            Set_Cooldown(cooldown);
            Set_Effects(effects);
        }
        public Skill(Skill cloned):base((Type)cloned)
        {
            Set_Cost(cloned.Get_Cost().Clone());
            Set_Cooldown(cloned.Get_Cooldown().Clone());
            Set_Effects(new List<Script>(cloned.Get_Effects()));
        }
        public override Type Clone()
        {
            return new Skill(Source, Name, Description, new List<Tag>(Tags),Cost.Clone(), Cooldown.Clone(),new List<Script>( Effects));
        }

        public override void Load(string[] args, ref int iter, Mod _mod)
        {
            for(string token = args[++iter]; token != "[END]"; token = args[++iter])
            {
                switch (token)
                {
                    case "[Type]":base.Load(args, ref iter, _mod);break;
                    case "[Cooldown]":Cooldown.Load(args, ref iter);break;
                    case "[Cost]":Cost.Load(args, ref iter);break;
                    case "[Scripts]":for (string token2 = args[++iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            Add_Effect(token2);
                        }  break;
                    default: throw new Exception("[Skill]:Invalid token: " + token);
                }
            }
        }
        public override string Save(string tab)
        {
            string returned =
            tab + "[Skill]" + Environment.NewLine +
            base.Save(tab+"  ") + Environment.NewLine +
            Cost.Save(tab+"  ") + Environment.NewLine +
            Cooldown.Save(tab+"  ") + Environment.NewLine +
            tab + "[Scripts] "+ Environment.NewLine;
            foreach (Script iter in Effects)
            {
                returned +=tab+"    "+ iter.Get_Identifier() +Environment.NewLine;
            }
            returned +=
            tab + "  [END]" + Environment.NewLine +
            tab + "[END]";
            return returned;
        }
        public override string ToString()
        {
            string returned =
            "[Skill]" + Environment.NewLine +
            base.Save("  ") + Environment.NewLine +
            Cost.Save("  ") + Environment.NewLine +
            Cooldown.Save("  ") + Environment.NewLine +
            "[Scripts] ";
            foreach (Script iter in Effects)
            {
                returned +="    " + iter.Get_Identifier() + Environment.NewLine;
            }
            returned +=
            "  [END]" + Environment.NewLine +
            "[END]";
            return returned;
        }
        public string ToLine()
        {
            return Name +"/"+ Cooldown.Get_Time() + "/" + Cooldown.Get_Time_Left();
        }

        public void Add_Effect(Script effect)
        {
            Effects.Add(effect);
        }
        public void Add_Effect(string identifier)
        {
            Add_Effect((Script)Database.Get(identifier));
        }
        public void Remove_Effect(Script effect)
        {
            Effects.Remove(effect);
        }
        public void Remove_Effect(string identifier)
        {
            Remove_Effect((Script)Database.Get(identifier));
        }

        public void Add_Cost(Cost_Value added)
        {
            Cost.Add_Cost(added);
        }
        public void Add_Cost(string identifier, double flat, double percent)
        {
            Cost.Add_Cost(identifier,flat,percent);
        }
        public void Remove_Cost(string identifier)
        {
            Cost.Remove_Cost(identifier);
        }
        public void Remove_Cost(Cost_Value removed)
        {
            Cost.Remove_Cost(removed);
        }

        public bool Can_Use(Entity source, Entity target)
        {
            if (Cost.Can_Pay(source) && Cooldown.Is_Done())
            {
                return true;
            }
            return false;
        }
        public void Use(Entity source, Entity target)
        {

            Cost.Pay(source);
            Cooldown.Start();
            for (int i = 0; i < Effects.Count; i++)
            {
                Effects[i].Execute(source, target);
            }
        }
        public bool Try_Use(Entity source, Entity target)
        {
            if (!Can_Use(source, target))
            {
                return false;
            }
            else
            {
                Use(source, target);
                return true;
            }
        }

        public Cooldown Get_Cooldown()
        {
            return Cooldown;
        }
        public Cost Get_Cost()
        {
            return Cost;
        }
        public List<Script> Get_Effects()
        {
            return Effects;
        }
        public double Get_Time_Left()
        {
            return Cooldown.Get_Time_Left();
        }


        public void Set_Cooldown(Cooldown cooldown)
        {
            if (cooldown != null) Cooldown = cooldown;
            else Cooldown = new Cooldown();
        }
        public void Set_Cost( Cost cost)
        {
            if (cost != null) Cost = cost;
            else Cost = new Cost();
        }
        public void Set_Effects(List<Script> effects)
        {
            if (effects != null) Effects = effects;
            else Effects = new List<Script>();
        }
        public void Set_Time_Left(double left)
        {
            Cooldown.Set_Time_Left(left);
        }
        public void Set_Time(double time)
        {
            Cooldown.Set_Time(time);
        }
    }
}
