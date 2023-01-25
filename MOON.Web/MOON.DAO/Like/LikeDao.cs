using MOON.Entities.Like;
using System;
using System.Data.SqlClient;
using System.Data;
using MOON.DAO.Common;

namespace MOON.DAO.Like
{
    public class LikeDao
    {


        private DbConnection connection = new DbConnection();

        private string strSql = String.Empty;


        public bool Insert(LikeEntity likeEntity)
        {

            strSql = "INSERT INTO Likes(UserId,ArticleId,CreatedAt) " + "VALUES (@UserId,@ArticleId,@CreatedAt)";
            SqlParameter[] sqlPara =
            {
                new SqlParameter("@UserId",likeEntity.UserId),
                new SqlParameter("@ArticleId",likeEntity.ArticleId),
                new SqlParameter("@CreatedAt",DateTime.Now.ToString()),
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        public int CountLike(int id)
        {
            strSql = "SELECT COUNT(*) FROM Likes WHERE ArticleId=@ArticleId";
            SqlParameter[] sqlPara =
           {
                 new SqlParameter("@ArticleId",id),
            };

            int sucess = Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlPara));

            return sucess;
        }

        public int CountButton(int id,int userId)
        {
            strSql = "SELECT COUNT(*) FROM Likes WHERE ArticleId=@ArticleId and UserId=@UserId";
            SqlParameter[] sqlPara =
           {
                 new SqlParameter("@ArticleId",id),
                 new SqlParameter("@UserId",userId),
            };

            int sucess = Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlPara));

            return sucess;
        }

        public DataTable ReactionViewers(int id)
        {
            strSql = "SELECT Users.Profile, Users.Username FROM Likes INNER JOIN Users ON Likes.UserId = Users.UserId WHERE Likes.ArticleId = " + id;
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public bool Delete(LikeEntity likeEntity)
        {
            strSql = "Delete From Likes Where ArticleId=@ArticleId and UserId=@UserId";

            SqlParameter[] sqlPara =
            {
                new SqlParameter("@ArticleId",likeEntity.ArticleId),
                new SqlParameter("@UserId",likeEntity.UserId)
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        public bool DeleteSpecificArticle(int id)
        {
            strSql = "DELETE FROM Likes WHERE ArticleId = @ArticleId";
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@ArticleId",id),
            };
            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlParam);
            return success;
        }

    }
}
