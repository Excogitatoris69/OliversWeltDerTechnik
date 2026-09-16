/* *******************************
Example-1 
********************************** */
grammar SimpleGrammar1;
/* Hinweis: Name muss identisch zu Dateinamen sein. */

/*
************
Parser rules
************
*/

document:
	IDENTIFIER '=' NUMBER
;


/*
************
Lexer rules
************
*/

IDENTIFIER: [a-zA-Z][a-zA-Z]*;

//IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*
//IDENTIFIER1: [a-zA-Z_][a-zA-Z_]*;
//IDENTIFIER2: [a-zA-Z_][a-zA-Z0-9_]*;

NUMBER:
	[0-9]+
;

WHITESPACE: 
	[ \t\n\r]+ -> skip
;



/* *******************************
Example-2
********************************** */
grammar SimpleGrammar1;
/* Hinweis: Name muss identisch zu Dateinamen sein. */

/*
************
Parser rules
************
*/

document:
	IDENTIFIER '=' NUMBER
|   IDENTIFIER '=' STRING
;


/*
************
Lexer rules
************
*/

IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;

NUMBER:
	[0-9]+
;

STRING:
	// '"' ~["]* '"'  // ~["]* -> jedes Zeichen außer "
	'"' ( ESCAPED_CHAR | ~('\\'|'"') )* '"'  // ESCAPED_CHAR -> ein escaped Zeichen oder ~["\\] -> jedes Zeichen außer " und \
;

fragment ESCAPED_CHAR:
	'\\' ["\\]
;


WHITESPACE: 
	[ \t\n\r]+ -> skip
;
 
/* *******************************
Example-3
********************************** */
grammar SimpleGrammar1;

/*
************
Parser rules
************
*/

document:
	variabledef (';' variabledef)* ';'?
;

variabledef:
	IDENTIFIER '=' NUMBER
|   IDENTIFIER '=' STRING
;


/*
************
Lexer rules
************
*/

IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;

NUMBER:
	[0-9]+
;

STRING:
	'"' ( ESCAPED_CHAR | ~('\\'|'"') )* '"'
;

fragment ESCAPED_CHAR:
	'\\' ["\\]
;


WHITESPACE: 
	[ \t\n\r]+ -> skip
;



/* *******************************
Example-4
********************************** */
grammar SimpleGrammar1;

/*
************
Parser rules
************
*/

document:
	variabledef (NEWLINE variabledef)* NEWLINE? EOF
;

variabledef:
	IDENTIFIER '=' NUMBER
|   IDENTIFIER '=' STRING
;


/*
************
Lexer rules
************
*/

IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;

NUMBER:
	[0-9]+
;

STRING:
	'"' ( ESCAPED_CHAR | ~('\\'|'"') )* '"'
	// '"' ( ESCAPED_CHAR | '\r\n' | ~('\\'|'"') )* '"'  // -> mit Zeilentrenner
;

fragment ESCAPED_CHAR:
	'\\' ["\\]
;

NEWLINE:
	'\r\n'
;

WHITESPACE: 
	[ \t\n\r]+ -> skip
;

/* *******************************
Example-5
********************************** */
grammar SimpleGrammar1;

/*
************
Parser rules
************
*/

document:
	//variabledef (NEWLINE variabledef)* NEWLINE? EOF
	variabledef (NEWLINE | variabledef)* NEWLINE? EOF
	//(assignment | NEWLINE)* EOF
;

variabledef:
	IDENTIFIER '=' NUMBER
|   IDENTIFIER '=' STRING
;


/*
************
Lexer rules
************
*/

IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;

NUMBER:
	[0-9]+
;

STRING:
	'"' ( ESCAPED_CHAR | '\r\n' | ~('\\'|'"') )* '"'
;

fragment ESCAPED_CHAR:
	'\\' ["\\]
;

// Zeilenkommentar
LINE_COMMENT:
	'//' ~[\r\n]* -> skip
;

// Blockkommentar, auch über mehrere Zeilen
BLOCK_COMMENT:
	'/*' .*? '*/' -> skip
;

NEWLINE:
	'\r\n'
;

WHITESPACE: 
	[ \t\n\r]+ -> skip
;


/* *******************************
Example-6
********************************** */
grammar SimpleGrammar1;

/*
************
Parser rules
************
*/

document:
	variabledef (NEWLINE | variabledef | chapter)* NEWLINE? EOF
;

variabledef:
	IDENTIFIER '=' NUMBER
|   IDENTIFIER '=' STRING
|   IDENTIFIER '=' BOOLEAN
;

chapter:
	CHAPTER
;

/*
************
Lexer rules
************
*/

// Unquoted String: nur True oder False, unabhängig von Groß-/Kleinschreibung.
BOOLEAN:
	[Tt][Rr][Uu][Ee]
	| [Ff][Aa][Ll][Ss][Ee]
;

IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;

NUMBER:
	[0-9]+
;

STRING:
	'"' ( ESCAPED_CHAR | '\r\n' | ~('\\'|'"') )* '"'
;


//Kapitel
CHAPTER:
	'[' [a-zA-Z0-9._]+ ']'
;

fragment ESCAPED_CHAR:
	'\\' ["\\]
;

// Zeilenkommentar
LINE_COMMENT:
	'//' ~[\r\n]* -> skip
;

// Blockkommentar, auch über mehrere Zeilen
BLOCK_COMMENT:
	'/*' .*? '*/' -> skip
;

NEWLINE:
	'\r\n'
;

WHITESPACE: 
	[ \t\n\r]+ -> skip
;


/* *******************************
Example-7
********************************** */
grammar SimpleGrammar1;

// ============================================================
// Parser
// ============================================================

document:
	element* EOF
    ;

element:
	assignment
    | chapter
    | NEWLINE
    ;

assignment:
	NAME '=' value
    ;

chapter:
	CHAPTER
    ;

value:
	NUMBER
    | BOOLEAN
    | STRING
    ;


// ============================================================
// Lexer
// ============================================================

// ------------------------------------------------------------
// Boolean
// Muss VOR NAME stehen, da z.B. "true" auch NAME matchen würde.
// ------------------------------------------------------------

BOOLEAN:
	[Tt][Rr][Uu][Ee]
    | [Ff][Aa][Ll][Ss][Ee]
    ;


// ------------------------------------------------------------
// Name
// ------------------------------------------------------------

NAME:
	[a-zA-Z_][a-zA-Z0-9_]*
    ;


// ------------------------------------------------------------
// Zahlenwert
// ------------------------------------------------------------

NUMBER:
	[+-]* [0-9]+ ('.' [0-9]+)?
    ;

// ------------------------------------------------------------
// Quoted String
//
// Erlaubt:
//   "Hallo"
//   "Hallo Welt"
//   "Hallo \"Welt\""
//   "C:\\temp"
//   "Mehrzeilig
//   über mehrere Zeilen"
//
// Nicht erlaubt:
//   nicht geschlossene Strings
//   ein nicht escaped " innerhalb des Strings
// ------------------------------------------------------------

STRING:
	'"' (ESCAPED_CHAR | '\r\n' | ~["\\])* '"'
    ;

fragment ESCAPED_CHAR:
	'\\' ["\\]
    ;


// ------------------------------------------------------------
// Kapitel
//
// Beispiele:
//   [ALLGEMEIN]
//   [Allgemein]
//   [CONFIG.PROD]
//   [DATABASE_01]
//   [KAPITEL.1]
// ------------------------------------------------------------

CHAPTER:
	'[' [a-zA-Z0-9._]+ ']'
    ;


// ------------------------------------------------------------
// Kommentare
// ------------------------------------------------------------

// Zeilenkommentar
// Alles bis zum nächsten \r oder \n wird ignoriert.
LINE_COMMENT:
	'//' ~[\r\n]* -> skip
    ;

// Blockkommentar
// .*? erlaubt auch \r\n und damit mehrzeilige Kommentare.
BLOCK_COMMENT:
	'/*' .*? '*/' -> skip
    ;


// ------------------------------------------------------------
// Zeilentrenner
// ------------------------------------------------------------

NEWLINE:
	'\r\n'
    ;


// ------------------------------------------------------------
// Leerzeichen und Tabs ignorieren
// ------------------------------------------------------------

WS:
	[ \t]+ -> skip
    ;





