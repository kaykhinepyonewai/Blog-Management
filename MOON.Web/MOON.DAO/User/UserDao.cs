using MOON.Entities.User;
using System;
using System.Data.SqlClient;
using System.Data;
using MOON.DAO.Common;

namespace MOON.DAO.User
{
    /// <summary>
    /// Defines the <see cref="UserDao" />.
    /// </summary>
    public class UserDao
    {
        /// <summary>
        /// Defines Database Connection..
        /// </summary>
        DbConnection connection = new DbConnection();
        /// <summary>
        /// Defines strSql..
        /// </summary>
        private string strSql = string.Empty;

        #region==========User========== 

        /// <summary>
        /// Count All
        /// </summary>
        public DataTable CountAll()
        {
            strSql = "SELECT * FROM Users ";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        /// <summary>
        /// Get User by id
        /// </summary>
        /// <param name="id">.</param>
        public DataTable GetId(int id)
        {
            strSql = "SELECT * FROM Users WHERE UserId = " + id;
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        public DataTable GetUsers()
        {
            strSql = "SELECT UserId,Username,Email,Address,Mobile,Gender,Roles.Role ";
            strSql += " FROM Users INNER JOIN Roles ON Users.RoleId = Roles.RoleId";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        /// <summary>
        /// Get export users
        /// </summary>
        public DataTable GetExports()
        {
            strSql = "SELECT Username,Email,Address,Mobile,Gender,Roles.Role ";
            strSql += " FROM Users INNER JOIN Roles ON Users.RoleId = Roles.RoleId";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        /// <summary>
        /// Get Search All Users
        /// </summary>
        public DataTable GetSearchUsers(string keyword)
        {
            strSql = "SELECT UserId,Username,Email,Address,Mobile,Gender,Roles.Role ";
            strSql += " FROM Users INNER JOIN Roles ON Users.RoleId = Roles.RoleId ";
            strSql += " WHERE Users.Username LIKE '%" + keyword + "%' OR Users.Email LIKE '%" + keyword + "%'";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        /// <summary>
        /// Get User with both value and column
        /// </summary>
        /// <param name="value" name="column">.</param>
        public DataTable GetSpecific(string value, string column)
        {
            strSql = "SELECT * FROM Users WHERE " + column + " =  '" + value + "' ";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="userEntity">.</param>
        public bool LoginInsert(UserEntity userEntity)
        {
            strSql = "INSERT INTO Users (RoleId,Username,Email,Password,ConfirmPassword,CreatedAt,UpdatedAt) " + "VALUES (@RoleId,@Username,@Email,@Password,@ConfirmPassword,@CreatedAt,@UpdatedAt)";
            SqlParameter[] sqlParams =
            {
                new SqlParameter("@RoleId",userEntity.RoleId),
                new SqlParameter("@Username",userEntity.Username),
                new SqlParameter("@Email",userEntity.Email),
                new SqlParameter("@Password",userEntity.Password),
                new SqlParameter("@ConfirmPassword",userEntity.ConfirmPassword),
                new SqlParameter("@CreatedAt",userEntity.CreatedAt),
                new SqlParameter("@UpdatedAt",userEntity.UpdatedAt),
            };
            return connection.ExecuteNonQuery(CommandType.Text, strSql, sqlParams);
        }

        /// <summary>
        /// Create User by excel
        /// </summary>
        /// <param name="userEntity">.</param>
        public bool LoginImport(UserEntity userEntity)
        {
            strSql = "INSERT INTO Users (Username,Email,Address,Mobile,Gender,RoleId,Password,ConfirmPassword,CreatedAt,UpdatedAt) " + "VALUES (@Username,@Email,@Address,@Mobile,@Gender,@RoleId,@Password,@ConfirmPassword,@CreatedAt,@UpdatedAt)";
            SqlParameter[] sqlParams =
            {

                new SqlParameter("@Username",userEntity.Username),
                new SqlParameter("@Email",userEntity.Email),
                 new SqlParameter("@Address",userEntity.Address),
                 new SqlParameter("@Mobile",userEntity.Mobile),
                 new SqlParameter("@Gender",userEntity.Gender),
                 new SqlParameter("@RoleId",userEntity.RoleId),
                new SqlParameter("@Password",userEntity.Password),
                new SqlParameter("@ConfirmPassword",userEntity.ConfirmPassword),
                new SqlParameter("@CreatedAt",userEntity.CreatedAt),
                new SqlParameter("@UpdatedAt",userEntity.UpdatedAt),
            };
            return connection.ExecuteNonQuery(CommandType.Text, strSql, sqlParams);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="userEntity">.</param>
        public bool Update(UserEntity userEntity)
        {
            strSql = "UPDATE Users SET RoleId=@RoleId, Username=@Username, FirstName=@FirstName, SecondName=@SecondName , Email=@Email, ";
            strSql += "Password=@Password, ConfirmPassword=@ConfirmPassword, Mobile=@Mobile, Address=@Address, Profile=@Profile, Gender=@Gender, CreatedAt=@CreatedAt, UpdatedAt=@UpdatedAt WHERE UserId = @UserId";
            SqlParameter[] sqlParams =
            {
                new SqlParameter("@UserId",userEntity.UserId),
                new SqlParameter("@RoleId",userEntity.RoleId),
                new SqlParameter("@Username",userEntity.Username),
                new SqlParameter("@FirstName",userEntity.FirstName),
                new SqlParameter("@SecondName",userEntity.SecondName),
                new SqlParameter("@Address",userEntity.Address),
                new SqlParameter("@Mobile",userEntity.Mobile),
                new SqlParameter("@Profile",userEntity.Profile),
                new SqlParameter("@Gender",userEntity.Gender),
                new SqlParameter("@Email",userEntity.Email),
                new SqlParameter("@Password",userEntity.Password),
                new SqlParameter("@ConfirmPassword",userEntity.ConfirmPassword),
                new SqlParameter("@CreatedAt",userEntity.CreatedAt),
                new SqlParameter("@UpdatedAt",userEntity.UpdatedAt),
            };
            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlParams);
            return success;
        }

        /// <summary>
        /// Exist User
        /// </summary>
        /// <param name="userEntity">.</param>
        public int Exist(UserEntity userEntity)
        {
            strSql = "SELECT COUNT(Username) FROM Users WHERE Username = " + "@Username";
            SqlParameter[] sqlParams =
            {
                new SqlParameter("@Username",userEntity.Username),
            };
            return Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlParams));
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id">.</param>
        public bool Delete(int id)
        {
            strSql = "DELETE FROM Users WHERE UserId = @UserId";
            SqlParameter[] sqlParam = {
                                        new SqlParameter("@UserId", id)
                                      };
            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlParam);
            return success;
        }


        public DataTable GetRoleId(int id)
        {
            strSql = "SELECT * FROM Users,Roles WHERE Users.RoleId=Roles.RoleId and UserId=" + id;
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        #endregion
    }
}
