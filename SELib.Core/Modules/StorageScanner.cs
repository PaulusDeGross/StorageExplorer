using System;
using System.Collections.Generic;
using System.IO;

namespace SELib.Core.Modules
{
    public record FileItem(string Path, long SizeBytes);

    public class StorageScanner
    {
        public static IEnumerable<FileItem> GetFilesAboveSize(string rootPath, long minSizeBytes)
        {
            var dirInfo = new DirectoryInfo(rootPath);
            var opts = new EnumerationOptions
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = true,
            };

            IEnumerable<FileInfo> files;
            try
            {
                files = dirInfo.EnumerateFiles("*.*", opts);
            }
            catch (Exception)
            {
                yield break;    // If root directory is denied, yield nothing
            }

            foreach (var file in files)
            {
                if (file.Length >= minSizeBytes)
                {
                    yield return new FileItem(file.FullName, file.Length);
                }
            }
        }
    }
}
