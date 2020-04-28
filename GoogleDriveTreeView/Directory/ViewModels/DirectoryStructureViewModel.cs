using System.Collections.ObjectModel;
using System.ComponentModel;
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
        /// A list of all directories on the drive
        /// </summary>
        public DirectoryStructureViewModel()
        {
            var children = DirectoryStructure.GetDrives();
            this.Items = new ObservableCollection<DirectoryItemViewModel>(
                             children.Select(drive => new DirectoryItemViewModel(null, drive.Name, drive.Id, DirectoryItemType.Drive)));
            ActionType = new ObservableCollection<DirectoryItemActionType>();
            ActionType.Add(DirectoryItemActionType.None);
        }

        public string CurrentUser { get; set; }

        public static ObservableCollection<DirectoryItemActionType> ActionType { get; set; }



    }
}
