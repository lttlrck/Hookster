This is a simhub plugin that generates events when telemetry starts and stops:

- TelemetryActive is generated when telemetry begins. It is only generated if telemetry was previously idle
- TelemetryIdle is generated 2 hours after the last telemetry packet is received

![image](https://github.com/user-attachments/assets/53de6d97-0db0-4925-a2ac-2d384ec15750)

The idle timeout of 2 hours is hardcoded, honeslty I was too lazy to wire up the UI (the UI is there though).

Why? I wanted to power off my Bass Shaker amps, Belt Tensioner power supply, VR headset and lighthouses when I stop playing. Sometimes I forget. I don't want to turn them on automatically, but the option is there. 

So I configure an event on TelemetryIdle. It runs a ShellMacro that uses curl to sent an HTTP request to turn off a Shelly Power Plus Outlet :-)

It was hacked together, but maybe it is useful.
