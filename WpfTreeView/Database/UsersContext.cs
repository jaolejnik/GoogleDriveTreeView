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
        /// <summary>
        /// Default constructor
        /// </summary>
        public UsersContext() : base()
        {

        }

        /// <summary>
        /// Set of Users 
        /// </summary>
        public DbSet<User> Users { set; get; }

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

        /// <summary>
        /// Adds user to the database.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public bool AddUser(User newUser)
        {
           
            if (newUser.Username != "" || newUser.Password != "")
            {
                this.Users.Add(newUser);
                this.SaveChanges();
                return true;
            }
            else
                return false;


        }

        
    }
}
