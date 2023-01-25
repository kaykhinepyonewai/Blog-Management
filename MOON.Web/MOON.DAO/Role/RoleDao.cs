using MOON.DAO.Common;
using System.Data;

namespace MOON.DAO.Role
{
    /// <summary>
    /// Defines the <see cref="RoleDao" />.
    /// </summary>
    public class RoleDao
    {
        /// <summary>
        /// Defines Database Connection..
        /// </summary>
        DbConnection connection = new DbConnection();
        /// <summary>
        /// Defines strSql..
        /// </summary>
        private string strSql = string.Empty;


        /// <summary>
        /// Get All
        /// </summary>
        public DataTable GetAll()
        {
            strSql = "SELECT * FROM Roles ";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetSpecific(string column, string value)
        {
            strSql = "SELECT * FROM Roles WHERE " + column + " =  '" + value + "' ";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

    }
}
