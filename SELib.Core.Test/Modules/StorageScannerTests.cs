using SELib.Core.Modules;

namespace SELib.Core.Test.Modules
{
    public class StorageScannerTests : IDisposable
    {
        private readonly string _tempDir;

        public StorageScannerTests()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(_tempDir);

            byte[] largeData = new byte[1024 * 1024];
            byte[] smallData = new byte[1024];

            File.WriteAllBytes(Path.Combine(_tempDir, "large_file.dat"), largeData);
            File.WriteAllBytes(Path.Combine(_tempDir, "small_file.dat"), smallData);
        }

        [Fact]
        public void GetFilesAboveSize_FiltersCorrectly()
        {
            // Act: Filter for files larger than 500 KB
            long minSize = 500 * 1024;
            var results = StorageScanner.GetFilesAboveSize(_tempDir, minSize).ToList();

            // Assert
            Assert.Single(results); // Should only find one file
            Assert.EndsWith("large_file.dat", results[0].Path);
        }

        public void Dispose()
        {
            if (Directory.Exists(_tempDir))
            {
                Directory.Delete(_tempDir, true);
            }
        }
    }
}