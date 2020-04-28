using System.Windows;


namespace GoogleDriveTreeView
{
    /// <summary>
    /// Interaction logic for DriveWindow.xaml.
    /// A window with main functionality of the app. 
    /// </summary>
    public partial class DriveWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Defualt constructor
        /// </summary>
        public DriveWindow(string username)
        {
            InitializeComponent();
            var dirVM = new DirectoryStructureViewModel();
            dirVM.CurrentUser = username;
            this.DataContext = dirVM;
        }
        #endregion

        /// <summary>
        /// Click action handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewUser_Click(object sender, RoutedEventArgs e)
        {
            NewUserWindow addNewUser = new NewUserWindow();
            addNewUser.ShowDialog();
        }

    }
}
