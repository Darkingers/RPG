using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class MyParser
    {
        static public object Interpret(string type, string value)
        {
            string[] parsed= Parse_Typename(type);
            string typename = parsed[0];
            string template = parsed[1];
            object returned = null;
            string[] members;
            string[] temporary; 
            switch (typename)
            {
                case "String":
                    returned = (string)value;
                    break;
                case "Record":
                    returned = new Record();
                    ((Record)returned).Read(value);
                    break;
                case "Trigger":
                    returned = new Trigger();
                    ((Trigger)returned).Read(value);
                    break;
                case "Tile":
                    returned = new Tile();
                    ((Tile)returned).Read(value);
                    break;
                case "Tag":
                    returned = new Tag(); ((Tag)returned).Read(value);
                    break;
                case "Mod":
                    returned = new Mod();
                    ((Mod)returned).Read(value);
                    break;
                case "Record:":
                    returned = Database.Get(value);
                    break;
                case "Numeral":
                    switch (value)
                    {
                        case "Flat": returned = Numeral.Integer; break;
                        case "Percent": returned = Numeral.Double; break;
                    }
                    break;
                case "Relation":
                    switch (value)
                    {
                        case "More": returned = Relation.More; break;
                        case "Less": returned = Relation.Less; break;
                        case "Equal": returned = Relation.Equal; break;

                    }
                    break;
                case "MovementMode":
                    switch (value)
                    {
                        case "Fly": returned = MovementMode.Fly; break;
                        case "Phase": returned = MovementMode.Phase; break;
                        case "Walk": returned = MovementMode.Walk; break;
                    }
                    break;
                case "Modifier":
                    switch (value)
                    {
                        case "Increase": returned = Modifier.Increase; break;
                        case "Decrease": returned = Modifier.Decrease; break;
                    }
                    break;
                case "Player":
                    returned = new Player();
                    ((Player)returned).Read(value);
                    break;
                case "Stat_Resource": 
                    returned = new Stat_Resource();
                    ((Stat_Resource)returned).Read(value);
                    break;
                case "Skill":
                    returned = new Skill();
                    ((Skill)returned).Read(value);
                    break;
                case "Item_Stat":
                    returned = new Item_Stat();
                    ((Item_Stat)returned).Read(value);
                    break;
                case "Stat_Regeneration":
                    returned = new Stat_Regeneration();
                    ((Stat_Regeneration)returned).Read(value);
                    break;
                case "Stat_Modifier":
                    returned = new Stat_Modifier();
                    ((Stat_Modifier)returned).Read(value);
                    break;
                case "Cost":
                    returned = new Cost();
                    ((Cost)returned).Read(value);
                    break;
                case "Slot":
                    returned = new Slot();
                    ((Slot)returned).Read(value);
                    break;
                case "Item":
                    returned = new Item();
                    ((Item)returned).Read(value);
                    break;
                case "Script":

                    returned = new Script();
                    ((Script)returned).Read(value);
                    break;
                case "Cooldown":
                    returned = new Cooldown();
                    ((Cooldown)returned).Read(value);
                    break;
                case "Copy":
                    returned = ((ScriptObject)Database.Get(value)).Clone();
                    break;
                case "Point":
                    temporary = value.Split(',');
                    returned = new Point(int.Parse(temporary[0]),int.Parse(temporary[1]));
                    break;
                case "Map":
                    returned = new Map();
                    ((Map)returned).Read(value);
                    break;
                case "Array"://TODO String splitting,',' causes bug
                    returned = new List<object>();
                    if (value == null)
                    {
                        break;
                    }
                    members = value.Split(',');
                    foreach (string added in members)
                    {
                        ((List<object>)returned).Add(Interpret(template, added));
                    }
                    break;
                case "Dictionary":
                    returned = new Dictionary<object, object>();
                    if(value==null)
                    {
                        break;
                    }
                    temporary = template.Split(',');
                    members = value.Split('&');
                    foreach(string added in members)
                    {
                        string[] pair = added.Split('@');
                        ((Dictionary<object, object>)returned).Add(MyParser.Interpret(temporary[0], pair[0]), MyParser.Interpret(temporary[1], pair[1]));
                    }
                    break;
                case "Grid":
                    temporary = value.Split(',');
                    string[] temp2 = temporary[0].Split(' ');
                    int h = int.Parse(temp2[0]);
                    int w = int.Parse(temp2[1]);
                    returned = new List<List<object>>(h);

                    
                    for(int i = 0; i < h; i++)
                    {
                        ((List<List<object>>)returned).Add(new List<object>(w));
                    }
                    string[] temp = temporary[1].Split(' ');
                    for (int i = 0; i < h; i++)
                    {
                        for(int j = 0; j < w; j++)
                        {
                            ((List<List<object>>)returned)[i][j] = MyParser.Interpret(template, temp[i + j]);
                        }
                    }
                    break;
                case "Reference":
                        returned = Database.Get(value);
                    break;
                default: throw new Exception("Not implemented type conversion: " + type);
            }
            return returned;
        }
       
        static public string[] Parse_Typename(string type)
        {
            string template = "";
            string typename = "";
            int templates = 0;
            ///Iterate over the type
            for (int i = 0; i < type.Length; i++)
            {
                if (templates == 0)
                {
                    if (type[i] == '<')
                    {
                        templates++;
                    }
                    else
                    {
                        typename += type[i];
                    }
                }
                else
                {
                    if (type[i] == '>' && i == type.Length - 1)
                    {
                        templates--;
                    }
                    else if (type[i] == '<')
                    {
                        templates++;
                        template += type[i];
                    }
                    else if (type[i] == '>')
                    {
                        templates--;
                        template += type[i];
                    }
                    else
                    {
                        template += type[i];
                    }
                }
            }
            if (templates != 0)
            {
                throw new Exception("Invalid type: " + type);
            }
            string[] returned = { typename, template };
            return returned;
        }

        static public string[] Parse_Array(string text)
        {
            return text.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
        }
        static public string Write(object value,string type,string name)
        {
            string[] temp = MyParser.Parse_Typename(type);
            string typename = temp[0];
            string tempalte = temp[1];
            string returned =type+":"+name+"{ ";
            switch (typename)
            {
                case "String":returned += (string)value;break;
                case "Double":returned += (double)value;break;
                case "Int":   returned += (int)value; break;
                case "Bool":  returned += (bool)value;break;
                case "Record":returned += ((Record)value).Get_Identifier();break;
                case "Relation":switch ((Relation)value) {
                        case Relation.Equal: returned += "Equal";break;
                        case Relation.Less: returned += "Less";break;
                        case Relation.More: returned += "More";break;
                    } break;
                case "Array":
                    
                default: returned += "Not implemented write: " + type;break;
            }
            returned += " }" + Environment.NewLine;
            return returned;
        }
        static public List<T> Convert_Array<T>(object converted)
        {
            List<object> temp = (List<object>)converted;
            List<T> returned = new List<T>();
            foreach(object iter in temp)
            {
                returned.Add((T)iter);
            }
            return returned;
        }
        static public Dictionary<TKey,TValue> Convert_Dictionary<TKey,TValue>(object converted)
        {
            Dictionary<object,object> temp = (Dictionary<object,object>)converted;
            Dictionary<TKey,TValue> returned=new Dictionary<TKey, TValue>();
            object[] keys = temp.Keys.ToArray();
            object[] values = temp.Values.ToArray();
            for(int i = 0; i < keys.Length; i++)
            {
                returned.Add((TKey)keys[i], (TValue)values[i]);
            }
            return returned;
        }
        static public List<List<T>> Convert_Grid<T>(object converted)
        {
            List<object> temp = (List<object>)converted;
            List<List<T>> returned = new List<List<T>>();
            for(int i = 0; i < temp.Count; i++)
            {
                returned.Add(new List<T>());
                List<object> rowtemp = (List<object>)temp[i];
                for(int j = 0; j < rowtemp.Count; j++)
                {
                    returned[i].Add((T)rowtemp[j]);
                }
            }
            return returned;
        }

        static public string[] Create_Tokens(string text)
        {
            List<string> returned = new List<string>();
            int brackets = 0;
            string attribute = "";
            int stage = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if((text[i]=='\r' || text[i] == '\t' || text[i]=='\n' || text[i] == ' ') && stage==0)
                {
                    continue;
                }
                else if(text[i]=='{')
                {
                    brackets++;
                    stage = 1;
                    attribute += text[i];
                }
                else if (text[i] == '}')
                {
                    brackets--;
                    attribute += text[i];
                    if (brackets == 0)
                    {
                        returned.Add(attribute);
                        stage = 0;
                        attribute = "";
                    }
                }
                else
                {
                    attribute += text[i];
                }
            }
            return returned.ToArray();
        }
        static public string Remove_Characters(string text,char[] characters,char delimiter)
        {
            if (text == null)
            {
                return "";
            }
            string returned = "";
            bool remove;
            bool delimit = false;
            foreach (char c in text)
            {
                remove = false;
                if (c == delimiter && !delimit)
                {
                    delimit = true;
                    continue;
                }
                else if(c==delimiter && delimit)
                {
                    delimit = false;
                    continue;
                }
                if (delimit)
                {
                    returned += c;
                }
                else
                {
                    foreach (char removed in characters)
                    {
                        if (c == removed)
                        {
                            remove = true;
                            break;
                        }
                    }
                    if (!remove)
                    {
                        returned += c;
                    }
                }
            }
            return returned;
        }
    }
}
