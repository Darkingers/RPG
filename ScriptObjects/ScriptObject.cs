
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class ScriptObject
    {
        static protected Dictionary<string, object> Global_Variables = new Dictionary<string, object>();
        protected Dictionary<string, object> Local_Variables;

        public ScriptObject()
        {
            Copy(new Dictionary<string, object>());
        }
        public ScriptObject(ScriptObject cloned)
        {
            Copy(cloned);
        }
        public ScriptObject(Dictionary<string,object> variables)
        {
            Copy(variables);
        }
        public bool Copy(ScriptObject cloned)
        {
            return Copy(new Dictionary<string,object>(cloned.Get_Variables()));
        }
        public bool Copy(Dictionary<string,object> variables)
        {
            Set_Variables(variables);
            return true;
        }
        public virtual ScriptObject Clone()
        {
            return new ScriptObject(this);
        }

        public virtual bool Set_Variable(string name, object value)
        {
            Local_Variables[name] =value;
            return true;
        }
        public bool Set_Variables(Dictionary<string,object> variables)
        {
            Local_Variables = variables;
            return true;
        }

        public Dictionary<string,object> Get_Variables()
        {
            return Local_Variables;
        }
        public virtual object Get_Variable(string name)
        {
            switch (name)
            {
      
                default: return Local_Variables[name];
            }
        }
        

        public virtual int Compare(object compared)
        {
            return -1;
        }
        public virtual object Call_Function(string name, object[] args)
        {
            switch (name)
            {

                case "Set": Set_Variable((string)args[0], args[1]); return null;
                case "Get": return Get_Variable((string)args[0]);
                case "Clone":return Clone();
                default:throw new Exception("Invalid function name: " + name);
            }
        }

        public void Clear()
        {
            Local_Variables.Clear();
        }
        public bool Add_Variable(string scope,string name,object value)
        {
            if (Set_Variable(name, value))
            {
                return false;
            }
            switch (scope)
            {
                case "Local":
                    if (Local_Variables.ContainsKey(name))
                    {
                        return false;
                    }
                    
                    return true;
                case "Global":
                    if (Global_Variables.ContainsKey(name))
                    {
                        return false;
                    }
                    Global_Variables.Add(name, value);
                    return true;
                default:return false;
            } 
        }
        public bool Remove_Variable(string name)
        {
            return Local_Variables.Remove(name);
        }
        public override string ToString()
        {
            string returned = "";
            foreach(object ob in Local_Variables.Values.ToList())
            {
                returned += ob.ToString() + Environment.NewLine;
            }
            return returned;
        }
        public void Read(string text)
        {
            ITokenSource lexer = new GrammarLexer(new AntlrInputStream(text));
            ITokenStream tokens = new CommonTokenStream(lexer);
            GrammarParser parser = new GrammarParser(tokens);
            parser.BuildParseTree = true;
            IParseTree Tree = parser.block();
            ScriptVisitor visitor = new ScriptVisitor();
            visitor.Set_Target(this);
            visitor.Visit(Tree);
        }
    }
}
