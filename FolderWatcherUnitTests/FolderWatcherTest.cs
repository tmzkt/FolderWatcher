using System;
using Xunit;

namespace FolderWatcherUnitTests
{
    public class FolderWatcherTest
    {
        [Fact]
        public void ExceptionWhenNullPassedToConstructor()
        {
            Assert.Throws<ArgumentException>("fileSystemWatcher", () => new FolderWatcher.FolderWatcher(null));
        }
    }
}
