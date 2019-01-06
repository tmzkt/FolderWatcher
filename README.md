# FolderWatcher
FolderWawtcher is command-line .NET Core application that monitors a file directory and outputs all events happening in that directory.

[![folderwatcher screenshot 1](https://raw.githubusercontent.com/tmzkt/FolderWatcher/master/screenshot1.jpg)]

[![folderwatcher screenshot 2](https://raw.githubusercontent.com/tmzkt/FolderWatcher/master/screenshot2.png)]

## System requirements
1. .NET Core 2.0

## How to use
1. Go to releases, download the zip.
2. Open the `appsettings.json` file to configure the `Path` and `Filter` parameters.
`Path` is the path to be monitored
`Filter` is the filter string. Only files matching the filter string will have activity reported.
3. Open the command line and execute `dotnet FolderWatcher.dll` from the downloaded folder.
4. Observe the events that are displayed in the console and in the local log file.
5. Press Control+C to exit the program and stop watching the folder.
