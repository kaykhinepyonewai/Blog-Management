using MOON.Entities.Dashboard;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using MOON.DAO.Common;

namespace MOON.DAO.Dashboard
{
    /// <summary>
    /// Defines the <see cref="ArticleDao" />.
    /// </summary>
    public class ArticleDao
    {
        /// <summary>
        /// Defines Database Connection..
        /// </summary>
        private DbConnection connection = new DbConnection();
        /// <summary>
        /// Defines strSql..
        /// </summary>
        private string strSql = String.Empty;

        #region==========Article========== 

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="articleEntity">.</param>

        public bool Insert(ArticleEntity articleEntity)
        {

            strSql = "INSERT INTO Articles(UserId,CategoryId,Title,Slug,Description,Excerpt,Thumbnail,Status,CreatedAt,DeleteStatus) " + "VALUES (@UserId,@CategoryId,@Title,@Slug,@Description,@Excerpt,@Thumbnail,@Status,@CreatedAt,@DeleteStatus)";
            SqlParameter[] sqlPara =
            {
                new SqlParameter("@UserId",articleEntity.UserId),
                new SqlParameter("@CategoryId",articleEntity.CategroyId),
                new SqlParameter("@Title",articleEntity.Title),
                new SqlParameter("@Slug",articleEntity.Slug),
                new SqlParameter("@Description",articleEntity.Description),
                new SqlParameter("@Excerpt",articleEntity.Excerpt),
                new SqlParameter("@Thumbnail",articleEntity.Thumbnail),
               
                new SqlParameter("@Status",articleEntity.Status),
                new SqlParameter("@CreatedAt",DateTime.Now.ToString()),
                new SqlParameter("@DeleteStatus",articleEntity.DeleteStatus),
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        /// <summary>
        /// Get All Articles
        /// </summary>
        public DataTable GetAll()
        {
            strSql = "select * from Articles as Articles , Users as Users , Categories as Categories where Articles.CategoryId=Categories.CategoryId and Articles.UserId=Users.UserId and Articles.Status='active' and Articles.DeleteStatus=0 ORDER BY Articles.ArticleId DESC";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetAllBySearch(string keyword)
        {
            strSql = "SELECT * FROM Articles INNER JOIN Categories ON Articles.CategoryId=Categories.CategoryId INNER JOIN Users ON Articles.UserId=Users.UserId ";
            strSql += " AND Articles.Status='active' AND Articles.DeleteStatus=0 AND Articles.Title LIKE '%" + keyword + "%' ORDER BY Articles.Title DESC ";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetSpecific(string column, string value)
        {
            strSql = "SELECT * FROM Articles WHERE Status='active' AND DeleteStatus=0 AND " + column + " = '" + value + "'";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetRelated(int id, string slug)
        {
            strSql = "SELECT TOP 3 Articles.Thumbnail, Articles.Title, Articles.Slug, Categories.Name FROM Articles ";
            strSql += " INNER JOIN Categories ON Articles.CategoryId = Categories.CategoryId WHERE NOT Articles.Slug = '" + slug + "' AND Articles.Status='active' AND ";
            strSql += " Articles.DeleteStatus=0 AND Articles.CategoryId = " + id + " ORDER BY NEWID()";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetReports()
        {
            strSql = "SELECT Articles.ArticleId, Users.Username, Articles.Title, Reports.Message, Articles.ReportAt FROM Articles ";
            strSql += " INNER JOIN Users ON Articles.UserId = Users.UserId INNER JOIN Reports ON ";
            strSql += " Articles.ReportId = Reports.ReportId WHERE Articles.Status = 'active' AND ";
            strSql += " Articles.DeleteStatus = 0 AND Articles.ReportStatus = 1 ORDER BY Articles.ArticleId DESC";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetReportsBySearch(string keyword)
        {
            strSql = "SELECT Articles.ArticleId, Users.Username, Articles.Title, Reports.Message, Articles.ReportAt FROM Articles ";
            strSql += " INNER JOIN Users ON Articles.UserId = Users.UserId INNER JOIN Reports ON ";
            strSql += " Articles.ReportId = Reports.ReportId WHERE Articles.Status = 'active' AND ";
            strSql += " Articles.DeleteStatus = 0 AND Articles.ReportStatus = 1 AND Articles.Title LIKE '%" + keyword + "%' OR Users.Username LIKE '%" + keyword + "%' ORDER BY Articles.Title DESC";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable ShowCard ()
        {
            strSql = "Select Articles.ArticleId, Articles.Slug ,Articles.Thumbnail, Articles.Title, Articles.Excerpt, Articles.CreatedAt, Categories.Name, Users.Username ";
            strSql += " FROM Articles INNER JOIN Categories ON Articles.CategoryId = Categories.CategoryId ";
            strSql += " INNER JOIN Users ON Articles.UserId = Users.UserId WHERE Articles.Status='active' and Articles.DeleteStatus=0 ORDER BY Articles.ArticleId DESC";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable ArticleFilterByCategory(string slug)
        {
            strSql = "Select Articles.ArticleId, Articles.Slug ,Articles.Thumbnail, Articles.Title, Articles.Excerpt, Articles.CreatedAt, Categories.Name, Users.Username ";
            strSql += " FROM Articles INNER JOIN Categories ON Articles.CategoryId = Categories.CategoryId ";
            strSql += " INNER JOIN Users ON Articles.UserId = Users.UserId WHERE Articles.Status='active' AND Articles.DeleteStatus=0 AND Categories.Slug = '" + slug + "' ORDER BY Articles.Title DESC ";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable ArticleFilterBySearch(string keyword)
        {
            strSql = "Select Articles.ArticleId, Articles.Slug ,Articles.Thumbnail, Articles.Title, Articles.Excerpt, Articles.CreatedAt, Categories.Name, Users.Username ";
            strSql += " FROM Articles INNER JOIN Categories ON Articles.CategoryId = Categories.CategoryId ";
            strSql += " INNER JOIN Users ON Articles.UserId = Users.UserId WHERE  Articles.Status != 'pending' AND Articles.DeleteStatus = 0 AND Articles.Title LIKE '%" + keyword + "%'";
            strSql += " OR Articles.Description  LIKE '%" + keyword + "%' ORDER BY Articles.Title DESC";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable ArticleFilterByBoth(string keyword, string slug)
        {
            strSql = "Select Articles.ArticleId, Articles.Slug ,Articles.Thumbnail, Articles.Title, Articles.Excerpt, Articles.CreatedAt, Categories.Name, Users.Username ";
            strSql += " FROM Articles INNER JOIN Categories ON Articles.CategoryId = Categories.CategoryId ";
            strSql += " INNER JOIN Users ON Articles.UserId = Users.UserId WHERE Articles.Title LIKE '%" + keyword + "%'";
            strSql += " OR Articles.Description  LIKE '%" + keyword + "%' OR Users.Username LIKE '%" + keyword + "%' OR Categories.Name LIKE '%" + keyword + "%' ";
            strSql += " AND Articles.Status='active' AND Articles.DeleteStatus=0 AND Categories.slug = '" + slug + "' ";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }


        /// <summary>
        /// Get Email with id
        /// </summary>
        /// /// <param name="id">.</param>
        public DataTable GetEmail(int id)
        {
            strSql = "select Email from Articles as Articles , Users as Users , Categories as Categories where Articles.CategoryId=Categories.CategoryId and Articles.UserId=Users.UserId and Status='pending' and Articles.DeleteStatus=0 and Articles.ArticleId="+id;
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        /// <summary>
        /// Get Thumbnail with id
        /// </summary>
        /// /// <param name="id">.</param>

        public DataTable GetThumbnail(int id)
        {
            strSql = "select Thumbnail from Articles as Articles , Users as Users , Categories as Categories where Articles.CategoryId=Categories.CategoryId and Articles.UserId=Users.UserId and Status='pending' and Articles.DeleteStatus=0 and Articles.ArticleId="+id;
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetArticleByCategory(int id)
        {
            strSql = "select * from Articles where Articles.CategoryId= " + id;
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetArticleByUser(int id)
        {
            strSql = "select * from Articles where Articles.UserId= " + id;
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetArticleByReport(int id)
        {
            strSql = "select * from Articles where Articles.ReportId= " + id;
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        /// <summary>
        /// Get All User with id
        /// </summary>
        /// /// <param name="user">.</param>
        public DataTable UserGetAll(int user)
        {
            strSql = "select * from Articles as Articles , Users as Users , Categories as Categories where Articles.CategoryId=Categories.CategoryId and Articles.UserId=Users.UserId and Status='active' and Articles.DeleteStatus=0 and Articles.UserId=" + user;
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable UserGetAllBySearch(int user, string keyword)
        {
            strSql = "SELECT * FROM Articles INNER JOIN Categories ON Articles.CategoryId=Categories.CategoryId INNER JOIN Users ON Articles.UserId = Users.UserId WHERE Articles.Status='active' AND Articles.DeleteStatus=0 ";
            strSql += "  AND Articles.UserId=" + user + " AND Users.UserId = " + user + " AND Articles.Title LIKE '%" + keyword + "%'";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }
             
        /// <summary>
        /// Get Waiting Article with id
        /// </summary>
        /// /// <param name="user">.</param>
        public DataTable GetWaiting(int user)
        {
            strSql = "select * from Articles as Articles , Users as Users , Categories as Categories where Articles.CategoryId=Categories.CategoryId and Articles.UserId=Users.UserId and Status='pending' and Articles.DeleteStatus=0 and Articles.UserId=" + user + " ";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetWaitingBySearch(int user, string keyword)
        {
            strSql = "SELECT * FROM Articles INNER JOIN Categories ON Articles.CategoryId=Categories.CategoryId INNER JOIN Users ON Articles.UserId=Users.UserId WHERE Articles.Status='pending' and Articles.DeleteStatus=0 and Articles.UserId=" + user + " ";
            strSql += " AND Articles.Title LIKE '%" + keyword + "%'";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        /// <summary>
        /// Get All Pending Article
        /// </summary>
        public DataTable GetAllPending()
        {

            strSql = "select * from Articles as Articles , Users as Users , Categories as Categories where Articles.CategoryId=Categories.CategoryId and Articles.UserId=Users.UserId and Articles.DeleteStatus=0 and Status='pending' ORDER BY Articles.ArticleId DESC";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetAllPendingBySearch(string keyword)
        {
            strSql = "SELECT * FROM Articles INNER JOIN Categories ON Articles.CategoryId=Categories.CategoryId INNER JOIN Users ON Articles.UserId=Users.UserId WHERE Articles.DeleteStatus=0 and Articles.Status='pending'";
            strSql += " AND Articles.Title LIKE '%" + keyword + "%' OR Users.Username LIKE '%" + keyword + "%' ORDER BY Articles.Title DESC ";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        /// <summary>
        /// Get Archieve Article with id
        /// </summary>
        /// /// <param name="id">.</param>
        public DataTable GetArchieve(int id)
        {

            strSql = "select * from Articles as Articles , Users as Users , Categories as Categories where Articles.CategoryId=Categories.CategoryId and Articles.UserId=Users.UserId and Articles.DeleteStatus=1 and Articles.Status='active' and Articles.UserId = " + id+ " ORDER BY Articles.Title DESC";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetAllArchives()
        {

            strSql = "select * from Articles as Articles , Users as Users , Categories as Categories where Articles.CategoryId=Categories.CategoryId and Articles.UserId=Users.UserId and Articles.DeleteStatus=1 and Articles.Status='active' ORDER BY Articles.Title DESC";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetArchieveBySearch(int id,string keyword)
        {

            strSql = "SELECT * FROM Articles INNER JOIN Categories ON Articles.CategoryId=Categories.CategoryId INNER JOIN Users ON Articles.UserId=Users.UserId WHERE Articles.DeleteStatus=1 AND Articles.Status='active' AND Articles.UserId = " + id + " ";
            strSql += " AND Articles.Title LIKE '%" + keyword + "%' ORDER BY Articles.Title DESC";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetAllArchivesBySearch(string keyword)
        {
            strSql = "SELECT * FROM Articles INNER JOIN Categories ON Articles.CategoryId=Categories.CategoryId INNER JOIN Users ON Articles.UserId=Users.UserId WHERE Articles.DeleteStatus=1 AND Articles.Status='active' ";
            strSql += " AND Articles.Title LIKE '%" + keyword + "%' ORDER BY Articles.Title DESC";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        /// <summary>
        /// Get Article with id
        /// </summary>
        /// /// <param name="id">.</param>
        public DataTable Get(int id)
        {

            strSql = "select * from Articles as Articles , Users as Users , Categories as Categories where Articles.ArticleId = " + id + "and Articles.CategoryId = Categories.CategoryId and Articles.UserId = Users.UserId and Articles.DeleteStatus=0";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        /// <summary>
        /// Update Article
        /// </summary>
        /// <param name="articleEntity">.</param>
        public bool Update(ArticleEntity articleEntity)
        {
            strSql = "UPDATE Articles SET Title=@Title,CategoryId=@CategoryId,Slug=@Slug,Description=@Description,Excerpt=@Excerpt,Thumbnail=@Thumbnail,Status=@Status WHERE ArticleId=@ArticleId";
            SqlParameter[] sqlPara =
            {
                 new SqlParameter("@Title",articleEntity.Title),
                new SqlParameter("@CategoryId",articleEntity.CategroyId),
                new SqlParameter("@Slug",articleEntity.Slug),
                 new SqlParameter("@Description",articleEntity.Description),
                new SqlParameter("@Excerpt",articleEntity.Excerpt),
                new SqlParameter("@Thumbnail",articleEntity.Thumbnail),
                new SqlParameter("@ArticleId",articleEntity.ArticleId),
                new SqlParameter("@Status",articleEntity.Status),
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        public bool UpdateArticleReport(ArticleEntity articleEntity)
        {
            strSql = "UPDATE Articles SET ReportId=@ReportId,ReportStatus=@ReportStatus, ReportAt = @ReportAt WHERE ArticleId=@ArticleId";
            SqlParameter[] sqlPara =
            {
                 new SqlParameter("@ReportId",articleEntity.ReportId),
                new SqlParameter("@ReportStatus",articleEntity.ReportStatus),
                new SqlParameter("@ReportAt",articleEntity.ReportAt),
               new SqlParameter("@ArticleId",articleEntity.ArticleId),
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        /// <summary>
        /// Update Article Status
        /// </summary>
        /// <param name="articleEntity">.</param>
        public bool UpdateStatus(ArticleEntity articleEntity)
        {
            strSql = "UPDATE Articles SET Status=@Status WHERE ArticleId=@ArticleId";
            SqlParameter[] sqlPara =
            {
                
                new SqlParameter("@Status",articleEntity.Status),
                new SqlParameter("@ArticleId",articleEntity.ArticleId),
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        /// <summary>
        /// Update Article Status Reject
        /// </summary>
        /// <param name="articleEntity">.</param>

        public bool UpdateStatusReject(ArticleEntity articleEntity)
        {
            strSql = "Delete Photos WHERE ArticleId=@ArticleId; Delete Articles WHERE ArticleId=@ArticleId";
            SqlParameter[] sqlPara =
            {

                new SqlParameter("@Status",articleEntity.Status),
                new SqlParameter("@ArticleId",articleEntity.ArticleId),
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        

        public DataTable CheckArticle(int id, string column)
        {
            strSql = "SELECT * FROM Articles WHERE " + column + " = " + id;
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }


        /// <summary>
        /// Get Article with articleId and userId
        /// </summary>
        /// /// <param name="articleId" and name="userId">.</param>
        public DataTable CheckArticleWithUser(int articleId, int userId)
        {
            strSql = "SELECT * FROM Articles WHERE ArticleId = " + articleId + " AND UserId = " + userId;
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }


        /// <summary>
        /// Get Article List 
        /// </summary>
        public List<ArticleEntity> GetAllArticle()
        {
            strSql = "select * from Articles as articles , Users as users , Categories as categories where articles.CategoryId=categories.CategoryId and articles.UserId=users.UserId and articles.DeleteStatus=0";
            return connection.GetAllArticle(CommandType.Text, strSql);
        }

        /// <summary>
        /// Get Article List with id
        /// </summary>
        ///  /// /// <param name="id">.</param>
        public List<ArticleEntity> GetAllArticle(int id)
        {
            strSql = "select * from Articles as articles , Users as users , Categories as categories where articles.CategoryId=categories.CategoryId and articles.UserId=users.UserId and articles.DeleteStatus=0 and articles.ArticleId=" + id;
            return connection.GetAllArticle(CommandType.Text, strSql);
        }


        public List<ArticleEntity> GetTrending()
        {
            strSql = "SELECT Articles.* FROM Articles WHERE Articles.ArticleId in (SELECT Likes.ArticleID FROM Likes GROUP BY Likes.ArticleId HAVING COUNT(Likes.LikeId) >= 2) AND Articles.DeleteStatus = 0 AND Articles.Status='active' ";
            return connection.GetAllTrending(CommandType.Text, strSql);
        }

        public DataTable GetTrends()
        {
            strSql = "SELECT Articles.* FROM Articles WHERE Articles.ArticleId in (SELECT Likes.ArticleID FROM Likes GROUP BY Likes.ArticleId HAVING COUNT(Likes.LikeId) >= 2) AND Articles.DeleteStatus = 0 AND Articles.Status='active' ";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }


        /// <summary>
        /// Get Article Detail By Slug with slug
        /// </summary>
        ///  /// /// <param name="slug">.</param>
        public List<ArticleEntity> GATDetailBySlug(string slug)
        {
            strSql = "select * from Articles as articles , Users as users , Categories as categories where articles.CategoryId=categories.CategoryId and articles.UserId=Users.UserId and articles.DeleteStatus=0 and articles.Slug= '"+ slug +"' " ;
            return connection.GetAllArticle(CommandType.Text, strSql);
        }

        /// <summary>
        /// Get Count Article with articleEntity
        /// </summary>
        ///  /// /// <param name="articleEntity">.</param>
        public int CountSlug(ArticleEntity articleEntity)
        {
            strSql = "SELECT COUNT(*) FROM Articles WHERE Slug=" + "@Slug and DeleteStatus=0";
            SqlParameter[] sqlPara =
           {
                 new SqlParameter("@Slug",articleEntity.Slug),
            };

            int sucess = Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlPara));

            return sucess;
        }

        /// <summary>
        /// Get Count Article with slug and userid
        /// </summary>
        ///  /// /// <param name="slug" and name="userid">.</param>
        public int CountPermit(string slug,int userid)
        {
            strSql = "select COUNT(*) from Articles as articles , Users as users , Categories as categories where articles.CategoryId=categories.CategoryId and articles.UserId=Users.UserId and articles.DeleteStatus=0 and articles.Slug=@Slug and articles.UserId=@UserId";
            SqlParameter[] sqlPara =
           {
                 new SqlParameter("@Slug",slug),
                 new SqlParameter("@UserId",userid),
            };

            int sucess = Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlPara));

            return sucess;
        }


        /// <summary>
        /// Get Count Title with articleEntity
        /// </summary>
        ///  /// /// <param name="articleEntity">.</param>
        public int CountTitle(ArticleEntity articleEntity)
        {
            strSql = "SELECT COUNT(*) FROM Articles WHERE Title=" + "@Title and DeleteStatus=0";
            SqlParameter[] sqlPara =
           {
                 new SqlParameter("@Title",articleEntity.Title),
            };

            int sucess = Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlPara));

            return sucess;
        }

        public int CountThumbnail(ArticleEntity articleEntity)
        {
            strSql = "SELECT COUNT(*) FROM Articles WHERE Thumbnail=" + "@Thumbnail and DeleteStatus=0";
            SqlParameter[] sqlPara =
           {
                 new SqlParameter("@Thumbnail",articleEntity.Thumbnail),
            };

            int sucess = Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlPara));

            return sucess;
        }



        public int CountUpdateTitle(ArticleEntity articleEntity)
        {
            strSql = "SELECT COUNT(*) FROM Articles WHERE Title=" + "@Title and DeleteStatus=0 and ArticleId!=" + "@ArticleId";
            SqlParameter[] sqlPara =
           {
                 new SqlParameter("@Title",articleEntity.Title),
                 new SqlParameter("@ArticleId",articleEntity.ArticleId),
            };

            int sucess = Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlPara));

            return sucess;
        }


        public int CountUpdateSlug(ArticleEntity articleEntity)
        {
            strSql = "SELECT COUNT(*) FROM Articles WHERE Slug=" + "@Slug and DeleteStatus=0 and ArticleId!=" + "@ArticledId";
            SqlParameter[] sqlPara =
           {
                 new SqlParameter("@Slug",articleEntity.Slug),
                 new SqlParameter("@ArticledId",articleEntity.ArticleId),
            };

            int sucess = Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlPara));

            return sucess;
        }


        public int CountUpdateThumbnail(ArticleEntity articleEntity)
        {
            strSql = "SELECT COUNT(*) FROM Articles WHERE Thumbnail=" + "@Thumbnail and DeleteStatus=0 and ArticleId!=" + "@ArticledId";
            SqlParameter[] sqlPara =
           {
                 new SqlParameter("@Thumbnail",articleEntity.Thumbnail),
                 new SqlParameter("@ArticledId",articleEntity.ArticleId),
            };

            int sucess = Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlPara));

            return sucess;
        }


        public int GetId(ArticleEntity articleEntity)
        {
            strSql = "SELECT ArticleId FROM Articles WHERE Slug=" + "@Slug and DeleteStatus=0";
            SqlParameter[] sqlPara =
           {
                 new SqlParameter("@Slug",articleEntity.Slug),
            };

            int sucess = Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlPara));

            return sucess;
        }

        public bool Delete(int id)
        {
            strSql = "Update Articles Set DeleteStatus=1 where ArticleId=@ArticleId";

            SqlParameter[] sqlPara =
            {
                new SqlParameter("@ArticleId",id)
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        public bool Remove(int id)
        {
            strSql = "DELETE FROM Articles WHERE ArticleId=@ArticleId AND DeleteStatus=1";

            SqlParameter[] sqlPara =
            {
                new SqlParameter("@ArticleId",id)
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        public bool ReportRemove(int id)
        {
            strSql = "DELETE FROM Articles WHERE ArticleId=@ArticleId AND DeleteStatus=0 AND ReportStatus = 1";

            SqlParameter[] sqlPara =
            {
                new SqlParameter("@ArticleId",id)
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        public bool RemoveAllArticles(int id)
        {
            strSql = "DELETE FROM Articles WHERE ArticleId=@ArticleId ";

            SqlParameter[] sqlPara =
            {
                new SqlParameter("@ArticleId",id)
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        public bool UnArchieve(int id)
        {
            strSql = "Update Articles Set DeleteStatus=0 where ArticleId=@ArticleId";

            SqlParameter[] sqlPara =
            {
                new SqlParameter("@ArticleId",id)
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        public DataTable GetSpecificArchieve (int id)
        {
            strSql = "SELECT * FROM Articles WHERE ArticleId = "+id;
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }


        public DataTable GetCategory()
        {
            strSql = "select * from Categories";

            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }
        #endregion
    }
}
