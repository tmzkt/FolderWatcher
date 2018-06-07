using System.IO;

namespace FolderWatcher
{
    public interface IFileSystemWatcher
    {
        string Path { get; set; }
        NotifyFilters NotifyFilter { get; set; }
        bool IncludeSubdirectories { get; set; }
        string Filter { get; set; }
        bool EnableRaisingEvents { get; set; }

        event FileSystemEventHandler Deleted;
        event FileSystemEventHandler Created;
        event FileSystemEventHandler Changed;
        event RenamedEventHandler Renamed;
        event ErrorEventHandler Error;
    }
}
