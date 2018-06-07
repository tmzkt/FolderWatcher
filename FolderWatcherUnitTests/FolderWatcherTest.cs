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
        public void ConstructorExceptionIfPassedNull()
        {
            Assert.Throws<ArgumentException>("fileSystemWatcher", () => new FolderWatcher.FolderWatcher(null));
        }

        [Fact]
        public void ConstructorIsSuccessful()
        {
            new FolderWatcher.FolderWatcher(A.Dummy<IFileSystemWatcher>());
        }

        [Fact]
        public void DoesNotStartIfAlreadyStarted()
        {
            IFileSystemWatcher fakeFileSystemWatcher = A.Fake<IFileSystemWatcher>();
            A.CallTo(() => fakeFileSystemWatcher.EnableRaisingEvents).Returns(true);
            FolderWatcher.FolderWatcher folderWatcher = new FolderWatcher.FolderWatcher(fakeFileSystemWatcher);

            bool isStarted = folderWatcher.Start();

            Assert.False(isStarted);
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
    }
}
