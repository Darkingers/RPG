using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Mod
    {
        protected string name;
        protected string path;
        public Mod(string _name = "",string _path="")
        {
            name = _name;
            path = _path;
        }
        public string Get_Name()
        {
            return name;
        }
        public string Get_Path()
        {
            return path;
        }
    }
}
