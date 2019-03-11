using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Tag
    {
        //TODO
        protected string Name;
        
        public Tag(string name = "")
        {
            Set_Name(name);
        }

        public string Get_Name()
        {
            return Name;
        }

        public void Set_Name(string name)
        {
            Name = name;
        }
    }
}
