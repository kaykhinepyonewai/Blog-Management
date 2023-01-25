using System;
using System.Collections.Generic;

namespace MOON.Entities.Like
{
    public class LikeEntity
    {
        private static IList<LikeEntity> like;

        public int LikeId { get; set; }

        public int ArticleId { get; set; }

        public string Thumbnail { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Excerpt { get; set; }

        public string Username { get; set; }

        public int UserId { get; set; }

        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }


        public LikeEntity()
        {
            InitializedObjectValue();
        }

        internal void InitializedObjectValue()
        {
            this.LikeId = 0;
            this.ArticleId = 0;
            this.Thumbnail = string.Empty;
            this.Title = string.Empty;
            this.Title = string.Empty;
            this.Slug = string.Empty;
            this.Excerpt = string.Empty;
            this.Username = string.Empty;
            this.UserId = 0;
            this.Status = 0;
            this.CreatedAt = DateTime.Now;
        }


    }
}
