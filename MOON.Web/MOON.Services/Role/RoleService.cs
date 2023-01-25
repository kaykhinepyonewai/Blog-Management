using MOON.DAO.Role;
using System.Data;

namespace MOON.Services.Role
{
    /// <summary>
    /// Defines the <see cref="RoleService" />.
    /// </summary>
    public class RoleService
    {
        /// <summary>
        /// Define Role Dao.
        /// </summary>
        RoleDao roleDao = new RoleDao();

        /// <summary>
        /// Get All Roles.
        /// </summary>
        public DataTable GetAll()
        {
            return roleDao.GetAll();
        }

        public DataTable GetSpecific(string column, string value)
        {
            return roleDao.GetSpecific(column, value);
        }
    }
}
