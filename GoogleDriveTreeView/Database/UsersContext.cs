using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDriveTreeView
{
    /// <summary>
    /// Class for better User's database managemanet  
    /// </summary>
    public class UsersContext:DbContext 
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UsersContext() : base()
        {

        }

        #endregion

        #region Database set

        /// <summary>
        /// Set of Users 
        /// </summary>
        public DbSet<User> Users { set; get; }

        #endregion

        #region Helper functions

        /// <summary>
        /// Checks whether given credentials are valid. 
        /// </summary>
        /// <param name="username">The given username</param>
        /// <param name="password">The given password</param>
        /// <returns></returns>
        public bool ValidCredentials (string username, string password)
        {
            var query = from user in Users
                        where user.Username == username && user.Password == password
                        select user;
            if (query.Count() == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks whether a user with the given username exists.
        /// </summary>
        /// <param name="username">The given username</param>
        /// <returns></returns>
        public bool UserExists(string username)
        {
            var query = from user in Users
                        where user.Username == username
                        select user;
            if (query.Count() == 1)
                return true;
            else
                return false;
        }

        #endregion
    }
}
