!include "nsProcess.nsh"


!include "GetParameterValue.nsh"


# WARNING: riferimento a songr@live.com nei messaggi di errore elevazione

!ifdef REQUIREADMIN
	!include "UAC.nsh"
!endif


!ifdef LOCAL
	!define REGHIVE HKCU
!else
	!define REGHIVE HKLM
!endif




OutFile "${DESTINATIONFILE}"


	Function .onInit



		Push "PATH"
		Push ""
		Call GetParameterValue
		Pop $2
		StrCmp $2 "" isempty
		StrCpy $INSTDIR $2 
		isempty:

!ifdef REQUIREADMIN
		uac_tryagain:
		!insertmacro UAC_RunElevated
		#MessageBox mb_TopMost "0=$0 1=$1 2=$2 3=$3"
		${Switch} $0
		
		
		${Case} 0
			${IfThen} $1 = 1 ${|} Quit ${|} ;we are the outer process, the inner process has done its work, we are done
			
			
			${If} $3 <> 0
				;we are admin, let the show go on
				!ifmacrodef OnInitElevatedCustomCode
					!insertmacro OnInitElevatedCustomCode
				!endif
				${Break}
			${EndIf}
			
			${If} $1 = 3 ;RunAs completed successfully, but with a non-admin user
				MessageBox mb_IconExclamation|mb_TopMost|mb_SetForeground "This installer requires admin access, try again?" /SD IDNO IDOK uac_tryagain IDNO 0
			${EndIf}
			;fall-through and die
		${Case} 1223
			MessageBox mb_IconStop|mb_TopMost|mb_SetForeground "This installer requires admin privileges."
			Quit
		${Case} 1062
			MessageBox mb_IconStop|mb_TopMost|mb_SetForeground "Unable to elevate, Logon service not running.$\r$\n$\r$\nPlease report the error to songr@live.com."
			Quit
		${Default}
			MessageBox mb_IconStop|mb_TopMost|mb_SetForeground "Unable to elevate, error $0.$\r$\n$\r$\nPlease report the error to songr@live.com."
			Quit
		${EndSwitch}
!endif

	FunctionEnd	




RequestExecutionLevel user
Name "${APPDISPNAME} ${VERSIONDISPLAY}"
!ifdef LOCAL
InstallDir "$LOCALAPPDATA\${APPNAME}"
!else
InstallDir "$PROGRAMFILES\${APPNAME}"
!endif
VIAddVersionKey  "ProductName" "${APPDISPNAME}"
VIAddVersionKey  "CompanyName" "${AUTHOR}"
VIAddVersionKey  "LegalCopyright" "${COPYRIGHT}"
VIAddVersionKey  "FileDescription" "${APPDISPNAME} Setup"
VIAddVersionKey  "ProductDescription" "${APPDISPNAME} Setup"
VIAddVersionKey  "FileVersion" "${VERSIONFULL}"


VIAddVersionKey  "OriginalFilename" "${FILENAME}"


VIAddVersionKey  "ProductVersion" "${VERSIONFULL} (${VERSIONHASH})"
VIProductVersion "${VERSIONFULL}"
BrandingText "${APPDISPNAME} ${VERSIONDISPLAY} Setup"



!macro WriteUninstallReg estimatedSize
	
	WriteRegStr ${REGHIVE} "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "DisplayName" "${APPDISPNAME}"
	WriteRegStr ${REGHIVE} "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "UninstallString" "$INSTDIR\Uninstall.exe"
	WriteRegDword ${REGHIVE} "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "NoModify" 1
	WriteRegDword ${REGHIVE} "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "NoRepair" 1
	WriteRegDword ${REGHIVE} "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "EstimatedSize" ${estimatedSize}
	WriteRegStr ${REGHIVE} "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "DisplayVersion" "${VERSIONDISPLAY}"
	WriteRegStr ${REGHIVE} "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "DisplayIcon" "$INSTDIR\${MAINEXE},0"
	WriteRegStr ${REGHIVE} "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "Publisher" "${AUTHOR}"
	WriteRegStr ${REGHIVE} "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "URLInfoAbout" "${WEBSITE}"
	

!macroend

!macro CreateShortcuts
	!ifndef STANDALONE
		!ifdef FULLEXE
			CreateShortCut "$DESKTOP\${APPDISPNAME}.lnk" "$INSTDIR\${MAINEXE}" "" "" "" "" "" "${APPDESC}"
		!endif
		CreateShortCut "$SMPROGRAMS\${APPDISPNAME}.lnk" "$INSTDIR\${MAINEXE}" "" "" "" "" "" "${APPDESC}"
	!endif
!macroend

!macro DeleteShortcuts
	Delete "$SMPROGRAMS\${APPDISPNAME}.lnk"
	Delete "$DESKTOP\${APPDISPNAME}.lnk"
!macroend

!macro DeleteUninstallReg
	DeleteRegKey ${REGHIVE} "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}"
!macroend

!macro ExecuteNGEN
		ExecCmd::exec 'call "$WINDIR\Microsoft.NET\Framework\v2.0.50727\ngen.exe" install "$INSTDIR\${MAINEXE}"'
!macroend


!macro OptimizePerformance
		DetailPrint 'Optimizing performance...'
		SetDetailsPrint none
		
		!insertmacro ExecuteNGEN
				
		SetDetailsPrint both
!macroend



!macro OptimizePerformancePreRun
		DetailPrint 'Optimizing performance...'
		SetDetailsPrint none
		
		!insertmacro ExecuteNGEN
		
		; /PostSetupPreInit non è attualmente utilizzato
		ExecWait '"$INSTDIR\${MAINEXE}" /PerformanceTest /PostSetupPreInit'
		
		SetDetailsPrint both
!macroend


!macro UninstallFeedback
	DetailPrint "Please send us feedback about your experience with ${APPDISPNAME}"
	ExecWait '"$INSTDIR\${MAINEXE}" /BeforeUninstall'
!macroend


