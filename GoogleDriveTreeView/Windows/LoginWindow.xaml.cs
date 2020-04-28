using System.Windows;


namespace GoogleDriveTreeView
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml    
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginWindow()
        {
            usersDb = new UsersContext();
            InitializeComponent();
        }

        /// <summary>
        /// The database instance containing users. 
        /// </summary>
        private UsersContext usersDb;

        /// <summary>
        /// Click action handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if ( usersDb.ValidCredentials(txtUsername.Text, txtPassword.Password) )
            {
                DriveWindow drive = new DriveWindow(txtUsername.Text);
                drive.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }
    }
}
