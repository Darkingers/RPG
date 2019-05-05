using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Skill : Record
    {
        Cost Cost;
        Cooldown Cooldown;
        List<Script> Effects;
        Bitmap Image;

        public Skill()
        {
            Copy(new Cost(), new Cooldown(), new List<Script>(),null);
        }
        public Skill(Record type,Cost cost,Cooldown cooldown,List<Script> effects,Bitmap image):base(type)
        {
            Copy(cost, cooldown, effects,image);
        }
        public Skill(Skill cloned)
        {
            Copy(cloned);
        }
        public bool Copy(Cost cost,Cooldown cooldown,List<Script> effects,Bitmap image)
        {
            return 
            Set_Cooldown(cooldown) &&
            Set_Cost(cost) &&
            Set_Effects(effects)&&
            Set_Image(image);
        }
        public bool Copy(Skill copied)
        {
           return base.Copy(copied) && Copy(
               new Cost(copied.Get_Cost()), 
               new Cooldown(copied.Get_Cooldown()), 
               new List<Script>(copied.Get_Effects()),
               copied.Get_Image()==null?null:new Bitmap(copied.Get_Image())
               
               );
        }
        public override ScriptObject Clone()
        {
            return new Skill(this);
        }
        public override bool Assign(Record copied)
        {
            return Copy((Skill)copied);
        }

        public bool Can_Use(Entity source, Tile target)
        {
            if (Cost.Can_Pay(source) && Cooldown.Get_Done())
            {
                return true;
            }
            return false;
        }
        public bool Use(Entity source, Tile target)
        {

            Cost.Pay(source);
            Cooldown.Start();
            object[] args = { source, target };
            for (int i = 0; i < Effects.Count; i++)
            {
                Effects[i].Execute(args);
            }
            return true;
        }
        public bool Try_Use(Entity source, Tile target)
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
        public Bitmap Get_Image()
        {
            return Image;
        }
        public string Get_Details()
        {
            string text = Get_Identifier() + Environment.NewLine + 
                Description +Environment.NewLine+"Costs/"+Environment.NewLine+
                Cost.ToString() +"Cooldown/"+Environment.NewLine+ Cooldown.ToString();
            return text.Replace('_',' ');
        }

        public bool Set_Cooldown(Cooldown cooldown)
        {
            Cooldown = cooldown;
            return true;
        }
        public bool Set_Cost( Cost cost)
        {
            Cost = cost;
            return true;
        }
        public bool Set_Effects(List<Script> effects)
        {
            Effects = effects;
            return true;
        }
        public bool Set_Image(Bitmap image)
        {
            Image = image;
            return true;
        }

        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Cost": return Set_Cost(
                    value.GetType()==Cost.GetType() ? (Cost)value:new Cost(new ScriptObject(),Converter.Convert_Array<Cost_Value>(value)));
                case "Cooldown": return Set_Cooldown((Cooldown)value);
                case "Effects": return Set_Effects(Converter.Convert_Array<Script>(value));
                case "Image":return Set_Image((Bitmap)value);
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Cooldown": return Get_Cooldown();
                case "Effects": return Get_Effects();
                case "Cost": return Get_Cost();
                case "Image":return Get_Image();
                default: return base.Get_Variable(name);
            }
        }
        public override object Call_Function(string name, object[] args)
        {
            switch (name)
            {
                case "Can_Use": return Can_Use((Entity)args[0], (Tile)args[1]);
                case "Try_Use": return Try_Use((Entity)args[0], (Tile)args[1]);
                case "Use": return Use((Entity)args[0], (Tile)args[1]);
                default: return base.Call_Function(name, args);
            }
        }

    }
}
