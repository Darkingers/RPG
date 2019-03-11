using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Script:Type
    {
        public Script(Mod _mod = null
           , string _name = ""
           , string _description = ""
           , List<Tag> _tags = null) : base(_mod, _name, _description, _tags)
        {

        }
        public override Type Clone()
        {
            return base.Clone();
        }
        public virtual void Load(string[] args,ref int iter,Mod _mod)
        {


        }
        public override string Save(string tab)
        {

            return "";
        }

        public int Execute(Entity _source,Entity _target)
        {
            ///PLACEHOLDER TEST SCRIPT
            List<Tag> sender =new List<Tag> { Database.Get_Tag("Physical"), Database.Get_Tag("Damage_Done") };
            List<Tag> reciever = new List<Tag> { Database.Get_Tag("Physical"), Database.Get_Tag("Damage_Recieved") };
            List<Tag> affected = new List<Tag> { Database.Get_Tag("Health") };
            External_Effect x = new External_Effect(Target.Current,Intention.Decrease, 250,sender,reciever,affected,false);
            _source.Send(x);
            _target.Recieve(x);
            return 0;
        }
    }
}
