


@echo off
cd /d %~dp0

set inputdlls=%cd%\bin\Release
set outputexe=%cd%\bin\JsonCSharpClassGenerator.exe
set mainassembly=%inputdlls%\JsonCSharpClassGenerator.exe

mkdir "%outputexe%\.."

%Apps%\ILMerge\ILMerge.exe /v2 /t:winexe /wildcards /allowDup /out:"%outputexe%" "%mainassembly%" "%inputdlls%\*.dll"
copy /Y "%mainassembly%.config" "%outputexe%.config"
pause