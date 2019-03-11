using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG
{
    class Type
    {
        protected Mod Source;
        protected string Name;
        protected string Description;
        protected List<Tag> Tags;
        
        public Type(
            Mod source=null
           ,string name=""
           ,string description=""
           ,List<Tag> tags = null
            )
        {
            Set_Source(source);
            Set_Name(name);
            Set_Description(description);
            Set_Tags(tags);
        }
        public Type(Type cloned)
        {
            Set_Source(cloned.Get_Source());
            Set_Name(cloned.Get_Name());
            Set_Description(cloned.Get_Description());
            Set_Tags(cloned.Get_Tags());
        }

        public virtual  Type Clone()
        {
            return new Type(Source,Name, Description,Tags);
        }

        public virtual void Load(string[] args,ref int iter,Mod source)
        {
            Set_Source(source);
            for(string token = args[++iter]; token != "[END]"; token = args[++iter])
            {
                switch (token)
                {
                    case "[Name]":Set_Name(args[++iter]);break;
                    case "[Description]":string description = ""; for (string token2 = args[++iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            description +=token2+ " ";
                        }Set_Description(description); break;
                    case "[Tags]":for (string token2 = args[++iter]; token2 != "[END]"; token2 = args[++iter])
                        {
                            Add_Tag(token2);
                        }break;
                    default: throw new Exception("[Type]:Invalid token: " + token);
                }
            }
        }
        public virtual string Save(string tab)
        {
            string returned =
            tab + "[Type]" + Environment.NewLine +
            tab + "  [Name] " + Name + Environment.NewLine +
            tab + "  [Description] " + Description + " [END]" + Environment.NewLine+
            tab + "  [Tags] ";
            foreach(Tag tag in Tags)
            {
                returned += tag.Get_Name() + " ";
            }
            returned += "[END]" + Environment.NewLine +
            tab + "[END]";
            return returned;
            
        }
        public override string ToString()
        {
            string returned =
            "[Type]" + Environment.NewLine +
            "  [Name] " + Name + Environment.NewLine +
            "  [Description] " + Description + " [END]" + Environment.NewLine +
            "  [Tags] ";
            foreach (Tag tag in Tags)
            {
                returned += tag.Get_Name() + " ";
            }
            returned += "[END]" + Environment.NewLine +
            "[END]";
            return returned;
        }

        public virtual bool Contains_Tag(Tag tag)
        {
            return Tags.Contains(tag);
        }
        public virtual bool Contains_Tags(List<Tag> tags)
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

        public virtual void Add_Tag(Tag tag)
        {
            Tags.Add(tag);
        }
        public virtual void Add_Tag(string identifier)
        {
            Tags.Add(Database.Get_Tag(identifier));
        }
        public virtual void Remove_Tag(Tag tag)
        {
            Tags.Remove(tag);
        }
        public virtual void Remove_Tag(string identifier)
        {
            Tags.Remove(Database.Get_Tag(identifier));
        }
        
        public virtual bool Equals(Type compared)
        {
            return (Name == compared.Get_Name());
        }
        public virtual bool Equals(string identifier)
        {
            return (Source.Get_Name() + ":" + Name == identifier);
        }

        public virtual Mod Get_Source()
        {
            return Source;
        }
        public virtual string Get_Name()
        {
            return Name;
        }
        public virtual string Get_Description()
        {
            return Description;
        }
        public virtual List<Tag> Get_Tags()
        {
            return Tags;
        }
        public virtual string Get_Identifier()
        {
            return Source.Get_Name() + ":" + Name;
        }
        public virtual string Get_Details()
        {
            string returned =
                "Mod: " + Source.Get_Name() + Environment.NewLine+
                "Name: " + Name + Environment.NewLine +
                "Description: " + Description + Environment.NewLine +
                "Tags: ";
            foreach(Tag tag in Tags)
            {
                returned += tag.Get_Name()+" ";
            }
            return returned;
        }

        public virtual void Set_Source(Mod source)
        {
            if(source!=null)Source = source;
        }
        public virtual void Set_Name(string name)
        {
            Name = name;
        }
        public virtual void Set_Description(string description)
        {
            Description = description;
        }
        public virtual void Set_Tags(List<Tag> tags)
        {
            if (tags != null) Tags = tags;
            else Tags = new List<Tag>();
        }
    }
}
