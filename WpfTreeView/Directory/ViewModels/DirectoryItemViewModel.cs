using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace GoogleDriveTreeView
{
    /// <summary>
    /// A view model for every directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">The full path of the item</param>
        /// <param name="id">The id of the item</param>
        /// <param name="type">The type of the item</param>
        public DirectoryItemViewModel(DirectoryItemViewModel parent, string name, string id, DirectoryItemType type)
        {
            this.ExpandCommand = new RelayCommand(Expand);

            this.Parent = parent;
            this.Name = name;
            this.Id = id;
            this.Type = type;

            ContextMenuItems = new List<ContextMenuItem>();

            if (this.Type != DirectoryItemType.File)
            {
                ContextMenuItems.Add(new ContextMenuItem()
                {
                    ItemHeader = "Upload a new file",
                    ItemAction = new RelayCommand(UploadFile)
                });
                ContextMenuItems.Add(new ContextMenuItem()
                {
                    ItemHeader = "Create a new folder",
                    ItemAction = new RelayCommand(CreateDirectory)
                });
            }

            if (this.Type != DirectoryItemType.Drive)
            {
                ContextMenuItems.Add(new ContextMenuItem()
                {
                    ItemHeader = "Download",
                    ItemAction = new RelayCommand(DownloadItem)
                });
                ContextMenuItems.Add(new ContextMenuItem()
                {
                    ItemHeader = "Delete",
                    ItemAction = new RelayCommand(DeleteItem)
                });
            }

            this.ClearChildren();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The type of the item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The full path of the item
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The extension of the item
        /// </summary>
        public string Extension { get; set; }

        private DirectoryItemViewModel Parent { get; set; }

        /// <summary>
        /// A list of item's children
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// A list of item's context menu options.
        /// </summary>
        public List<ContextMenuItem> ContextMenuItems { get; set; }

        /// <summary>
        /// Downloads the item and saves it to the chosen path.
        /// </summary>
        public void DownloadItem()
        {
            var dialog = new FolderBrowserDialog();
            dialog.Description = "Choose a location to save the file:";
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (this.Type == DirectoryItemType.File)
                    GoogleDriveAPI.DownloadFile(this.Id, this.Name, dialog.SelectedPath);

                if (this.Type == DirectoryItemType.Folder)
                    GoogleDriveAPI.DownloadDirectory(this.Id, this.Name, dialog.SelectedPath);
            }

        }

        /// <summary>
        /// Deletes the item
        /// </summary>
        public void DeleteItem()
        {
            GoogleDriveAPI.Delete(this.Id);
            Parent.Expand();
        }

        /// <summary>
        /// Creates a new directory inside the item (if it's a drive or a directory)
        /// </summary>
        public void CreateDirectory()
        {
            GoogleDriveAPI.CreateDirectory("nowy folder", this.Id);
            Expand();
        }

        /// <summary>
        /// Uploads a file to the item (if it's a drive or a directory)
        /// </summary>
        public void UploadFile()
        {
            var dialog = new OpenFileDialog();
            dialog.ShowDialog();
            
            if (dialog.FileName != "")
            {
                Console.WriteLine(dialog.FileName);
                GoogleDriveAPI.UploadFile(dialog.FileName, this.Id);
                Expand();
            }
        }


        /// <summary>
        /// Indicates if the item can be expanded
        /// </summary>
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        /// <summary>
        /// Indicates if the item is expanded or not
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }

            set
            {
                if (value == true)
                    Expand();
                else
                    this.ClearChildren();

            }
        }

        /// <summary>
        /// The command to expand the item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Helpers
        /// <summary>
        /// Expands the item and collects all children
        /// </summary>
        private void Expand()
        {
            if (this.Type == DirectoryItemType.File)
                return;

            var children = DirectoryStructure.GetDirectoryContents(this.Id);
            this.Children = new ObservableCollection<DirectoryItemViewModel>(
                                children.Select(content => new DirectoryItemViewModel(this, content.Name, content.Id, content.Type)));

        }

        /// <summary>
        /// Removes all children 
        /// </summary>
        private void ClearChildren()
        {
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }

        #endregion
    }
}
