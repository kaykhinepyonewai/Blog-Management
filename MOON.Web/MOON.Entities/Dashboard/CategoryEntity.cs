using System;
using System.Collections.Generic;

namespace MOON.Entities.Dashboard
{
    public class CategoryEntity
    {
        private static IList<CategoryEntity> category;


        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public DateTime CreatedAt { get; set; }

        internal static IList<CategoryEntity> Category
        {
            get => category;
            set => category = value;
        }


        public CategoryEntity()
        {
            InitializedObjectValue();
        }

        internal void InitializedObjectValue()
        {
            this.CategoryId = 0;
            this.Name = string.Empty;
            this.Slug = string.Empty;
            this.CreatedAt = DateTime.Now;
        }



    }
}
