using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        /// <param name="fullPath">The full path of the item</param>
        /// <param name="type">The type of the item</param>
        public DirectoryItemViewModel(string name, string id, DirectoryItemType type)
        {
            this.ExpandCommand = new RelayCommand(Expand);

            this.Name = name;
            this.Id = id;
            this.Type = type;

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
        /// A list of item's children
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

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
                                children.Select(content => new DirectoryItemViewModel(content.Name, content.Id, content.Type)));

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
