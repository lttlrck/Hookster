#Requires AutoHotkey v2.0

WinActivate("ahk_exe PimaxClient.exe")

Click 99,38
Click 1374,557
Click 1892,44
Sleep 500
WinActivate("ahk_exe DeviceSetting.exe")
Click 90,171
Click 893,107
Click
Send A_Args[1]
Click 806,690
Sleep 500
Click 990,20