namespace GoogleDriveTreeView
{
    /// <summary>
    /// Helper class to help classify if item is a drive, a file or a folder
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryItem() { }

        /// <summary>
        /// Constructor with essential arguments
        /// </summary>
        /// <param name="name">The name of the item</param>
        /// <param name="id">The id of the item</param>
        /// <param name="type">The type of the item</param>
        public DirectoryItem(string name, string id, DirectoryItemType type)
        {
            Name = name;
            Id = id;
            Type = type;
        }

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