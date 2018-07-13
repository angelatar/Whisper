using System;
using System.Collections.Generic;
using System.Linq;
using UserDAL;

namespace UserBL
{
    /// <summary>
    /// Repository for Users
    /// </summary>
    public class UsersRepository
    {
        /// <summary>
        /// Dal functionality for Users
        /// </summary>
        private readonly DataAccessor Functionality;

        /// <summary>
        /// Initializer for Dal functionality
        /// </summary>
        public UsersRepository()
        {
            this.Functionality = new DataAccessor();
        }

        #region getters

        /// <summary>
        /// Gets all users from storage
        /// </summary>
        /// <returns>Users collection</returns>
        public IEnumerable<User> GetUsers()
        {
            return this.Functionality.GetAllUsers().Select(Mapper.CreateUser);
        }

        /// <summary>
        /// Gets the user by given id
        /// </summary>
        /// <returns>User</returns>
        public User GetUserByID(int id)
        {
            return Mapper.CreateUser(this.Functionality.GetUserByID(id));
        }

        /// <summary>
        /// Gets the user by given username
        /// </summary>
        /// <returns>User</returns>
        public User GetUserByUsername(string username)
        {
            return Mapper.CreateUser(this.Functionality.GetUserByUsername(username));
        }

        /// <summary>
        /// Gets the Email of given user
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>Email</returns>
        public string GetEmail(int id)
        {
            return this.Functionality.GetEmail(id);
        }

        /// <summary>
        /// Gets the Last login of given user
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>Last login</returns>
        public DateTime GetLastLogin(int id)
        {
            return this.Functionality.GetLastLogin(id);
        }

        #endregion

        #region Creator

        /// <summary>
        /// Creates new user in storage
        /// </summary>
        /// <param name="user">New user</param>
        /// <returns>if user is created retruns true, else false</returns>
        public bool CreateUser(User user)
        {
            return this.Functionality.CreateUser(Mapper.SplitUser(user));
        }

        #endregion

        #region deletor

        /// <summary>
        /// Deletes the specific user from storage
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>if user is deleted returns true, else false</returns>
        public bool DeleteUser(int id)
        {
            return this.Functionality.DeleteUser(id);
        }

        #endregion

        #region changers

        /// <summary>
        /// Updates the specific user's password in storage
        /// </summary>
        /// <param name="id">User's id</param>
        /// <param name="passwordhash">new password</param>
        /// <returns>if user's password is updated returns true, else false</returns>
        public bool UpdateUserPassword(int id, string passwordhash)
        {
            return this.Functionality.UpdateUserPassword(id, passwordhash);
        }

        /// <summary>
        /// Updates the specific user's last login date in storage
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>if user's last login date is updated returns true, else false</returns>
        public bool UpdateUserLastLogin(int id)
        {
            return this.Functionality.UpdateUserLastLogin(id);
        }

        #endregion
    }
}
