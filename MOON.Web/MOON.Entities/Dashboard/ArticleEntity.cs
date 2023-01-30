using System;
using System.Collections.Generic;

namespace MOON.Entities.Dashboard
{

    /// <summary>
    /// Defines the <see cref="ArticleEntity" />.
    /// </summary>
    public class ArticleEntity
    {
        /// <summary>
        /// Gets or sets the IList ArticleEntity.
        /// </summary>
        private static IList<ArticleEntity> article;


        /// <summary>
        /// Gets or sets the Article id.
        /// </summary>
        public int ArticleId { get; set; }


        /// <summary>
        /// Gets or sets the User id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the Category id.
        /// </summary>
        public int CategroyId { get; set; }


        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Slug.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Excerpt.
        /// </summary>
        public string Excerpt { get; set; }

        /// <summary>
        /// Gets or sets the Thumbnail.
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the CreatedAt.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the DeleteStatus.
        /// </summary>
        public int DeleteStatus { get; set; }

        /// <summary>
        /// Gets or sets the UserName.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>

        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the CategoryName.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the Profile.
        /// </summary>
        public string Profile { get; set; }

        public int ReportId { get; set; }

        public int ReportStatus { get; set; }

        public DateTime ReportAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleEntity"/> class.
        /// </summary>
        public ArticleEntity()
        {
            InitializedObjectValue();
        }

        /// <summary>
        /// The InitializedObjectValue.
        /// </summary>
        internal void InitializedObjectValue()
        {
            this.ArticleId = 0;
            this.UserId = 0;
            this.CategroyId = 0;
            this.CreatedAt = DateTime.Now;
            this.Title = string.Empty;
            this.Slug = string.Empty;
            this.Description = string.Empty;
            this.Excerpt = string.Empty;
            this.Thumbnail = string.Empty;
            this.UserName = string.Empty;
            this.Email = string.Empty;
            this.CategoryName = string.Empty;
            this.Status = string.Empty;
            this.CreatedAt = DateTime.Now;
            this.DeleteStatus = 0;
            this.ReportId = 0;
            this.ReportStatus = 0;
            this.ReportAt = DateTime.Now;
        }


    }
}
