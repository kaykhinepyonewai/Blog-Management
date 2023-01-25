using MOON.DAO.Dashboard;
using MOON.Entities.Dashboard;
using System.Collections.Generic;
using System.Data;

namespace MOON.Services.Dashboard
{
    public class ArticleService
    {
        private ArticleDao articleDao = new ArticleDao();

        public bool Insert(ArticleEntity articleEntity)
        {
            return articleDao.Insert(articleEntity);
        }

        public List<ArticleEntity> GAT()
        {
            List<ArticleEntity> cc = articleDao.GetAllArticle();
            return cc;
        }

        public List<ArticleEntity> GetTrending()
        {
            List<ArticleEntity> cc = articleDao.GetTrending();
            return cc;
        }

        public DataTable GetTrends()
        {
            return articleDao.GetTrends();
        }

        public List<ArticleEntity> GATDetail(int id)
        {
            List<ArticleEntity> cc = articleDao.GetAllArticle(id);
            return cc;
        }


        public List<ArticleEntity> GATDetailBySlug(string slug)
        {
            List<ArticleEntity> cc = articleDao.GATDetailBySlug(slug);
            return cc;
        }

        public int CountPermit(string slug,int userid)
        {
            return articleDao.CountPermit(slug,userid);
        }

        public int CountSlug(ArticleEntity articleEntity)
        {
            return articleDao.CountSlug(articleEntity);
        }

        public int CountTitle(ArticleEntity articleEntity)
        {
            return articleDao.CountTitle(articleEntity);
        }

        public int CountThumbnail(ArticleEntity articleEntity)
        {
            return articleDao.CountThumbnail(articleEntity);
        }


        public int CountUpdateSlug(ArticleEntity articleEntity)
        {
            return articleDao.CountUpdateSlug(articleEntity);
        }

        public int CountUpdateTitle(ArticleEntity articleEntity)
        {
            return articleDao.CountUpdateTitle(articleEntity);
        }

        public int CountUpdateThumbnail(ArticleEntity articleEntity)
        {
            return articleDao.CountUpdateThumbnail(articleEntity);
        }

        public DataTable GetAll()
        {
            DataTable dt = articleDao.GetAll();
            return dt;
        }

        public DataTable GetAllBySearch(string keyword)
        {
            DataTable dt = articleDao.GetAllBySearch(keyword);
            return dt;
        }

        public DataTable GetSpecific(string column, string value)
        {
            DataTable dt = articleDao.GetSpecific(column, value);
            return dt;
        }

        public DataTable GetRelated(int id, string slug)
        {
            DataTable dt = articleDao.GetRelated(id, slug);
            return dt;
        }

        public DataTable GetReports()
        {
            DataTable dt = articleDao.GetReports();
            return dt;
        }

        public DataTable GetReportsBySearch(string keyword)
        {
            DataTable dt = articleDao.GetReportsBySearch(keyword);
            return dt;
        }

        public bool UpdateArticleReport(ArticleEntity articleEntity)
        {
            bool success = articleDao.UpdateArticleReport(articleEntity);
            return success;
        }

        public DataTable ShowCard()
        {
            DataTable dt = articleDao.ShowCard();
            return dt;  
        }

        public DataTable GetEmail(int id)
        {
            DataTable dt = articleDao.GetEmail(id);
            return dt;
        }

        public DataTable ArticleFilterByCategory(string slug)
        {
            DataTable dt = articleDao.ArticleFilterByCategory(slug);
            return dt;
        }

        public DataTable ArticleFilterBySearch(string keyword)
        {
            DataTable dt = articleDao.ArticleFilterBySearch(keyword);
            return dt;
        }

        public DataTable ArticleFilterByBoth(string keyword, string slug)
        {
            DataTable dt = articleDao.ArticleFilterByBoth(keyword, slug);
            return dt;
        }


        public DataTable GetThumbnail(int id)
        {
            DataTable dt = articleDao.GetThumbnail(id);
            return dt;
        }


        public DataTable UserGetAll(int user)
        {
            DataTable dt = articleDao.UserGetAll(user);
            return dt;
        }

        public DataTable UserGetAllBySearch(int user, string keyword)
        {
            DataTable dt = articleDao.UserGetAllBySearch(user, keyword);
            return dt;
        }

        public DataTable GetWaiting(int user)
        {
            DataTable dt = articleDao.GetWaiting(user);
            return dt;
        }

        public DataTable GetWaitingBySearch(int user, string keyword)
        {
            DataTable dt = articleDao.GetWaitingBySearch(user, keyword);
            return dt;
        }

        public DataTable GetAllPending()
        {
            DataTable dt = articleDao.GetAllPending();
            return dt;
        }

        public DataTable GetAllPendingBySearch(string keyword)
        {
            DataTable dt = articleDao.GetAllPendingBySearch(keyword);
            return dt;
        }

        public DataTable GetArchieve(int id)
        {
            DataTable dt = articleDao.GetArchieve(id);
            return dt;
        }

        public DataTable GetArchieveBySearch(int id,string keyword)
        {
            DataTable dt = articleDao.GetArchieveBySearch(id, keyword);
            return dt;
        }

        public DataTable Get(int id)
        {
            DataTable dt = articleDao.Get(id);
            return dt;
        }

        public bool Update(ArticleEntity articleEntity)
        {
            return articleDao.Update(articleEntity);
        }

        public bool UpdateStatus(ArticleEntity articleEntity)
        {
            return articleDao.UpdateStatus(articleEntity);
        }


        public bool UpdateStatusReject(ArticleEntity articleEntity)
        {
            return articleDao.UpdateStatusReject(articleEntity);
        }

        public DataTable CheckArticle(int id, string column)
        {
            return articleDao.CheckArticle(id,column);
        }

        public DataTable CheckArticleWithUser(int articleId, int userId)
        {
            return articleDao.CheckArticleWithUser(articleId, userId);
        }

        public int GetId(ArticleEntity articleEntity)
        {
            return articleDao.GetId(articleEntity);
        }

        public DataTable GetCategory()
        {
            DataTable dt = articleDao.GetCategory();
            return dt;
        }

        public bool Delete(int id)
        {
            return articleDao.Delete(id);
        }

        public bool ReportRemove(int id)
        {
            return articleDao.ReportRemove(id);
        }

        public bool UnArchieve(int id)
        {
            return articleDao.UnArchieve(id);
        }

        public bool Remove(int id)
        {
            return articleDao.Remove(id);
        }

        public DataTable GetSpecificArchieve(int id)
        {
            return articleDao.GetSpecificArchieve(id);
        }

    }
}
