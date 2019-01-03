# FolderWatcher
FolderWawtcher is command-line .NET Core application that monitors a file directory and outputs all events happening in that directory.

[![folderwatcher screenshot 1](https://raw.githubusercontent.com/tmzkt/FolderWatcher/master/screenshot1.jpg)]

[![folderwatcher screenshot 2](https://raw.githubusercontent.com/tmzkt/FolderWatcher/master/screenshot2.png)]

## How to use
1. Download the zip and open the `appsettings.json` file to configure the `Path` and `Filter` parameters.
`Path` is the path to be monitored
`Filter` is the filter string. Only files matching the filter string will have activity reported.
2. Execute the exe by double clicking or running from the command line
3. Observe the events that are displayed in the console and in the local log file
4. Press Control+C to exit the program and stop the monitoring
