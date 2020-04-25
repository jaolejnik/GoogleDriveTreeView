﻿using System.Windows;

namespace GoogleDriveTreeView
{
    /// <summary>
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public NewUserWindow()
        {
            usersDb = new UsersContext();
            InitializeComponent();
        }

        /// <summary>
        /// The database instance containing users. 
        /// </summary>
        private UsersContext usersDb;

        /// <summary>
        /// New user's username
        /// </summary>
        public string Username { get { return txtUsername.Text; } }

        /// <summary>
        /// New user's password
        /// </summary>
        public string Password { get { return txtPassword.Password; } }

        /// <summary>
        /// Click action handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string message = "";

            if (!usersDb.UserExists(txtUsername.Text))
                usersDb.AddUser(new User(txtUsername.Text, txtPassword.Password));
            else
                message += "* A user with the given username already exists!\n\n";

            if (txtPassword.Password != txtPasswordConfirm.Password)
                message += "* Passwords don't match!\n\n";


            if (message != "")
                MessageBox.Show(message);
            else
                this.DialogResult = true;
        }

        
    }
}
