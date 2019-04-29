using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Mod:Record
    {
        protected string Path;
        protected List<string> Load_Order;

        public Mod()
        {
            Copy("Invalid", new List<string>());
        }
        public Mod(Record type,string path,List<string> load_order):base(type)
        {
            Copy(path,load_order);
        }
        public Mod(Mod cloned) { 
            Copy(cloned);
        }
        public bool Copy(Mod copied)
        {
            return base.Copy(copied)&&Copy(copied.Get_Path(),copied.Get_Load_Order());
        }
        public bool Copy(string path,List<string> load_order)
        {
            return 
            Set_Path(path)&&
            Set_Load_Order(load_order);
        }
        public override ScriptObject Clone()
        {
            return new Mod(this);
        }
        public override bool Assign(Record copied)
        {
            return Copy((Mod)copied);
        }

        public string Get_Path()
        {
            return Path;
        }
        public List<string> Get_Load_Order()
        {
            return Load_Order;
        }
        public override string Get_Identifier()
        {
            return Name+":"+Name;
        }

        public bool Set_Path(string path)
        {
            Path = path;
            return true;
        }
        public bool Set_Load_Order(List<string> load_order)
        {
            Load_Order = load_order;
            return true;
        }

        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Path": return Set_Path((string)value);
                case "Load_Order":return Set_Load_Order(MyParser.Convert_Array<string>(value));
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Path":return Path;
                case "Load_Order":return Load_Order;
                default: return base.Get_Variable(name);
            }
        }
        public override int Compare(object compared)
        {
            if (Get_Identifier() == ((Mod)compared).Get_Identifier())
            {
                return 0;
            }
            else
            {
                return 1;
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
