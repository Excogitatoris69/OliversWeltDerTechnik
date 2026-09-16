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


