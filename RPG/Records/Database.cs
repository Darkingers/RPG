using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RPG
{
    class Database
    {
        static protected Dictionary<string, Mod> mods = new Dictionary<string, Mod>();
        static protected Dictionary<string, Record> records = new Dictionary<string, Record>();
        static protected Dictionary<string, Tag> tags = new Dictionary<string, Tag>();
        static protected Dictionary<string, Bitmap> images = new Dictionary<string, Bitmap>();
        static protected Dictionary<string, Slot> slots = new Dictionary<string, Slot>();

        static public string Get_Image_Name(Bitmap img)
        {
            List<string> keys = images.Keys.ToList();
            List<Bitmap> values = images.Values.ToList();
            for (int i = 0; i < images.Keys.Count; i++)
            {
                if (values[i] == img)
                {
                    return keys[i];
                }
            }
            return "";
        }
        static public Bitmap Get_Image(string _name)
        {
            return images[_name];
        }
        static public Tag Get_Tag(string _name)
        {
            return tags[_name];
        }
        static public Slot Get_Slot(string _name)
        {
            return slots[_name];
        }
        static public Record Get(string name)
        {
            return records[name];
        }
        static public Mod Get_Mod(string name)
        {
            return mods[name];
        }

        static public void Load()
        {
            DirectoryInfo di = new DirectoryInfo("Mods");
            DirectoryInfo[] folders = di.GetDirectories();
            foreach (DirectoryInfo mod in folders)
            {
                Mod loaded = new Mod();
                loaded.Set_Path(mod.FullName);
                loaded.Set_Source(loaded);

                string text=System.IO.File.ReadAllText(mod.FullName + "\\Mod.txt");
                List<string[]> temp = MyParser.Parse_Text(text);
                loaded.Read(temp[0][1]);
                records.Add(loaded.Get_Name(), loaded);

                Load_Mod(loaded);
            }
        }
        
        static public void Load_File(string path,Mod source)
        {
            string text = System.IO.File.ReadAllText(path);
            List<string[]> temp = MyParser.Parse_Text(text);
            foreach(string[] iter in temp)
            {
                switch (iter[0])
                {
                    case "Object":Load_Object(iter[1], iter[2]);break;
                    case "Script":Load_Script(iter[1], iter[2], iter[3],iter[4]);break;
                    default:throw new Exception("Database:Load_File:Invalid denominator: " + iter[0]);
                }
            }
        }
        static public void Load_Object(string value,string type)
        {
            Record added = (Record)MyParser.Interpret(type, value);
            records.Add(added.Get_Identifier(),added);
        }
        static public void Load_Script(string value,string return_type,string name,string args)
        {
            Script added = new Script();
            string[] temp = args.Split(',');
            List<string> arguments = new List<string>();
            List<string> types = new List<string>();
            foreach(string iter in temp)
            {
                string[] temp2 = iter.Split(' ');
                types.Add(temp2[0]);
                arguments.Add(temp2[1]);
            }
            added.Read(value);
            records.Add(added.Get_Identifier(), added);
        }
        static public void Load_Mod(Mod loaded)
        {
            Load_Images(loaded);
            List<string> folders = loaded.Get_Load_Order();
            foreach (string folder in folders)
            {
                Load_Folders(loaded.Get_Path()+"//"+folder,loaded);
            }
        }
        static public void Load_Images(Mod source)
        {
            DirectoryInfo di = new DirectoryInfo(source.Get_Path()+"//Images");
            FileInfo[] files = di.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                Bitmap added = (Bitmap)Bitmap.FromFile(file.FullName);
                images.Add(source.Get_Name() + ":" + file.Name, added);
            }


        }
        static public void Load_Folders(string path,Mod source)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            DirectoryInfo[] folders = di.GetDirectories();
            foreach(DirectoryInfo iter in folders)
            {
                Load_Folders(iter.FullName,source);
            }

            FileInfo[] files = di.GetFiles("*.txt");
            foreach (FileInfo file in files)
            {
                Load_File(file.FullName,source);
            }

        }
    }
}
