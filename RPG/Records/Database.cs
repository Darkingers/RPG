using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace RPG
{
    class Database
    {
        static protected Dictionary<string, object> records = new Dictionary<string,object>();

        static public bool Add_Record(Record added)
        {
            records.Add(added.Get_Identifier(), added);
            return true;
        }
        static public object Get(string name)
        {
                return records[name];
        }

        static public void Load()
        {
            DirectoryInfo di = new DirectoryInfo("Mods");
            DirectoryInfo[] folders = di.GetDirectories();
            foreach (DirectoryInfo mod in folders)
            {
                Load_File(mod.FullName + "\\Mod.txt");
                Mod loaded = (Mod)Get(mod.Name+":"+mod.Name);
                loaded.Set_Source(loaded);
                ScriptVisitor.Set_Source(loaded);
                loaded.Set_Path(mod.FullName);
                Load_Mod(loaded);
            }
            Dictionary<Slot,Item> slots = new Dictionary<Slot,Item>();
            List<object> values = records.Values.ToList();
            for(int i = 0; i < values.Count; i++)
            {
                if (values[i].GetType() == typeof(Slot))
                {
                    slots.Add((Slot)values[i],null);
                }

            }
            for(int i = 0; i < values.Count; i++)
            {
                if (values[i].GetType() == typeof(Player) || values[i].GetType() == typeof(Entity))
                {
                    Dictionary<Slot, Item> temp = new Dictionary<Slot, Item>(slots);
                    ((Entity)records[((Record)values[i]).Get_Identifier()]).Set_Equipment(temp);
                }
            }
        }
        
        static public void Load_File(string path)
        {
            string text = System.IO.File.ReadAllText(path);
            ITokenSource lexer = new GrammarLexer(new AntlrInputStream(text));
            ITokenStream tokens = new CommonTokenStream(lexer);
            GrammarParser parser = new GrammarParser(tokens);
            parser.BuildParseTree = true;
            IParseTree Tree = parser.file();
            ScriptVisitor visitor = new ScriptVisitor();
            visitor.Visit(Tree);
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
                records.Add(source.Get_Name() + ":" + Path.GetFileNameWithoutExtension(file.Name), added);
                added.MakeTransparent(Color.White);
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
                Load_File(file.FullName);
            }

        }
    }
}
