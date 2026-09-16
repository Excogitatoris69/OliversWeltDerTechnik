@echo off
rem -----------------------------------------------------------------------------------
rem Script for build Parser and Lexer
rem Author: Oliver Matle, September 2026
rem -----------------------------------------------------------------------------------

rem -----------------------------
rem settings
rem -----------------------------
set rootPath=E:\eigenes\Projekte
set package=ExtendedIniParser
set grammarfile=%cd%\..\ExtendedIniGrammar.g4
set destpath=%cd%\..\gencsharp
set genJavaPath=%cd%\..\genjava

rem -----------------------------
rem gen c#
rem -----------------------------
echo generate c# files to %destpath%
set antlrlib=%rootPath%\Tutorial_Antlr\Tools\antlr-4.13.2-complete.jar
rem -no-listener
java -jar %antlrlib% -Dlanguage=CSharp  -visitor -no-listener -package %package% -o %destpath%  %grammarfile%


echo Finished. Now build your c# project

