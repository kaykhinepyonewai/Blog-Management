using MOON.Entities.Report;
using System;
using System.Data.SqlClient;
using System.Data;
using MOON.DAO.Common;

namespace MOON.DAO.Report
{
    public class ReportDao
    {
        private DbConnection connection = new DbConnection();

        private string strSql = String.Empty;

        public DataTable GetAll()
        {
            strSql = "SELECT * FROM Reports";

            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable Get(int id)
        {
            strSql = "SELECT * FROM Reports" +
                      " WHERE  ReportId= " + id;

            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public bool Insert(ReportEntity reportEntity)
        {
            strSql = "INSERT INTO Reports (Message,CreatedAt)" +
                     "VALUES(@Message, @CreatedAt)";

            SqlParameter[] sqlParam = {
                                        new SqlParameter("@Message", reportEntity.Message),
                                        new SqlParameter("@CreatedAt", reportEntity.CreatedAt)
                                      };
            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlParam);

            return success;
        }


        public bool Update(ReportEntity reportEntity)
        {
            strSql = "UPDATE Reports SET Message=@Message,CreatedAt=@CreatedAt WHERE ReportId = @ReportId";

            SqlParameter[] sqlParam = {
                                        new SqlParameter("@ReportId", reportEntity.ReportId),
                                        new SqlParameter("@Message", reportEntity.Message),
                                        new SqlParameter("@CreatedAt", reportEntity.CreatedAt)
                                      };
            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlParam);

            return success;
        }

        public bool Delete(int id)
        {
            strSql = "DELETE FROM Reports WHERE ReportId = @ReportId";
            SqlParameter[] sqlParam = {
                                        new SqlParameter("@ReportId", id)
                                      };
            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlParam);
            return success;
        }
        public int Exist(ReportEntity reportEntity)
        {
            strSql = "SELECT COUNT(Message) FROM Reports " +
                     "WHERE Message = " + "@Message";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@Message",reportEntity.Message),
            };
            return Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlParameters));
        }

    }
}

