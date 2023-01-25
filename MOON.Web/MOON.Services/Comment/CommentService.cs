using MOON.DAO.Comment;
using MOON.Entities.Comment;
using System.Data;

namespace MOON.Services.Comment
{
    /// <summary>
    /// Defines the <see cref="CommentService" />.
    /// </summary>
    public class CommentService
    {
        /// <summary>
        /// Define Comment Dao.
        /// </summary>
        CommentDao commentDao = new CommentDao();

        /// <summary>
        /// Save Comment.
        /// </summary>
        /// <param name="commentEntity">.</param>
        public bool Insert(CommentEntity commentEntity)
        {
            return commentDao.Insert(commentEntity);
        }

        public bool Update(CommentEntity commentEntity)
        {
            return commentDao.Update(commentEntity);
        }

        public DataTable GetComments(int id)
        {
            return commentDao.GetComments(id);
        }

        public DataTable GetCommentsBySearch(int id, string keyword)
        {
            return commentDao.GetCommentsBySearch(id, keyword);
        }
        public DataTable GetSpecific(string column, int value)
        {
            return commentDao.GetSpecific(column, value);
        }

        public bool DeleteSpecificArticle(int id)
        {
            return commentDao.DeleteSpecificArticle(id);
        }

        public bool DeleteComment(int articleId, int commentId)
        {
            return commentDao.DeleteComment(articleId, commentId);
        }

    }
}
