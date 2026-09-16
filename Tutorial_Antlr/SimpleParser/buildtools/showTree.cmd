@echo off
rem -----------------------------------------------------------------------------------
rem Script for Show Grammar Tree
rem Author: Oliver Matle, September 2026
rem -----------------------------------------------------------------------------------

rem -----------------------------
rem settings
rem -----------------------------
rem set grammarName=SimpleGrammar1
set grammarName=ExtendedIniGrammar
set grammarStart=document
set testdataFile=%cd%\..\testdata\test_config.ini
set rootPath=E:\eigenes\Projekte


rem -----------------------------
rem intern settings
rem -----------------------------
set grammarfile=%cd%\..\%grammarName%.g4
set antlrlib=%rootPath%\Tutorial_Antlr\Tools\antlr-4.13.2-complete.jar
set genJavaPath=%cd%\..\genjava
set classpath= %antlrlib%;%genJavaPath%

rem -----------------------------
rem gen java wegen show Tree Gui
rem -----------------------------
echo generate java files to %genJavaPath%
java -jar %antlrlib% -no-listener -visitor -o %genJavaPath%  %grammarfile%
echo java compile
javac -cp %antlrlib% %genJavaPath%\*.java


rem -----------------------------
rem Show Tree in Window
rem -----------------------------
echo Show graphical Tree.
rem echo Type your input and close with CTRL-Z
java -cp %classpath% org.antlr.v4.gui.TestRig %grammarName% %grammarStart% -gui %testdataFile%
