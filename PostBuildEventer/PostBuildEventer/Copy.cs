using System;
using System.IO;

namespace PostBuildEventer
{
    static class Copy
    {
        public static void CopyFileFolder(string sources, string destinations, bool overwrite, Action<string, string, bool> action)
        {
            action(sources, destinations, overwrite);
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (true == copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private static void FileCopy(string sourceFile, string targetPath, bool overwrite)
        {
            // Create a new target folder, if necessary.
            if (false == System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            FileInfo fileInfo = new FileInfo(sourceFile);
            string destFile = System.IO.Path.Combine(targetPath, fileInfo.Name);

            System.IO.File.Copy(sourceFile, destFile, overwrite);
        }
    }
}
