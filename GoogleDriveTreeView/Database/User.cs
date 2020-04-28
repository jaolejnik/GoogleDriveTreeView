using System.ComponentModel.DataAnnotations;

namespace GoogleDriveTreeView
{
    /// <summary>
    /// Class that stores user data
    /// </summary>
    public class User
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="username">User's username</param>
        /// <param name="password">User's password</param>
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Argumentless constructor 
        /// </summary>
        public User() { }

        #endregion

        #region Public properties
        /// <summary>
        /// User's username (marked as PrimaryKey)
        /// </summary>
        [Key]
        public string Username { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public string Password { get; set; }

        #endregion 
    }
}
