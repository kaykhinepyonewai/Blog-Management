using MOON.DAO.Common;
using System.Data.SqlClient;
using System.Data;
using MOON.Entities.Comment;

namespace MOON.DAO.Comment
{
    public class CommentDao
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
        /// Create User
        /// </summary>
        /// <param name="commentEntity">.</param>
        public bool Insert(CommentEntity commentEntity)
        {
            strSql = "INSERT INTO Comments (UserId,ArticleId,Message,CreatedAt,UpdatedAt) " + "VALUES (@UserId,@ArticleId,@Message,@CreatedAt,@UpdatedAt)";
            SqlParameter[] sqlParams =
            {
                new SqlParameter("@UserId",commentEntity.UserId),
                new SqlParameter("@ArticleId",commentEntity.ArticleId),
                new SqlParameter("@Message",commentEntity.Message),
                new SqlParameter("@CreatedAt",commentEntity.CreatedAt),
                new SqlParameter("@UpdatedAt",commentEntity.UpdatedAt),
            };
            return connection.ExecuteNonQuery(CommandType.Text, strSql, sqlParams);
        }

        public DataTable GetSpecific(string column, int id)
        {
            strSql = "SELECT Users.Username, Users.Profile, Comments.Message, Comments.CreatedAt FROM Comments ";
            strSql += " INNER JOIN Users ON Comments.UserId = Users.UserId WHERE " + column + " = " + id + " ORDER BY Comments.CommentId DESC";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public bool Update(CommentEntity commentEntity)
        {
            strSql = "UPDATE Comments SET Message = @Message WHERE CommentId = @CommentId";
            SqlParameter[] sqlParams =
            {
                new SqlParameter("@CommentId",commentEntity.CommentId),
                new SqlParameter("@Message",commentEntity.Message),
            };
            return connection.ExecuteNonQuery(CommandType.Text, strSql, sqlParams);
        }

        public DataTable GetComments(int id)
        {
            strSql = "SELECT Articles.ArticleId, Comments.CommentId, Users.Username ,Articles.Title, Comments.Message";
            strSql += " FROM Comments INNER JOIN Articles ON Comments.ArticleId = Articles.ArticleId INNER JOIN ";
            strSql += " Users ON Comments.UserId = Users.UserId WHERE Articles.UserId = " + id;
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetCommentsBySearch(int id, string keyword)
        {
            strSql = "SELECT Articles.ArticleId, Comments.CommentId, Users.Username ,Articles.Title, Comments.Message";
            strSql += " FROM Comments INNER JOIN Articles ON Comments.ArticleId = Articles.ArticleId INNER JOIN ";
            strSql += " Users ON Comments.UserId = Users.UserId WHERE Articles.UserId = " + id + "";
            strSql += " AND (Articles.Title LIKE '%" + keyword + "%' OR Users.Username LIKE '%" + keyword + "%' )";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public bool DeleteSpecificArticle(int id)
        {
            strSql = "DELETE FROM Comments WHERE ArticleId = @ArticleId";
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@ArticleId",id),
            };
            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlParam);
            return success;
        }

        public bool DeleteComment(int articleid, int commentid)
        {
            strSql = "DELETE FROM Comments WHERE CommentId = @CommentId AND ArticleId = @ArticleId";
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@ArticleId",articleid),
                new SqlParameter("@CommentId",commentid)
            };
            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlParam);
            return success;
        }
    }
}
