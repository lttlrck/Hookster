This is a simhub plugin that generates events when telemetry starts and stops:

- TelemetryActive is generated when telemetry begins. It is only generated if telemetry was previously idle
- TelemetryIdle is generated 2 hours after the last telemetry packet is received

The idle timeout of 2 hours is hardcoded, honeslty I was too lazy to wire up the UI (the UI is there though).

Why? I wantedf to power off my Bass Shaker amps, Belt Tensioner power supply, VR headset and lighthouses when I stop playing. Sometimes I forget. 

I only use TelemetryIdle. It runs a ShellMacro that uses curl to sent an HTTP request to turn off a Shelly Power Outlet :-)

It was hacked together, but maybe it is useful.
