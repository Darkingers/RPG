grammar Grammar;

/*
 * Parser Rules
 */
 
script				:statementList;
statementList		:statement*;
block				: CURLY1 statementList CURLY2 ;
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
typeName            : ID;
functionName        : ID;

arguments			:  BRACKET1(expression (PUNC expression)*)? BRACKET2;
condition			: (simpleExpression) comparator (simpleExpression);
comparator			:  LT | GT | LTE | GTE | EQ | NEQ;
expression			: simpleExpression (operator simpleExpression)*;
simpleExpression	
					:
					varName
					| functionStatement
					| (STRING
					| BOOLEAN
					| NULL
					| DOUBLE
					| INT
					| INTERPRETED
					| REFERENCE
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
PARSEMODE: 'Object' | 'Script';
BRACKET2: ')';
DOT		: '.';
NULL	: 'null';
BOOLEAN	: 'true' | 'false';
STRING	: '"' (~[\r\n])* '"';
REFERENCE:[a-zA-Z][a-zA-Z0-9_]*':'[a-zA-Z][a-zA-Z0-9_]*;
DOUBLE  : [0-9]+'.'[0-9]+;
INT     : [0-9]+;
INTERPRETED: '{' (~[\r\n\t])* '}';
ID		: [a-zA-Z][a-zA-Z0-9_]*;
WS		: (' '| '\t' | '\n' | '\r') -> skip;