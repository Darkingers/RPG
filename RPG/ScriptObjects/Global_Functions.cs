using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Global_Functions
    {
        static public object Call_Function(string name,object[] args)
        {
            switch (name)
            {


                default:return ((Script)Database.Get(name)).Execute(args);break;
            }
        }
    }
}
