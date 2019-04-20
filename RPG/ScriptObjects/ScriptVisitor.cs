using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace RPG
{
    class ScriptVisitor : GrammarBaseVisitor<object>
    {
        ScriptObject Target=new ScriptObject();
        object Returned=null;
        string relevant_type;

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
        public object Create_Variable(string scope, string name, object value)
        {
            if (scope == "")
            {
                scope = "Local";
            }
            return Target.Add_Variable(scope, name, value);
        }
        public object Create_Variable(string scope,string name, string type, string value)
        {
            return Create_Variable(scope, name, MyParser.Interpret(type, value));
        }
        public void Set_Target(ScriptObject target)
        {
            Target = target;
        }

        public override object Visit([NotNull] IParseTree tree)
        {
            object returned = base.Visit(tree);
            if (Target != null)
            {
                Target.Clear();
            }
            return Returned;
        }
        public override object VisitCondition([NotNull] GrammarParser.ConditionContext context)
        {
            ScriptObject left = (ScriptObject)Visit(context.simpleExpression(0));
            ScriptObject right = (ScriptObject)Visit(context.simpleExpression(1));

            switch (context.comparator().GetText())
            {
                case "<": return ((left.Compare(right)) < 0);
                case ">": return ((left.Compare(right)) > 0);
                case "<=": return ((left.Compare(right)) <= 0);
                case ">=": return ((left.Compare(right)) >= 0);
                case "==": return ((left.Compare(right)) == 0);
                case "!=": return ((left.Compare(right)) != 0);
                default:
                    throw new Exception("ScriptVisitor:VisitCondition:Invalid comparator: " + context.comparator

().GetText());
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
            return Create_Variable(context.SCOPE().GetText(), context.varName().GetText(), Visit(context.expression()));
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
            object returned = null;
            if (context.varName() != null)
            {
                returned = Get_Variable(context.varName().GetText());
                for (int i = 0; i < context.functionName().Length; i++)
                {
                    object[] args = (object[])VisitArguments(context.arguments(i));
                    returned = ((ScriptObject)returned).Call_Function(context.functionName(i).GetText(), args);
                }
            }
            else
            {
                object[] args = (object[])VisitArguments(context.arguments(0));
                returned = ((Script)Database.Get(context.functionName(0).GetText())).Execute(args);
                for (int i = 1; i < context.functionName().Length; i++)
                {
                    args = (object[])VisitArguments(context.arguments(i));
                    returned = ((ScriptObject)returned).Call_Function(context.functionName(i).GetText(), args);
                }

            }
            return returned;
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
            return Visit(context.elseStatement());
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
                List<double> values = new List<double>();
                List<string> ops = new List<string>();
                for(int i = 0; i < context.@operator().Length; i++)
                {
                    switch (context.@operator(i).GetText())
                    {
                        case "/":
                            values.Add(
                                (double)VisitSimpleExpression(context.simpleExpression(i))
                                / (double)VisitSimpleExpression(context.simpleExpression(i+1))
                                );
                            i++;
                            break;
                        case "*":
                            values.Add(
                           (double)VisitSimpleExpression(context.simpleExpression(i))
                           * (double)VisitSimpleExpression(context.simpleExpression(i + 1))
                           );
                            i++;
                            break;
                        case "^":
                            values.Add(
                            Math.Pow(
                           (double)VisitSimpleExpression(context.simpleExpression(i))
                           ,(double)VisitSimpleExpression(context.simpleExpression(i + 1))
                           ));
                            i++;
                            break;
                        default:
                            ops.Add(context.@operator(i).GetText());
                            values.Add((double)VisitSimpleExpression(context.simpleExpression(i)));break;
                    }
                }
                double returned = 0;
                for(int i = 0; i < ops.Count; i++)
                {
                    switch(ops[i])
                    {
                        case "+":returned += values[i];break;
                        case "-":returned -= values[i];break;
                        default:throw new Exception("ScriptVisitor:VisitExpression:Invalid operator: " + ops[i]);
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
            else if (context.functionStatement() != null)
            {
                return Visit(context.functionStatement());
            }
            else if (context.STRING() != null)
            {
                return context.STRING().GetText();
            }
            else if (context.DOUBLE() != null)
            {
                return double.Parse(context.DOUBLE().GetText());
            }
            else if (context.INT() != null)
            {
                return int.Parse(context.INT().GetText());
            }
            else if (context.REFERENCE() != null)
            {
                return Database.Get(context.REFERENCE().GetText());
            }
            else if (context.INTERPRETED() != null)
            {
                return MyParser.Interpret(relevant_type, context.INTERPRETED().GetText());
            }
            else if (context.BOOLEAN() != null)
            {
                return bool.Parse(context.BOOLEAN().GetText());
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
    }
}
