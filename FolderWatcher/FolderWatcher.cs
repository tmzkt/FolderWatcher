using Serilog;
using System;
using System.IO;

namespace FolderWatcher
{
    public class FolderWatcher
    {
        private readonly IFileSystemWatcher fileSystemWatcher;

        public FolderWatcher(IFileSystemWatcher fileSystemWatcher)
        {
            this.fileSystemWatcher = fileSystemWatcher ?? throw new ArgumentException("Argument cannot be null", "fileSystemWatcher");
            this.fileSystemWatcher.Created += OnCreated;
            this.fileSystemWatcher.Deleted += OnDeleted;
            this.fileSystemWatcher.Changed += OnChanged;
            this.fileSystemWatcher.Renamed += OnRenamed;
            this.fileSystemWatcher.Error += OnError;
            this.fileSystemWatcher.IncludeSubdirectories = true;
            this.fileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
        }

        public bool Start()
        {
            if (IsWatching())
            {
                return false;
            }

            Output($"Starting watcher for {fileSystemWatcher.Path} with filter string {fileSystemWatcher.Filter}");
            fileSystemWatcher.EnableRaisingEvents = true;
            return true;
        }

        public bool Stop()
        {
            if (!IsWatching())
            {
                return false;
            }

            fileSystemWatcher.EnableRaisingEvents = false;
            Output($"Stopped watcher for {fileSystemWatcher.Path} with filter string {fileSystemWatcher.Filter}");
            return true;
        }

        public bool IsWatching()
        {
            return fileSystemWatcher.EnableRaisingEvents;
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
