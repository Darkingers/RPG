using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Script:Record
    {
        protected IParseTree Tree;
        protected ScriptVisitor Visitor;
        protected Dictionary<string, string> Arguments;
        protected string Return_Type;

        public Script()
        {
            Copy(null, new ScriptVisitor(), new Dictionary<string,string>(),"INVALID");
        }
        public Script(Record type,IParseTree tree,ScriptVisitor visitor,Dictionary<string,string> arguments,string return_type) : base(type)
        {
            Copy(tree,visitor,arguments,return_type);
        }
        public Script(Script cloned)
        {
            Copy(cloned);
        }
        public bool Copy(Script copied)
        {
            return base.Copy(copied) && Copy(copied.Get_Tree(),copied.Get_Visitor(),copied.Get_Arguments(),copied.Get_Return_Type());
        }
        public bool Copy(IParseTree tree,ScriptVisitor visitor,Dictionary<string,string> arguments,string return_type)
        {
            return
            Set_Tree(tree) &&
            Set_Visitor(visitor) &&
            Set_Arguments(arguments) &&
            Set_Return_Type(return_type);
        }
        public override ScriptObject Clone()
        {
            return new Script(this);
        }
        public override bool Assign(Record copied)
        {
            return Copy((Script)copied);
        }

        public bool Check_Syntax(string text)
        {
            return true;
        }
        public bool Build(string text)
        {
            ITokenSource lexer = new GrammarLexer(new AntlrInputStream(text));
            ITokenStream tokens = new CommonTokenStream(lexer);
            GrammarParser parser = new GrammarParser(tokens);
            parser.BuildParseTree = true;
            Tree = parser.statementList();
            return Check_Syntax(text);
        }
        public object Execute(object[] args)
        {
            if (args.Length != Arguments.Count)
            {
                throw new Exception("Script: " + Get_Identifier() + ":Invalid arguments");
            }

            Visitor.Add_Arguments(Arguments.Keys.ToList(), args);
            return Visitor.Visit(Tree);
        }

        public bool Set_Visitor(ScriptVisitor visitor)
        {
            Visitor = visitor;
            return true;
        }
        public bool Set_Tree(IParseTree tree)
        {
            Tree = tree;
            return true;
        }
        public bool Set_Arguments(Dictionary<string,string> arguments)
        {
            Arguments = arguments;
            return true;
        }
        public bool Set_Return_Type(string type)
        {
            Return_Type = type;
            return true;
        }

        public ScriptVisitor Get_Visitor()
        {
            return Visitor;
        }
        public IParseTree Get_Tree()
        {
            return Tree;
        }
        public Dictionary<string,string> Get_Arguments()
        {
            return Arguments;
        }
        public string Get_Return_Type()
        {
            return Return_Type;
        }

        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Tree": return Set_Tree((IParseTree)value);
                case "Visitor": return Set_Visitor((ScriptVisitor)value);
                case "Arguments": return Set_Arguments(Converter.Convert_Dictionary<string,string>(value));
                case "Executed":return Set_Tree((IParseTree)value);
                default: return base.Set_Variable(name, value);
            }
        }
        public override object Get_Variable(string name)
        {
            switch (name)
            {
                case "Tree": return Get_Tree();
                case "Visitor": return Get_Visitor();
                case "Arguments": return Get_Arguments();
                case "Retrun_Type":return Get_Return_Type();
                default: return base.Get_Variable(name);
            }
        }
        public override object Call_Function(string name, object[] args)
        {
            switch (name)
            {
                case "Execute": return Execute(args);
                case "Build": return Build((string)args[0]);
                case "Check_Syntax": return Check_Syntax((string)args[0]);
                case "Return_Type":return Set_Return_Type((string)args[0]);
                default: return base.Call_Function(name, args);
            }
        }
    }
}
