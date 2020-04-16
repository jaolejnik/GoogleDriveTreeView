using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoogleDriveTreeView
{   
    /// <summary>
    /// A helper class for better directory information retrieval. 
    /// </summary>
    public static class DirectoryStructure
    {   
        /// <summary>
        /// Gets all logical drive on the computer
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            // Get every logical drive
            return Directory.GetLogicalDrives().Select(drive => 
                new DirectoryItem { FullPath = drive, 
                                    Type = DirectoryItemType.Drive 
                                  }).ToList();
        }

        /// <summary>
        /// Gets the top level of directories content
        /// </summary>
        /// <param name="fullPath">The full path of the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            var items = new List<DirectoryItem>();

            #region Get Folders
            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem 
                    {   FullPath = dir, 
                        Type = DirectoryItemType.Folder 
                    }));
            }
            catch { }
            #endregion

            #region Get files
            
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    items.AddRange(fs.Select(file => new DirectoryItem
                    {
                        FullPath = file,
                        Type = DirectoryItemType.File
                    }));
            }
            catch { }
            #endregion

            return items;
        }

        #region Helpers
        /// <summary>
        /// Gets the name of the folder/file frotm the full path.
        /// </summary>
        /// <param name="path">The full path of the folder/file</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            var normalizedPath = path.Replace('/', '\\');
            var lastIndex = normalizedPath.LastIndexOf('\\');

            if (lastIndex <= 0)
                return path;

            return path.Substring(lastIndex + 1);
        }
        #endregion
    }
}