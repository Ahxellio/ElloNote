using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElloNote.Infrastructure
{
    internal static class ExplorerHelper
    {
        /// <summary>
        /// Checks if the path is a file
        /// </summary>
        public static bool IsFile(this string path)
        {
            return !string.IsNullOrEmpty(path) && File.Exists(path);
        }

        /// <summary>
        /// Checks if a path is a directory
        /// </summary>
        public static bool IsDirectory(this string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        }

        /// <summary>
        /// Checks if a path is a drive
        /// </summary>
        public static bool IsDrive(this string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        }

        /// <summary>
        /// Gets the name of a file within a path
        /// </summary>
        public static string GetFileName(this string fullpath)
        {
            return Path.GetFileName(fullpath);
        }

        /// <summary>
        /// Returns the directory path of the directory a file is located in
        /// (e.g, C:\f1\fold2\f3\hello.txt, returns C:\f1\fold2\f3)
        /// </summary>
        public static string GetParentDirectory(this string fullpath)
        {
            return Path.GetFileName(fullpath);
        }

        /// <summary>
        /// Returns the name of the directory/folder
        /// </summary>
        public static string GetDirectoryName(this string path)
        {
            return path.IsDirectory() ? new DirectoryInfo(path).Name : "";
        }

    }
}
