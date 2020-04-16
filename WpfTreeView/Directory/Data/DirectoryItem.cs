namespace GoogleDriveTreeView
{
    /// <summary>
    /// Helper class to help classify if item is a drive, a file or a folder
    /// </summary>
    public class DirectoryItem
    {   
        /// <summary>
        /// Type of the item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The full path of the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }
    }
}