using System;
using System.Collections.Generic;

namespace GoogleDriveTreeView
{
    /// <summary>
    /// A helper class for better directory information retrieval. 
    /// </summary>
    public static class DirectoryStructure
    {
        public static GoogleDriveAPI DriveAPI = new GoogleDriveAPI();
        
        /// <summary>
        /// Gets all logical drive on the computer
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetDrives()
        {
            var root = DriveAPI.GetRootId();
            List<DirectoryItem> drives = new List<DirectoryItem>();
            drives.Add(new DirectoryItem
            {
                Name = root.Name,
                Id = root.Id,
                Type = DirectoryItemType.Drive
            });

            return drives;
        }

        /// <summary>
        /// Gets the top level of directories content
        /// </summary>
        /// <param name="parentId">The id of the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string parentId)
        {
            var items = new List<DirectoryItem>();

            #region Get Folders
            try
            {
                var dirs = DriveAPI.GetDirectories(parentId);

                foreach (var folder in dirs.Files)
                {
                    var newItem = new DirectoryItem()
                    {
                        Name = folder.Name,
                        Id = folder.Id,
                        Type = DirectoryItemType.Folder
                    };
                    items.Add(newItem);
                }
            }
            catch { }
            #endregion

            #region Get files

            try
            {
                var fs = DriveAPI.GetFiles(parentId);

                foreach (var file in fs.Files)
                {
                    var newItem = new DirectoryItem()
                    {
                        Name = file.Name,
                        Id = file.Id,
                        Type = DirectoryItemType.File
                    };
                    items.Add(newItem);
                }
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