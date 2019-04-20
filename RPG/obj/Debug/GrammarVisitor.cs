//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Gabor\source\repos\RPG\RPG\Grammar.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace RPG {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="GrammarParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface IGrammarVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.script"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitScript([NotNull] GrammarParser.ScriptContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.statementList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementList([NotNull] GrammarParser.StatementListContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlock([NotNull] GrammarParser.BlockContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] GrammarParser.StatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.variableDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableDeclaration([NotNull] GrammarParser.VariableDeclarationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.whileStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileStatement([NotNull] GrammarParser.WhileStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfStatement([NotNull] GrammarParser.IfStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.elseifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseifStatement([NotNull] GrammarParser.ElseifStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.elseStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseStatement([NotNull] GrammarParser.ElseStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.forStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForStatement([NotNull] GrammarParser.ForStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.assignmentStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignmentStatement([NotNull] GrammarParser.AssignmentStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.functionStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionStatement([NotNull] GrammarParser.FunctionStatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.varName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarName([NotNull] GrammarParser.VarNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.typeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeName([NotNull] GrammarParser.TypeNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.functionName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionName([NotNull] GrammarParser.FunctionNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.arguments"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArguments([NotNull] GrammarParser.ArgumentsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCondition([NotNull] GrammarParser.ConditionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.comparator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComparator([NotNull] GrammarParser.ComparatorContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] GrammarParser.ExpressionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.simpleExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimpleExpression([NotNull] GrammarParser.SimpleExpressionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.operator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperator([NotNull] GrammarParser.OperatorContext context);
}
} // namespace RPG
