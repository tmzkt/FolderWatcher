using FakeItEasy;
using FolderWatcher;
using System;
using System.IO;
using Xunit;

namespace FolderWatcherUnitTests
{
    public class FolderWatcherTest
    {
        [Fact]
        public void ConstructorDoesNotThrowExceptions()
        {
            new FolderWatcher.FolderWatcher(A.Dummy<IFileSystemWatcher>());
        }

        [Fact]
        public void ConstructorThrowsExceptionIfPassedNull()
        {
            Assert.Throws<ArgumentException>("fileSystemWatcher", () => new FolderWatcher.FolderWatcher(null));
        }

        [Fact]
        public void Starts()
        {
            IFileSystemWatcher fakeFileSystemWatcher = A.Fake<IFileSystemWatcher>();
            A.CallTo(() => fakeFileSystemWatcher.EnableRaisingEvents).Returns(false);
            FolderWatcher.FolderWatcher folderWatcher = new FolderWatcher.FolderWatcher(fakeFileSystemWatcher);

            bool isStarted = folderWatcher.Start();

            Assert.True(isStarted);
            A.CallToSet(() => fakeFileSystemWatcher.EnableRaisingEvents).To(true).MustHaveHappened();
        }

        [Fact]
        public void StartReturnsFalseIfAlreadyStarted()
        {
            IFileSystemWatcher fakeFileSystemWatcher = A.Fake<IFileSystemWatcher>();
            A.CallTo(() => fakeFileSystemWatcher.EnableRaisingEvents).Returns(true);
            FolderWatcher.FolderWatcher folderWatcher = new FolderWatcher.FolderWatcher(fakeFileSystemWatcher);

            bool isStarted = folderWatcher.Start();

            Assert.False(isStarted);
        }

        [Fact]
        public void Stops()
        {
            IFileSystemWatcher fakeFileSystemWatcher = A.Fake<IFileSystemWatcher>();
            A.CallTo(() => fakeFileSystemWatcher.EnableRaisingEvents).Returns(true);
            FolderWatcher.FolderWatcher folderWatcher = new FolderWatcher.FolderWatcher(fakeFileSystemWatcher);

            bool isStopped = folderWatcher.Stop();

            Assert.True(isStopped);
            A.CallToSet(() => fakeFileSystemWatcher.EnableRaisingEvents).To(false).MustHaveHappened();
        }

        [Fact]
        public void StopReturnsFalseIfAlreadyStopped()
        {
            IFileSystemWatcher fakeFileSystemWatcher = A.Fake<IFileSystemWatcher>();
            A.CallTo(() => fakeFileSystemWatcher.EnableRaisingEvents).Returns(false);
            FolderWatcher.FolderWatcher folderWatcher = new FolderWatcher.FolderWatcher(fakeFileSystemWatcher);

            bool isStopped = folderWatcher.Stop();

            Assert.False(isStopped);
        }

        [Fact]
        public void IsWatchingWhenEnableRaisingEventsIsTrue()
        {
            IFileSystemWatcher fakeFileSystemWatcher = A.Fake<IFileSystemWatcher>();
            A.CallTo(() => fakeFileSystemWatcher.EnableRaisingEvents).Returns(true);
            FolderWatcher.FolderWatcher folderWatcher = new FolderWatcher.FolderWatcher(fakeFileSystemWatcher);

            bool isWatching = folderWatcher.IsWatching();

            Assert.True(isWatching);
        }

        [Fact]
        public void IsNotWatchingWhenEnableRaisingEventsIsFalse()
        {
            IFileSystemWatcher fakeFileSystemWatcher = A.Fake<IFileSystemWatcher>();
            A.CallTo(() => fakeFileSystemWatcher.EnableRaisingEvents).Returns(false);
            FolderWatcher.FolderWatcher folderWatcher = new FolderWatcher.FolderWatcher(fakeFileSystemWatcher);

            bool isWatching = folderWatcher.IsWatching();

            Assert.False(isWatching);
        }
    }
}
