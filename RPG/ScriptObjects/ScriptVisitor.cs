using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace RPG
{
    class ScriptVisitor : GrammarBaseVisitor<object>
    {
        static protected Dictionary<string, object> Factory = new Dictionary<string, object>()
        {
            { "Mod", new Mod() },
            { "Record",new Record() },
            { "Tile",new Tile() },
            { "Tag",new Tag() },
            {"Slot",new Slot() },
            {"Stat_Resource",new Stat_Resource() },
            {"Stat_Regeneration", new Stat_Regeneration() },
            {"Stat_Modifier",new Stat_Modifier() },
            {"Script",new Script() },
            {"Skill",new Skill() },
            {"Item",new Item() },
            {"Map",new Map() },
            {"Entity",new Entity() },
            {"Player",new Player() },
            {"Item_Stat",new Item_Stat() },
            {"Cost_Value",new Cost_Value() },
            {"Cost",new Cost() },
            {"Cooldown",new Cooldown() },
            {"Effect",new Effect() },
            {"Trigger",new Trigger() }
        };
        protected ScriptObject Target = new ScriptObject();
        protected object Returned =null;
        protected string relevant_type;
        static protected Mod Source;

        public bool Add_Arguments(List<string> arg_names, object[] values)
        {
            for (int i = 0; i < arg_names.Count; i++)
            {
                Target.Add_Variable("Local", arg_names[i], values[i]);
            }
            return true;
        }
        public object Get_Variable(string name)
        {
            return Target.Get_Variable(name);
        }
        public object Set_Variable(string name, object value)
        {
            return Target.Set_Variable(name, value);
        }
        public static void Set_Source(Mod source)
        {
            Source = source;
        }
        public object Create_Variable(string scope, string name, object value)
        {
            if (scope == "")
            {
                scope = "Local";
            }
            return Target.Add_Variable(scope, name, value);
        }
        public void Set_Target(ScriptObject target)
        {
            Target = target;
        }


        public override object VisitFile([NotNull] GrammarParser.FileContext context)
        {
            for (int i = 0; i < context.script().Length; i++)
            {
                System.Console.WriteLine(context.script(i).functionName().GetText() + " Errors:");
                Record added = (Script)Visit(context.script(i));
                added.Set_Source(Source);
                System.Console.WriteLine(added.Get_Identifier());
                Database.Add_Record(added);
            }
            for (int i = 0; i < context.@object().Length; i++)
            {
                System.Console.WriteLine(context.@object(i).typeName().GetText() + " Errors:");
                Record added = (Record)Visit(context.@object(i));
                added.Set_Source(Source);
                System.Console.WriteLine( added.Get_Identifier());
                Database.Add_Record(added);
            }
            return null;
        }
        public override object VisitObject([NotNull] GrammarParser.ObjectContext context)
        {
            string type = context.typeName().GetText();
            ScriptObject returned = ((ScriptObject)Factory[context.typeName().GetText()]).Clone();
            ScriptObject temp = Target;
            
            Target = returned;
            Visit(context.block());
            Target = temp;
            
            return returned;
        }
        public override object VisitScript([NotNull] GrammarParser.ScriptContext context)
        {
            Script returned = new Script();
            returned.Set_Return_Type(context.typeName(0).GetText());
            returned.Set_Name(context.functionName().GetText());
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            for(int i = 1; i < context.typeName().Length; i++)
            {
                arguments.Add(context.varName(i-1).GetText(),context.typeName(i).GetText());
            }
            returned.Set_Arguments(arguments);
            returned.Set_Tree(context.block());
            return returned;
        }
        public override object VisitCondition([NotNull] GrammarParser.ConditionContext context)
        {
            object left = Visit(context.simpleExpression(0));
            object right = Visit(context.simpleExpression(1));
            switch (context.comparator().GetText())
            {
                case "<": return (double)left < (double)right;
                case ">": return (double)left > (double)right;
                case "<=": return (double)left <= (double)right;
                case ">=": return (double)left >= (double)right;
                case "==":
                    if (left != null) return left.Equals(right);
                    else if (right != null) return right.Equals(left);
                    else return true;
                case "!=":
                    if (left != null) return !left.Equals(right);
                    else if (right != null) return !right.Equals(left);
                    else return false;
                default:
                    throw new Exception("ScriptVisitor:VisitCondition:Invalid comparator: " + context.comparator().GetText());
            }
        }
        public override object VisitBlock([NotNull] GrammarParser.BlockContext context)
        {
            for (int i = 0; i < context.statementList().statement().Length; i++)
            {
                Visit(context.statementList().statement(i));
            }
            return null;
        }
        public override object VisitVariableDeclaration([NotNull] GrammarParser.VariableDeclarationContext context)
        {
            relevant_type = context.typeName().GetText();
            string scope = "";
            if (context.SCOPE() == null)
            {
                scope = "Local";
            }
            else
            {
               scope = context.SCOPE().GetText();
            }
            object value = null;
            if (context.expression() == null)
            {
                value = null;
            }
            else
            {
                value = Visit(context.expression());
            }
            return Create_Variable(scope, context.varName().GetText(),value);
        }
        public override object VisitWhileStatement([NotNull] GrammarParser.WhileStatementContext context)
        {
            while ((bool)Visit(context.condition()))
            {
                Visit(context.block());
            }
            return null;
        }
        public override object VisitAssignmentStatement([NotNull] GrammarParser.AssignmentStatementContext context)
        {
            return Set_Variable(context.varName().GetText(), Visit(context.expression()));
        }
        public override object VisitForStatement([NotNull] GrammarParser.ForStatementContext context)
        {
            Visit(context.assignmentStatement(0));
            while ((bool)Visit(context.condition()))
            {
                Visit(context.block());
                Visit(context.assignmentStatement(1));
            }
            return null;
        }
        public override object VisitFunctionStatement([NotNull] GrammarParser.FunctionStatementContext context)
        {
            return Visit(context.function());
        }
        public override object VisitFunction([NotNull] GrammarParser.FunctionContext context)
        {
            if (context.localFunction() != null)
            {
                return Visit(context.localFunction());
            }
            else if (context.globalFunction() != null)
            {
                return Visit(context.globalFunction());
            }
            else return null;
        }
        public override object VisitLocalFunction([NotNull] GrammarParser.LocalFunctionContext context)
        {
            object lastreturn = Get_Variable(context.varName().GetText());
            for(int i = 0; i < context.functionName().Length; i++)
            {
                ScriptObject temp = (ScriptObject)lastreturn;
                string name = context.functionName(i).GetText();
                object[] arguments = (object[])Visit(context.arguments(i));
                System.Console.WriteLine("In function: " +name);
                lastreturn =temp.Call_Function(name,arguments );
            }
            return lastreturn;
        }
        public override object VisitGlobalFunction([NotNull] GrammarParser.GlobalFunctionContext context)
        {
            object lastreturn= Global_Functions.Call_Function(
                context.functionName(0).GetText(),
                (object[]) Visit(context.arguments(0))
                );
            for(int i = 1; i < context.functionName().Length; i++)
            {

                lastreturn = ((ScriptObject)lastreturn).Call_Function(
                    context.functionName(i).GetText(),
                    (object[])Visit(context.arguments(i))
                    );
            }
            return lastreturn;
        }
        public override object VisitIfStatement([NotNull] GrammarParser.IfStatementContext context)
        {
            if ((bool)Visit(context.condition()))
            {
                Visit(context.block());
                return true;
            }
            for (int i = 0; i < context.elseifStatement().Length; i++)
            {
                if ((bool)Visit(context.elseifStatement(i)))
                {
                    return true;
                }
            }
            if (context.elseStatement() != null)
            {
                return Visit(context.elseStatement());
            }
            else return null; 
        }
        public override object VisitElseifStatement([NotNull] GrammarParser.ElseifStatementContext context)
        {
            if ((bool)Visit(context.condition()))
            {
                return Visit(context.block());
            }
            else return null;
        }
        public override object VisitElseStatement([NotNull] GrammarParser.ElseStatementContext context)
        {
            Visit(context.block());
            return true;
        }
        public override object VisitExpression([NotNull] GrammarParser.ExpressionContext context)
        {
            if (context.simpleExpression().Length == 1)
            {
                return VisitSimpleExpression(context.simpleExpression(0));
            }
            else
            {
                object returned= VisitSimpleExpression(context.simpleExpression(0));
                for (int i = 1; i < context.simpleExpression().Length; i++)
                {
                    if (returned.GetType() == typeof(string))
                    {
                        switch (context.@operator(i-1).GetText())
                        {
                            case "+":
                                returned=(string)returned+(string)VisitSimpleExpression(context.simpleExpression(i));
                                break;
                        }

                    }
                }
                return returned;
            }
                
        }
        public override object VisitSimpleExpression([NotNull] GrammarParser.SimpleExpressionContext context)
        {

            if (context.varName() != null)
            {
                return Target.Get_Variable(context.varName().GetText());
            }
            else if (context.@object() != null)
            {
                return Visit(context.@object());
            }
            else if (context.array() != null)
            {
                List<object> returned = new List<object>();
                for(int i = 0; i < context.array().expression().Length;i++)
                {
                    returned.Add(Visit(context.array().expression(i)));
                }
                return returned;
            }
            else if (context.enumerator() != null)
            {
                return Visit(context.enumerator());
            }
            else if (context.grid() != null)
            {
                List < List<object> > returned= new List<List<object>>();
                for(int i = 0; i < context.grid().array().Length; i++)
                {
                    returned.Add((List<object>)Visit(context.grid().array(i)));
                }
                return returned;
            }
            else if (context.block() != null)
            {
                return context.block();
            }
            else if (context.dictionary() != null)
            {
                Dictionary<object, object> returned = new Dictionary<object, object>();
                for (int i = 0; i < context.dictionary().expression().Length; i++)
                {
                    returned.Add(Visit(context.dictionary().expression(i)), Visit(context.dictionary().expression(++i)));
                }
                return returned;
            }
            else if (context.function() != null)
            {
                return Visit(context.function());
            }
            else if (context.STRING() != null)
            {
                string text = context.STRING().GetText();
                text=text.Remove(0, 1);
                text = text.Remove(text.Length-1, 1);
                return text;
            }
            else if (context.POINT() != null)
            {
                string[] parsed = context.POINT().GetText().Split(':');
                parsed[0] = parsed[0].Remove(0, 1);
                parsed[1] = parsed[1].Remove(parsed[1].Length - 1, 1);
                return new Point(int.Parse(parsed[0]), int.Parse(parsed[1]));
            }
            else if (context.DOUBLE() != null)
            {
                return double.Parse(context.DOUBLE().GetText().Replace('.',','));
            }
            else if (context.INT() != null)
            {
                return int.Parse(context.INT().GetText());
            }
            else if (context.REFERENCE() != null)
            {
                return Database.Get(context.REFERENCE().GetText());
            }
            else if (context.BOOLEAN() != null)
            {
                return bool.Parse(context.BOOLEAN().GetText());
            }
            else if (context.SCRIPT() != null)
            {
                string text = context.SCRIPT().GetText();
                text = text.Remove(0, 1);
                text = text.Remove(text.Length - 1, 1);
                return text;
            }
            
            else return null;
        } 
        public override object VisitArguments([NotNull] GrammarParser.ArgumentsContext context)
        {
            List<object> args = new List<object>();
            for (int i = 0; i < context.expression().Length; i++)
            {
                args.Add(Visit(context.expression(i)));
            }
            return args.ToArray();
        }
        public override object VisitEnumerator([NotNull] GrammarParser.EnumeratorContext context)
        {
            switch (context.ID().GetText())
            {
                case "Current":return Number.Current;
                case "Fly": return MovementMode.Fly;
                case "Walk": return MovementMode.Walk;
                case "Phase": return MovementMode.Phase;
                case "Decrease": return Modifier.Decrease;
                case "Increase": return Modifier.Increase;
                case "Flat": return Number.Flat;
                case "Percent": return Number.Percent;
                case "More": return Relation.More;
                case "Equal": return Relation.Equal;
                case "Less": return Relation.Less;
                default:return null;
            }
        }
    }
}
