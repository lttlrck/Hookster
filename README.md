# Hookster

This SimHub plugin generates events and calls webhooks, batch files or AHK scripts when telemetry starts, goes idle for a configurable time. 

It also exposes four generic webhook event targets that can be mapped in the Event mapper. 

It will be the home for any other itches I want to scratch in SimHub.

## Events

![image](https://github.com/user-attachments/assets/9b6a362b-9eca-4c48-be6b-84b69905d421)

- `TelemetryActive` is generated when telemetry begins. It is only generated if telemetry was previously idle
- `TelemetryIdle` is generated 4 hours (configurable) after the last telemetry packet is received

![image](https://github.com/user-attachments/assets/44c4ed73-1414-4a52-b677-e7e9d45e4271)

### Scripts

When the events are fired, Hookster will run any configured Idle or Active scripts. Scripts can be a `.bat` or `.ahk`. Hookster expects AutoHotKey to be installed in `C:\Program Files\AutoHotkey\`

## Power Control

I wanted to power off my Bass Shaker amps, Belt Tensioner power supply, VR headset and lighthouses when I stop playing. Sometimes I forget. I don't want to turn them on automatically, but the option is there with `TelemetryActive`.

So I configured an event on `TelemetryIdle` that ran a ShellMacro that uses curl to send an HTTP request to turn off a Shelly Power Plus Outlet.

I later added webhook support so it can do this directly, without event mapping and ShellMacro. To use fill in the appropriate webhook field in the plugin Settings. Multiple webhooks can be separated by `|`. The plug-in will send a GET request to each URL when the condition is met. 

In my case, Idle Webhook is set to `http://192.168.74.183/relay/0?turn=off|http://192.168.74.184/relay/0?turn=off`. This turns off two Shelly Power Plus outlets.

The idle timeout is 4 hours (14400 seconds), it can be changed in the plug-in settings.

![image](https://github.com/user-attachments/assets/0750a28f-1a60-4922-9771-f4fd1771e850)

## VR Profiles

I wanted to instruct PimaxPlay to switch VR game profiles, it doesn't do it automatically:

![image](https://github.com/user-attachments/assets/1a769262-fee3-4dc1-8b4a-83ca77826779)

To achieve this, it is possible to run a batch file or AutoHotKey script when the game name changes.

For my requirement, when the game changes Hookster runs (if present) `C:\Program Files (x86)\SimHub\Hookster\PiToolSelectGame.ahk` with the game name as an argument. This AutoHotKey script performs the actual UI gymnastics to switch games in the PimaxPlay UI. 

The script is configurable and must reside in `C:\Program Files (x86)\SimHub\Hookster\`. Scripts can be a `.bat` or `.ahk`. Hookster expect AutoHotKey to be installed in `C:\Program Files\AutoHotkey\`

## Generic Webhooks

There are four webhook event targets exposed that can be mapped to any event in the SimHub event mapper. 

![image](https://github.com/user-attachments/assets/6382a6a9-7b56-4530-bd2d-5ba1693f66b0)

These webhooks are called Webhook 1, Webhook 2, Webhook 3 and Webhook 4. They can be configured in the plug-in settings. Multiple webhooks can be separated by `|`. The plug-in will send a GET request to each URL when triggered.

![image](https://github.com/user-attachments/assets/a761afb1-e8dc-4bb6-9359-018895a230f8)



