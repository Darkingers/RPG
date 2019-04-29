
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG
{
    class Record:ScriptObject
    {
        protected Mod Source;
        protected string Name;
        protected string Description;
        protected List<Tag> Tags;

        public Record()
        {
            Copy(null, "INVALID", "INVALID", new List<Tag>());
        }
        public Record(ScriptObject scriptobject,Mod source,string name,string description,List<Tag> tags ):base(scriptobject)
        {
            Copy(source, name, description, tags);
        }
        public Record(Record cloned)
        {
            Copy(cloned);
        }
        public bool Copy(Mod source,string name,string description,List<Tag> tags)
        {
            return
            Set_Source(source)&&
            Set_Name(name)&&
            Set_Description(description)&&
            Set_Tags(tags);
        }
        public bool Copy(Record copied)
        {
            return base.Copy(copied)&&Copy(copied.Get_Source(), copied.Get_Name(), copied.Get_Description(), new List<Tag>(copied.Get_Tags()));
        }
        public virtual bool Assign(Record copied)
        {
            return Copy(copied);
        }

        public override ScriptObject Clone()
        {
            return new Record(this);
        }

        public bool Contains_Tag(Tag tag)
        {
            return Tags.Contains(tag);
        }
        public bool Contains_Tags(List<Tag> tags)
        {
            foreach(Tag tag in tags)
            {
                if (!Tags.Contains(tag))
                {
                    return false;
                }
            }
            return true;
        }

        public bool Add_Tag(Tag tag)
        {
            if (Tags.Contains(tag))
            {
                return false;
            }
            else
            {
                Tags.Add(tag);
                return true;
            }
        }
        public bool Add_Tags(List<Tag> tags)
        {
            bool returned = true;
            foreach(Tag tag in tags)
            {
                if (!Add_Tag(tag))
                {
                    returned = false;
                }
            }
            return returned;
        }
        public bool Remove_Tag(Tag tag)
        {
            return Tags.Remove(tag);
        }
        public bool Remove_Tags(List<Tag> tags)
        {
            bool returned= true;
            foreach(Tag tag in tags)
            {
                if (!Remove_Tag(tag))
                {
                    returned = false;
                }
            }
            return returned;
        }

        public bool Equals(Record compared)
        {
            return (Get_Identifier() == compared.Get_Identifier());
        }
        public bool Equals(string identifier)
        {
            return (Get_Identifier() == identifier);
        }

        public Mod Get_Source()
        {
            return Source;
        }
        public string Get_Name()
        {
            return Name;
        }
        public string Get_Description()
        {
            return Description;
        }
        public List<Tag> Get_Tags()
        {
            return Tags;
        }
        public virtual string Get_Identifier()
        {
            return Source.Get_Name() + ":" + Name;
        }

        public bool Set_Source(Mod source)
        {
            Source = source;
            return true;
        }
        public bool Set_Name(string name)
        {
            Name = name;
            return true;
        }
        public bool Set_Description(string description)
        {
            Description =description;
            return true;
        }
        public bool Set_Tags(List<Tag> tags)
        {
            Tags = tags;
            return true;
        }

        public override string ToString()
        {
            return Get_Identifier();
        }
        public override bool Set_Variable(string name,object value)
        {
            switch (name)
            {
                case "Source":return Set_Source((Mod)value);
                case "Name": return Set_Name((string)value);
                case "Description": return Set_Description((string)value);
                case "Tags": return Set_Tags(MyParser.Convert_Array<Tag>(value));
                case "this":return Assign((Record)value);
                default:return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Source":return Get_Source();
                case "Name":return Get_Name();
                case "Description":return Get_Description();
                case "Tags":return Get_Tags();
                default:return base.Get_Variable(name);
            }
        }
        public override int Compare(object compared)
        {
            if ((Get_Identifier() == ((Record)compared).Get_Identifier()))
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
                case "Add_Tag":return Add_Tag((Tag)args[0]);
                case "Add_Tags":return Add_Tags((List<Tag>)args[0]);
                case "Remove_Tag":return Remove_Tag((Tag)args[0]);
                case "Remove_Tags":return Remove_Tags((List<Tag>)args[0]);
                case "Contains_Tag":return Contains_Tag((Tag)args[0]);
                case "Contains_Tags":return Contains_Tags((List<Tag>)args[0]);
                default:return base.Call_Function(name, args);
            }
        }
    }
}
