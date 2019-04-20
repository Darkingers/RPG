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
        IParseTree Tree;
        ScriptVisitor Visitor;
        List<string> Arguments;

        public Script()
        {
            Copy(null, new ScriptVisitor(), new List<string>());
        }
        public Script(Record type,IParseTree tree,ScriptVisitor visitor,List<string> arguments) : base(type)
        {
            Copy(tree,visitor,arguments);
        }
        public Script(Script cloned)
        {
            Copy(cloned);
        }
        public bool Copy(Script copied)
        {
            return base.Copy(copied) && Copy(copied.Get_Tree(),copied.Get_Visitor(),copied.Get_Arguments());
        }
        public bool Copy(IParseTree tree,ScriptVisitor visitor,List<string> arguments)
        {
            return
            Set_Tree(tree)&&
            Set_Visitor(visitor)&&
            Set_Arguments(arguments);
        }
        public override object Clone()
        {
            return new Script(this);
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
            Tree = parser.script();
            return Check_Syntax(text);
        }
        public object Execute(object[] args)
        {
            if (args.Length != Arguments.Count)
            {
                throw new Exception("Script: " + Get_Identifier() + ":Invalid arguments");
            }

            Visitor.Add_Arguments(Arguments, args);
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
        public bool Set_Arguments(List<string> arguments)
        {
            Arguments = arguments;
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
        public List<string> Get_Arguments()
        {
            return Arguments;
        }

        public override string ToString(string tab)
        {
            string returned =
                base.ToString(tab) +
                tab + MyParser.Write(Tree, "IParseTree", "Tree") +
                tab + MyParser.Write(Visitor, "ScriptVisitor", "Visitor") +
                tab + MyParser.Write(Arguments, "Array<String>", "Arguments");
            return returned;
        }
        public override bool Set_Variable(string name, object value)
        {
            switch (name)
            {
                case "Tree": return Set_Tree((IParseTree)value);
                case "Visitor": return Set_Visitor((ScriptVisitor)value);
                case "Arguments": return Set_Arguments(MyParser.Convert_Array<string>(value));
                case "Executed":return Build((string)value);
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
                default: return base.Call_Function(name, args);
            }
        }
    }
}
