#Requires AutoHotkey v2.0

GAME := A_Args[1]

; Map game names:

if (GAME == "Automobilista2") {
	GAME :="Automobilista 2"
}

WinActivate("ahk_exe PimaxClient.exe")

WinMaximize("PimaxClient")

Click 72,34
Click 2220,570
Sleep 500

WinMinimize("PimaxClient")

WinActivate("ahk_exe DeviceSetting.exe")
Click 90,171
Click 893,107
Click
Send GAME
Click 806,690
Sleep 500
Click 990,20

WinMinimize("Device Settings")
