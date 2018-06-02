using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace FolderWatcher
{
    class FolderWatcher
    {
        private readonly FileSystemWatcher FileSystemWatcher;

        public FolderWatcher(string path)
        {
            // TODO bad path handling
            FileSystemWatcher = new FileSystemWatcher(path);
            Initialize();
        }

        public FolderWatcher(string path, string filter)
        {
            // TODO bad path and filter string handling
            FileSystemWatcher = new FileSystemWatcher(path, filter);
            Initialize();
        }

        private void Initialize()
        {
            FileSystemWatcher.Created += OnCreated;
            FileSystemWatcher.Deleted += OnDeleted;
            FileSystemWatcher.Changed += OnChanged;
            FileSystemWatcher.Renamed += OnRenamed;
            FileSystemWatcher.Error += OnError;
            //FileSystemWatcher.IncludeSubdirectories = true;
            FileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
        }

        public bool Start()
        {
            if (IsWatching())
            {
                return false;
            }

            Output($"Starting watcher for {FileSystemWatcher.Path} with filter string {FileSystemWatcher.Filter}");
            FileSystemWatcher.EnableRaisingEvents = true;
            return true;
        }

        public bool Stop()
        {
            if (!IsWatching())
            {
                return false;
            }

            FileSystemWatcher.EnableRaisingEvents = false;
            Output("Stopped watcher for {FileSystemWatcher.Path} with filter string {FileSystemWatcher.Filter}");
            return true;
        }

        public bool IsWatching()
        {
            return FileSystemWatcher.EnableRaisingEvents;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            Output(string.Format("Create {0}", e.Name));
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Output(string.Format("Delete {0}", e.Name));
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Output(string.Format("Change {0}", e.Name));
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            Output(string.Format("Rename {0} -> {1}", e.OldName, e.Name));
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            Output(string.Format("Exception {0}", e.GetException()));
        }

        private void Output(string msg)
        {
            Log.Information(msg);
        }
    }
}
