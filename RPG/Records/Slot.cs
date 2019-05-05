using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Slot:Record
    {

        public Slot():base()
        {

        }
        public Slot(Record type):base(type)
        {

        }
        public override ScriptObject Clone()
        {
            return new Slot(this);
        }
        public override bool Assign(Record copied)
        {
            return Copy((Slot)copied);
        }

        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
   
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
              
                default: return base.Get_Variable(name);
            }
        }
        
        public override object Call_Function(string name, object[] args)
        {
            switch (name)
            {

                default: return base.Call_Function(name, args);
            }
        }
       
    }
}
