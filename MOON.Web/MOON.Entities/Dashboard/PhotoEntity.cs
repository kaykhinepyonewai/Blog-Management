using System;
using System.Collections.Generic;

namespace MOON.Entities.Dashboard
{
    public class PhotoEntity
    {
        private static IList<PhotoEntity> Photos;

        public int PhotoId { get; set; }

        public int ArticleId { get; set; }

        public string PhotoImage { get; set; }

        public DateTime CreatedAt { get; set; }

        public int DeleteStatus { get; set; }

        public PhotoEntity()
        {
            InitializedObjectValue();
        }

        internal void InitializedObjectValue()
        {
            this.PhotoId = 0;
            this.ArticleId = 0;
            this.PhotoImage = string.Empty;
            this.CreatedAt = DateTime.Now;
            this.DeleteStatus = 0;
        }
    }
}
