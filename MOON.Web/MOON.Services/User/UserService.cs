using MOON.DAO.User;
using MOON.Entities.User;
using System.Data;

namespace MOON.Services.User
{
    /// <summary>
    /// Defines the <see cref="UserService" />.
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// Define User Dao..
        /// </summary>
        UserDao userDao = new UserDao();

        /// <summary>
        /// Save User.
        /// </summary>
        /// <param name="userEntity">.</param>
        public bool Insert(UserEntity userEntity)
        {
            return userDao.LoginInsert(userEntity);
        }

        /// <summary>
        /// Save User from excepl import.
        /// </summary>
        /// <param name="userEntity">.</param>
        public bool InsertImport(UserEntity userEntity)
        {
            return userDao.LoginImport(userEntity);
        }

        /// <summary>
        /// Update User.
        /// </summary>
        /// <param name="userEntity">.</param>
        public bool Update(UserEntity userEntity)
        {
            return userDao.Update(userEntity);
        }

        /// <summary>
        /// Check User.
        /// </summary>
        /// <param name="userEntity">.</param>
        public int Exist(UserEntity userEntity)
        {
            return userDao.Exist(userEntity);
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="id">.</param>
        /// <returns>.</returns>
        public DataTable GetId(int id)
        {
            return userDao.GetId(id);
        }

        /// <summary>
        /// Get Users.
        /// </summary>
        public DataTable GetUsers()
        {
            return userDao.GetUsers();
        }

        /// <summary>
        /// Get export users.
        /// </summary>
        public DataTable GetExports()
        {
            return userDao.GetExports();
        }

        /// <summary>
        /// Get search users.
        /// </summary>
        public DataTable GetSearchUsers(string keyword)
        {
            return userDao.GetSearchUsers(keyword);
        }

        /// <summary>
        /// Get Specific User.
        /// </summary>
        ///  <param name="value" name="column">.</param>
        public DataTable GetSpecific(string value, string column)
        {
            return userDao.GetSpecific(value, column);
        }

        /// <summary>
        /// Count Users.
        /// </summary>
        public DataTable CountAll()
        {
            return userDao.CountAll();
        }

        /// <summary>
        /// Delete User.
        /// </summary>
        /// <param name="id">.</param>
        public bool Delete(int id)
        {
            return userDao.Delete(id);
        }

        public DataTable GetRoleId(int id)
        {
            return userDao.GetRoleId(id);
        }

    }
}
