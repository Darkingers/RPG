grammar Grammar;

/*
 * Parser Rules
 */
 
file				:(object | script)*;
object				:typeName block;
script				:typeName functionName BRACKET1 (typeName varName)?(PUNC typeName varName)* BRACKET2 block;
block				: CURLY1 statementList CURLY2 ;
statementList		:statement*;

statement
		:
		variableDeclaration
		| ifStatement	
		| whileStatement
		| forStatement
		| assignmentStatement
		| functionStatement
		;

variableDeclaration : SCOPE? typeName varName (EQUALS expression)? SEMI;
assignmentStatement	: varName EQUALS expression SEMI;
whileStatement		: WHILE condition  block;
ifStatement			: IF condition  block (elseifStatement* elseStatement)?;
elseifStatement		: ELSEIF condition  block;
elseStatement		: ELSE block;
forStatement		: FOR BRACKET1 assignmentStatement? SEMI condition SEMI assignmentStatement? BRACKET2 block;
functionStatement	: localFunction | globalFunction;

globalFunction		: functionName arguments (DOT functionName arguments)* SEMI;
localFunction		: varName DOT functionName arguments(DOT functionName arguments)* SEMI;

varName			    : ID;
typeName            : ID('<'typeName(','typeName)*'>')?;
functionName        : ID;
dictionary		    : CURLY1((expression'@'expression)(','expression'@'expression)*)?CURLY2;
grid				: CURLY1 array* CURLY2;
array				: CURLY1((expression)(','(expression))*)?CURLY2;

condition			: BRACKET1 simpleExpression comparator simpleExpression BRACKET2;
arguments			: BRACKET1 (expression (PUNC expression)*)? BRACKET2;
comparator			:  LT | GT | LTE | GTE | EQ | NEQ;
operator			: PLUST | MINUS | MULT | DIV | POWER;
enumerator			: '[' ID ']';

expression			: simpleExpression (operator simpleExpression)*;
simpleExpression	
					:
					object
					| functionStatement
					| array
					| varName
					| block
					| dictionary
					| grid
					| enumerator
					| (BOOLEAN
					| NULL
					| DOUBLE
					| INT
					| REFERENCE
					| SCRIPT
					| STRING
					| POINT
					)
					;


/*
 * Lexer Rules
 */
 
PLUST	: '+';
MINUS	: '-';
MULT	: '*';
DIV		: '/';
POWER	: '^';


LT		: '<';
GT		: '>';
LTE		: '<=';
GTE		: '>=';
EQ		: '==';
NEQ		: '!=';



PUNC	: ',';
SEMI	: ';';
EQUALS	: '=';
FOR		: 'for';
SCOPE   : 'static';
IF		: 'if';
ELSEIF	: 'if else';
ELSE	: 'else';
WHILE	: 'while';
CURLY1	: '{';
CURLY2	: '}';
BRACKET1: '(';
BRACKET2: ')';
DOT		: '.';
NULL	: 'null' |'NULL';
BOOLEAN	: 'true' | 'false';
STRING	: '"'(~[\r\n])*'"';
POINT	: '['[0-9]+':'[0-9]+']';
REFERENCE:[a-zA-Z][a-zA-Z0-9_]*':'[a-zA-Z][a-zA-Z0-9_]*;
DOUBLE  : [0-9]+'.'[0-9]+;
INT     : [0-9]+;
ID		: [a-zA-Z][a-zA-Z0-9_]*;

WS		: (' '| '\t' | '\n' | '\r') -> skip;