using MOON.DAO;
using MOON.Entities.Dashboard;
using System.Collections.Generic;
using System.Data;

namespace MOON.Services.Dashboard
{
    public class CategoryService
    {

        private CategoryDao categoryDao = new CategoryDao();
        public bool Insert(CategoryEntity categoryEntity)
        {
            return categoryDao.Insert(categoryEntity);
        }
        public int CountName(CategoryEntity categoryEntity)
        {
            return categoryDao.CountName(categoryEntity);
        }

        public int CountSlug(CategoryEntity categoryEntity)
        {
            return categoryDao.CountSlug(categoryEntity);
        }

        public List<CategoryEntity> GAC()
        {
            List<CategoryEntity> cc = categoryDao.GetAllCategory();
            return cc;
        }

        public DataTable Get(int id)
        {
            DataTable dt = categoryDao.Get(id);
            return dt;
        }

        public DataTable GetAll()
        {
            DataTable dt = categoryDao.GetAll();
            return dt;
        }

        public DataTable GetAllBySearch(string keyword)
        {
            DataTable dt = categoryDao.GetAllBySearch(keyword);
            return dt;
        }

        public int CountTitleUpdate(CategoryEntity categoryEntity)
        {
            return categoryDao.CountTitleUpdate(categoryEntity);
        }

        public int CountSlugUpdate(CategoryEntity categoryEntity)
        {
            return categoryDao.CountSlugUpdate(categoryEntity);
        }

        public bool Update(CategoryEntity categoryEntity)
        {
            return categoryDao.Update(categoryEntity);
        }

        public bool Delete(int id)
        {
            return categoryDao.Delete(id);
        }


    }
}
