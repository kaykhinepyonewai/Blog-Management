using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using MOON.DAO.Common;
using MOON.Entities.Dashboard;

namespace MOON.DAO.Dashboard
{
    public class PhotoDao
    {
        private DbConnection connection = new DbConnection();

        private string strSql = String.Empty;
        public bool Insert(PhotoEntity photoEntity)
        {

            strSql = "INSERT INTO Photos(ArticleId,PhotoName,CreatedAt,DeleteStatus) " + "VALUES (@ArticleId,@PhotoName,@CreatedAt,@DeleteStatus)";
            SqlParameter[] sqlPara =
            {
                new SqlParameter("@ArticleId",photoEntity.ArticleId),
                new SqlParameter("@PhotoName",photoEntity.PhotoImage),
                new SqlParameter("@CreatedAt",DateTime.Now.ToString()),
                new SqlParameter("@DeleteStatus",photoEntity.DeleteStatus),
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }


        public List<PhotoEntity> GetAllImage(int id)
        {
           
            strSql = "select * from Photos where DeleteStatus=0 and ArticleId=" + id;
            return connection.GetAllImage(CommandType.Text, strSql);
        }


        public List<PhotoEntity> GetImageBySlug(string slug)
        {

            strSql = "select PhotoName from Photos,Articles where Photos.ArticleId=Articles.ArticleId and Photos.DeleteStatus=0 and Articles.Slug= '"+slug+"' ";
            return connection.GetImageBySlug(CommandType.Text, strSql);
        }


        public List<PhotoEntity> GetArchieveImages(int id)
        {
            strSql = "select * from Photos where DeleteStatus=1 and ArticleId=" + id;
            return connection.GetAllImage(CommandType.Text, strSql);
        }

        public DataTable GetImage(int id)
        {

            strSql = "select PhotoName,PhotoId from Photos where DeleteStatus=0 and ArticleId=" + id;
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public bool Delete(int id)
        {
            strSql = "Update Photos Set DeleteStatus=1 WHERE PhotoId=@PhotoId";

            SqlParameter[] sqlPara =
            {
                new SqlParameter("@PhotoId",id)
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        public bool UnArchieve(int id)
        {
            strSql = "Update Photos Set DeleteStatus=0 WHERE ArticleId=@ArticleId";

            SqlParameter[] sqlPara =
            {
                new SqlParameter("@ArticleId",id)
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        public bool DeleteAritcle(int id)
        {
            strSql = "Update Photos Set DeleteStatus=1 WHERE ArticleId=@ArticleId";

            SqlParameter[] sqlPara =
            {
                new SqlParameter("@ArticleId",id)
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        public bool Remove(int id)
        {
            strSql = "DELETE FROM Photos WHERE DeleteStatus=1 AND ArticleId=@ArticleId";

            SqlParameter[] sqlPara =
            {
                new SqlParameter("@ArticleId",id)
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        public bool ReportPhotosRemove(int id)
        {
            strSql = "DELETE FROM Photos WHERE DeleteStatus = 0 AND ArticleId=@ArticleId";

            SqlParameter[] sqlPara =
            {
                new SqlParameter("@ArticleId",id)
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

    }
}
