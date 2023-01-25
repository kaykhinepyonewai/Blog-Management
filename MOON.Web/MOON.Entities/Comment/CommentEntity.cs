using System;

namespace MOON.Entities.Comment
{
    public class CommentEntity
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public CommentEntity()
        {
            InitializedObjectValue();
        }

        /// <summary>
        /// The InitializedObjectValue.
        /// </summary>
        internal void InitializedObjectValue()
        {
            this.CommentId = 0;
            this.UserId = 0;
            this.ArticleId = 0;
            this.Message = string.Empty;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }
    }
}
