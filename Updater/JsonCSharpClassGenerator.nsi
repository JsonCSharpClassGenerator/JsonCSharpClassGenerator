SetCompressor /SOLID lzma

!AddIncludeDir "NSIS"
!AddPluginDir "NSIS"

!include "MUI2.nsh"


!define LOCAL
!define STANDALONE


!include "JsonCSharpClassGenerator.CompilationInfo.nsh"


!define APPNAME "JsonCSharpClassGenerator"
!define APPDISPNAME "JSON C# Class Generator"
!define MAINEXE "JsonCSharpClassGenerator.exe"
!define APPDESC "Static class generator from sample JSON data"
!define COPYRIGHT "© 2009-2012 Andrea Martinelli"
!define WEBSITE "http://at-my-window.blogspot.com/?page=songr"
!define AUTHOR "Andrea Martinelli"

!include "Common2.nsh"




!define MUI_ABORTWARNING

!define MUI_FINISHPAGE_RUN
!define MUI_FINISHPAGE_RUN_TEXT "Launch ${APPDISPNAME}"

!define MUI_FINISHPAGE_RUN_FUNCTION "UserLaunchApp"




!insertmacro MUI_PAGE_INSTFILES

!insertmacro MUI_LANGUAGE "English"


!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
!insertmacro MUI_FUNCTION_DESCRIPTION_END




!insertmacro MUI_RESERVEFILE_LANGDLL





Section ""
	SetOutPath "$INSTDIR"
	
	${nsProcess::KillProcess} "JsonCSharpClassGenerator.exe" $R0
	Sleep 500


	File "..\bin\${MAINEXE}.config"
	File "..\bin\${MAINEXE}"


	Exec '"$INSTDIR\${MAINEXE}"'
	Quit

SectionEnd


