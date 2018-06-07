using System.IO;

namespace FolderWatcher
{
    public class FileSystemWatcherWrapper : IFileSystemWatcher
    {
        private readonly FileSystemWatcher fileSystemWatcher;

        public FileSystemWatcherWrapper(FileSystemWatcher fileSystemWatcher)
        {
            this.fileSystemWatcher = fileSystemWatcher;
            this.fileSystemWatcher.Deleted += this.Deleted;
            this.fileSystemWatcher.Created += this.Created;
            this.fileSystemWatcher.Changed += this.Changed;
            this.fileSystemWatcher.Renamed += this.Renamed;
            this.fileSystemWatcher.Error += this.Error;
        }

        public string Path { get { return fileSystemWatcher.Path; }  set { fileSystemWatcher.Path = value; } }
        public NotifyFilters NotifyFilter { get { return fileSystemWatcher.NotifyFilter; } set { fileSystemWatcher.NotifyFilter = value; } }
        public bool IncludeSubdirectories { get { return fileSystemWatcher.IncludeSubdirectories; } set { fileSystemWatcher.IncludeSubdirectories = value; } }
        public string Filter { get { return fileSystemWatcher.Filter; } set { fileSystemWatcher.Filter = value; } }
        public bool EnableRaisingEvents { get { return fileSystemWatcher.EnableRaisingEvents; } set { fileSystemWatcher.EnableRaisingEvents = value; } }

        public event FileSystemEventHandler Deleted;
        public event FileSystemEventHandler Created;
        public event FileSystemEventHandler Changed;
        public event RenamedEventHandler Renamed;
        public event ErrorEventHandler Error;
    }
}
