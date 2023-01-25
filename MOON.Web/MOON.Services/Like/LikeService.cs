using MOON.DAO.Like;
using MOON.Entities.Like;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOON.Services.Like
{
    public class LikeService
    {
        private LikeDao likeDao = new LikeDao();
        public bool Insert(LikeEntity likeEntity)
        {
            return likeDao.Insert(likeEntity);
        }

        public int CountLike(int id)
        {
            return likeDao.CountLike(id);
        }

        public int CountButton(int id,int userId)
        {
            return likeDao.CountButton(id,userId);
        }

        public DataTable ReactionViewers(int id)
        {
            return likeDao.ReactionViewers(id);
        }
        //public List<LikeEntity> GAT()
        //{
        //    List<LikeEntity> cc = likeDao.getAll();
        //    return cc;
        //}

        public bool Delete(LikeEntity likeEntity)
        {
            return likeDao.Delete(likeEntity);
        }

        public bool DeleteSpecificArticle(int id)
        {
            return likeDao.DeleteSpecificArticle(id);
        }
    }
}
