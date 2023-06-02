# GoCleanYourComputer
A simple computer cleaning tool for Windows.

Written in C# with JetBrains Rider. Thanks to JetBrains for providing me with a free copy of all of their software for developing my open-source projects like this one.

### Here's a list of the commands and what they do:

### Get System Information

Retrieves system config information using the msinfo32 utility and stores it in the same directory as the executable as a .nfo file.

### Remove Temporary Files

Deletes temporary application files in C:\Users\(Your Username)\AppData\Local\Temp.

### Clean Windows Files

Deletes temporary Windows files such as update setup packages using the cleanmgr utility.

### Defragment Hard Drives

Defragments your hard drives using the defrag utility which should make them run faster. You don't need to use this if you have only solid state storage (SSDs) in your computer.

### Remove Widgets

Removes the Widgets app for the taskbar that annoyingly pops up if you hover your mouse over it and wastes bandwidth.

### Disable Telemetry

Disables the Windows "Phone Home" telemetry that sends private data about you to Microsoft so they can server you creepy personalized ads.

### Disable Bing Search Results

Disables Bing results from appearing in Windows search because it sends everything you type to Microsoft. (And to be honest, nobody really even uses Bing anyways)

### Fix Bootloader Distortion

Applies an environment variable that fixes an issue on some systems that results in incorrect scaling on the Windows logo and recovery environment on startup.

### Restore System Image

Updates your Windows recovery image to the latest version and repairs any corruption that may have occurred on it.
