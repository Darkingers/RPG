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
        static protected Dictionary<string, Type> types = new Dictionary<string, Type>();
        static protected Dictionary<string, Tag> tags = new Dictionary<string, Tag>();
        static protected Dictionary<string, Bitmap> images = new Dictionary<string, Bitmap>();
        static protected Dictionary<string, Slot> slots = new Dictionary<string, Slot>();

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
        static public Type Get(string _name)
        {
            return types[_name];
        }

        static protected string[] Parse(string text)
        {
            return text.Split(new string[] { " ", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        static public void Load()
        {
            DirectoryInfo di = new DirectoryInfo("Mods");
            DirectoryInfo[] folders = di.GetDirectories();
            foreach (DirectoryInfo mod in folders)
            {
                Mod loaded = new Mod(mod.Name,mod.FullName);
                Load_Tags(loaded);
                Load_Stats(loaded);
                Load_Slots(loaded);
                Load_Images(loaded);
                Script test = new Script(loaded, "TestScript", "Test script", null);
                types.Add(test.Get_Identifier(), test);
                Load_Skills(loaded);
                Load_Items(loaded);
                Load_Entities(loaded);
                Load_Tiles(loaded);
                Load_Maps(loaded);
            }
        }
        static protected void Load_Tiles(Mod mod)
        {
            DirectoryInfo di = new DirectoryInfo(mod.Get_Path()+"\\Tiles");
            FileInfo[] files = di.GetFiles("*.txt");
            foreach (FileInfo file in files)
            {
                string text = System.IO.File.ReadAllText(file.FullName);
                Tile loaded = new Tile(mod);
                int iter = 0;
                loaded.Load(Parse(text), ref iter,mod);
                System.Console.WriteLine(loaded.Save("   "));
                types.Add(loaded.Get_Identifier(), loaded);
            }
        }
        static protected void Load_Maps(Mod mod)
        {
            DirectoryInfo di = new DirectoryInfo(mod.Get_Path() + "\\Maps");
            FileInfo[] files = di.GetFiles("*.txt");
            foreach (FileInfo file in files)
            {
                string text = System.IO.File.ReadAllText(file.FullName);
                Map loaded = new Map(mod);
                int iter = 0;
                loaded.Load(Parse(text), ref iter, mod);
                types.Add(loaded.Get_Identifier(), loaded);
            }
        }
        static protected void Load_Images(Mod mod)
        {
            DirectoryInfo di = new DirectoryInfo(mod.Get_Path()+"\\Images");
            FileInfo[] files = di.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                Bitmap added = (Bitmap)Bitmap.FromFile(file.FullName);

                images.Add(mod.Get_Name() + ":" + file.Name, added);
            }
        }
        static protected void Load_Tags(Mod mod)
        {
            string[] parsed=Parse(System.IO.File.ReadAllText(mod.Get_Path()+"\\Tags.txt"));
            foreach(string tag in parsed)
            {
                tags.Add(tag, new Tag(tag));
            }


        }
        static protected void Load_Slots(Mod mod)
        {
            string[] parsed = Parse(System.IO.File.ReadAllText(mod.Get_Path() + "\\Tags.txt"));
            foreach (string slot in parsed)
            {
                slots.Add(slot, new Slot(slot));
            }


        }
        static protected void Load_Skills(Mod mod)
        {
            DirectoryInfo di = new DirectoryInfo(mod.Get_Path() + "\\Skills");
            FileInfo[] files = di.GetFiles("*.txt");
            foreach (FileInfo file in files)
            {
                string text = System.IO.File.ReadAllText(file.FullName);
                Skill loaded = new Skill(mod);
                int iter = 0;
                loaded.Load(Parse(text), ref iter, mod);
                System.Console.WriteLine(loaded.Save("   "));
                types.Add(loaded.Get_Identifier(), loaded);
            }



        }
        static protected void Load_Items(Mod mod)
        {
            DirectoryInfo di = new DirectoryInfo(mod.Get_Path() + "\\Items");
            FileInfo[] files = di.GetFiles("*.txt");
            foreach (FileInfo file in files)
            {
                string text = System.IO.File.ReadAllText(file.FullName);
                Item loaded = new Item(mod);
                int iter = 1;
                loaded.Load(Parse(text), ref iter, mod);
                System.Console.WriteLine(loaded.Save("   "));
                types.Add(loaded.Get_Identifier(), loaded);
            }
        }
        static protected void Load_Stats(Mod mod)
        {
            DirectoryInfo di = new DirectoryInfo(mod.Get_Path() + "\\Stats");
            FileInfo[] files = di.GetFiles("*.txt");
            foreach (FileInfo file in files)
            {
                string text = System.IO.File.ReadAllText(file.FullName);
                string[] parsed = Parse(text);
                Stat loaded=null;
                switch (parsed[0])
                {
                    case "[Stat_Resource]":loaded = new Stat_Resource(mod); break;
                    case "[Stat_Modifier]": loaded = new Stat_Modifier(mod); break;
                    case "[Stat_Regeneration]": loaded = new Stat_Regeneration(mod); break;
                    default: System.Console.Write("Invalid token:" + parsed[0]); break;
                }
                int iter = 0;
                loaded.Load(Parse(text), ref iter, mod);
                System.Console.WriteLine(loaded.Save("   "));
                types.Add(loaded.Get_Identifier(), loaded);
            }


        }
        static protected void Load_Entities(Mod mod)
        {
            DirectoryInfo di = new DirectoryInfo(mod.Get_Path() + "\\Entities");
            FileInfo[] files = di.GetFiles("*.txt");
            foreach (FileInfo file in files)
            {
                string text = System.IO.File.ReadAllText(file.FullName);
                string[] parsed = Parse(text);
                Entity loaded = null;
                switch (parsed[0])
                {
                    case "[Player]":loaded = new Player(mod);break;
                }
                int iter = 1;
                loaded.Load(Parse(text), ref iter, mod);
                types.Add(loaded.Get_Identifier(), loaded);
            }



        }
    }
}
