using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager
{
    internal record class RecursiveFolderCopy(string folderPath)
    {
        DirectoryInfo sourceInf = new DirectoryInfo(folderPath);
         
        public string CopyTo(string DestinationPath, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(folderPath);

            if (!sourceInf.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {sourceInf.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = sourceInf.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(DestinationPath + sourceInf.Name);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(DestinationPath, file.Name);
                file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(DestinationPath, subDir.Name);
                    CopyTo(subDir.FullName, true);
                }
            }

            return Path.Combine(DestinationPath, Path.GetFileName(folderPath));
        }
    }
}
