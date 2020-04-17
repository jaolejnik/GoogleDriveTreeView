namespace GoogleDriveTreeView
{
    /// <summary>
    /// Helper class to help classify if item is a drive, a file or a folder
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The id of the item
        /// </summary>
        public string Id { get; set; }
    }
}