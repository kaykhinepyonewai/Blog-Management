using MOON.Entities.Dashboard;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using MOON.DAO.Common;

namespace MOON.DAO
{
    public class CategoryDao
    {
        private DbConnection connection = new DbConnection();

        private string strSql = String.Empty;

        public bool Insert(CategoryEntity categoryEntity)
        {

            strSql = "INSERT INTO Categories(Name,Slug,CreatedAt) " + "VALUES (@Name,@Slug,@CreatedAt)";
            SqlParameter[] sqlPara =
            {
                new SqlParameter("@Name",categoryEntity.Name),
                new SqlParameter("@Slug",categoryEntity.Slug),
                new SqlParameter("@CreatedAt",categoryEntity.CreatedAt),
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }



        public int CountName(CategoryEntity categoryEntity)
        {
            strSql = "SELECT COUNT(*) FROM Categories WHERE Name=" + "@Name";
            SqlParameter[] sqlPara =
           {
                 new SqlParameter("@Name",categoryEntity.Name),
            };

            int sucess = Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlPara));

            return sucess;
        }

        public int CountSlug(CategoryEntity categoryEntity)
        {
            strSql = "SELECT COUNT(*) FROM Categories WHERE Slug=" + "@Slug";
            SqlParameter[] sqlPara =
           {
                 new SqlParameter("@Slug",categoryEntity.Slug),
            };

            int sucess = Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlPara));

            return sucess;
        }

        public List<CategoryEntity> GetAllCategory()
        {

            strSql = "select * from Categories";
            return connection.GetAllCategory(CommandType.Text, strSql);
        }


        public DataTable Get(int id)
        {

            strSql = "select * from Categories  where CategoryId = " + id + " ";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetAll()
        {

            strSql = "select * from Categories ";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public DataTable GetAllBySearch(string keyword)
        {

            strSql = "select * from Categories Where Name LIKE '%"+ keyword +"%' ";
            return connection.ExecuteDataTable(CommandType.Text, strSql);
        }

        public int CountTitleUpdate(CategoryEntity categoryEntity)
        {
            strSql = "SELECT COUNT(*) FROM Categories WHERE Name=" + "@Name and CategoryId!=" + "@CategoryId";
            SqlParameter[] sqlPara =
           {
                 new SqlParameter("@Name",categoryEntity.Name),
                 new SqlParameter("@CategoryId",categoryEntity.CategoryId),
            };

            int sucess = Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlPara));
            return sucess;
        }

        public int CountSlugUpdate(CategoryEntity categoryEntity)
        {
            strSql = "SELECT COUNT(*) FROM Categories WHERE Slug=" + "@Slug and CategoryId!=" + "@CategoryId";
            SqlParameter[] sqlPara =
           {
                 new SqlParameter("@Slug",categoryEntity.Slug),
                 new SqlParameter("@CategoryId",categoryEntity.CategoryId),
            };

            int sucess = Convert.ToInt32(connection.ExecuteScalar(CommandType.Text, strSql, sqlPara));
            return sucess;
        }


        public bool Update(CategoryEntity categoryEntity)
        {

            strSql = "UPDATE Categories SET Name=@Name,Slug=@Slug WHERE CategoryId=@CategoryId";
            SqlParameter[] sqlPara =
            {
                 new SqlParameter("@Name",categoryEntity.Name),
                new SqlParameter("@Slug",categoryEntity.Slug),
                new SqlParameter("@CategoryId",categoryEntity.CategoryId),
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

        public bool Delete(int id)
        {
            strSql = "DELETE FROM Categories WHERE CategoryId=@CategoryId";

            SqlParameter[] sqlPara =
            {
                new SqlParameter("@CategoryId",id)
            };

            bool success = connection.ExecuteNonQuery(CommandType.Text, strSql, sqlPara);
            return success;
        }

    }


}

