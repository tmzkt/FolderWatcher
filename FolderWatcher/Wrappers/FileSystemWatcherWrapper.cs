using System.IO;

namespace FolderWatcher
{
    public class FileSystemWatcherWrapper : IFileSystemWatcher
    {
        private readonly FileSystemWatcher fileSystemWatcher;

        public FileSystemWatcherWrapper(FileSystemWatcher fileSystemWatcher)
        {
            this.fileSystemWatcher = fileSystemWatcher;
        }

        public string Path { get { return fileSystemWatcher.Path; }  set { fileSystemWatcher.Path = value; } }
        public NotifyFilters NotifyFilter { get { return fileSystemWatcher.NotifyFilter; } set { fileSystemWatcher.NotifyFilter = value; } }
        public bool IncludeSubdirectories { get { return fileSystemWatcher.IncludeSubdirectories; } set { fileSystemWatcher.IncludeSubdirectories = value; } }
        public string Filter { get { return fileSystemWatcher.Filter; } set { fileSystemWatcher.Filter = value; } }
        public bool EnableRaisingEvents { get { return fileSystemWatcher.EnableRaisingEvents; } set { fileSystemWatcher.EnableRaisingEvents = value; } }

        public event FileSystemEventHandler Deleted { add { fileSystemWatcher.Deleted += value; } remove { fileSystemWatcher.Deleted -= value; } }
        public event FileSystemEventHandler Created { add { fileSystemWatcher.Created += value; } remove { fileSystemWatcher.Created -= value; } }
        public event FileSystemEventHandler Changed { add { fileSystemWatcher.Changed += value; } remove { fileSystemWatcher.Changed -= value; } }
        public event RenamedEventHandler Renamed { add { fileSystemWatcher.Renamed += value; } remove { fileSystemWatcher.Renamed -= value; } }
        public event ErrorEventHandler Error { add { fileSystemWatcher.Error += value; } remove { fileSystemWatcher.Error -= value; } }
    }
}
