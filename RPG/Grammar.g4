grammar Grammar;

/*
 * Parser Rules
 */
 
file				:object*;
object				:typeName block;
block				: CURLY1 statementList CURLY2 ;
statementList		:statement*;

statement
		:
		variableDeclaration
		| ifStatement	
		| elseifStatement
		| elseStatement
		| whileStatement
		| forStatement
		| assignmentStatement
		| functionStatement
		;

variableDeclaration : SCOPE? typeName varName (EQUALS expression)? SEMI;
whileStatement		: WHILE BRACKET1 condition BRACKET2  block;
ifStatement			: IF BRACKET1 condition BRACKET2  (block)(elseifStatement* elseStatement)?;
elseifStatement		: ELSEIF BRACKET1 condition BRACKET2  ( block);
elseStatement		: ELSE BRACKET1 condition BRACKET2  (block);
forStatement		: FOR BRACKET1 assignmentStatement? SEMI condition SEMI assignmentStatement? BRACKET2 (block);
assignmentStatement	: varName EQUALS expression SEMI;
functionStatement	: (varName DOT)? functionName arguments(DOT functionName arguments)*  SEMI;

varName			    : ID;
typeName            : ID('<'typeName(','typeName)*'>')?;
functionName        : ID;
dictionary		    : CURLY1((expression'@'expression)(','expression'@'expression)*)?CURLY2;
grid				: CURLY1 array* CURLY2;
array				: CURLY1((expression)(','(expression))*)?CURLY2;


arguments			:  BRACKET1(expression (PUNC expression)*)? BRACKET2;
condition			: (simpleExpression) comparator (simpleExpression);
comparator			:  LT | GT | LTE | GTE | EQ | NEQ;
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



operator: PLUST | MINUS | MULT | DIV | POWER;

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

ID		: [a-zA-Z][a-zA-Z0-9_]*;

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
NULL	: 'null';
BOOLEAN	: 'true' | 'false';
STRING	: '"'(~[\r\n])*'"';
SCRIPT	: '['(~[\r\n])*']';
POINT	: [0-9]+':'[0-9]+;
REFERENCE:[a-zA-Z][a-zA-Z0-9_]*':'[a-zA-Z][a-zA-Z0-9_]*;
DOUBLE  : [0-9]+'.'[0-9]+;
INT     : [0-9]+;

WS		: (' '| '\t' | '\n' | '\r') -> skip;