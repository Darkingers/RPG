using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Stat_Experimental:ScriptObject
    {

        /// <summary>
        /// This would be a fully scriptable stat.
        /// </summary>
        protected List<object> Binded;
        protected double Value;

        protected List<Script> On_Tick;
        protected List<Script> On_Effect;
        protected List<Script> On_Modify;

        public void Tick()
        {
            object[] args = { Value,Binded };
            foreach(Script iter in On_Tick)
            {
                iter.Execute(args);
            }
        }
        public void Process_Effect(Effect effect)
        {
            object[] args = {Value,effect, Binded };
            foreach (Script iter in On_Effect)
            {
                iter.Execute(args);
            }
        }
        public  void Modify(double value)
        {
            object[] args = { Value, value, Binded };
            foreach (Script iter in On_Modify)
            {
                iter.Execute(args);
            }
        }
    }
}
