﻿using System.Collections.ObjectModel;
using System.Linq;


namespace GoogleDriveTreeView
{
    /// <summary>
    /// The view model for the main directory structure view
    /// </summary>
    public class DirectoryStructureViewModel : BaseViewModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        /// <summary>
        /// A list of all directories on the computer
        /// </summary>
        public DirectoryStructureViewModel()
        {
            var children = DirectoryStructure.GetDrives();
            this.Items = new ObservableCollection<DirectoryItemViewModel>(
                             children.Select(drive => new DirectoryItemViewModel(drive.Name, drive.Id, DirectoryItemType.Drive)));
        }
    }
}
