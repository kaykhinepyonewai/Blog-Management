using MOON.DAO.Dashboard;
using MOON.Entities.Dashboard;
using System.Collections.Generic;
using System.Data;

namespace MOON.Services.Dashboard
{
    public class PhotoService
    {
        private PhotoDao photoDao = new PhotoDao();
        public bool InsertImage(PhotoEntity photoEntity)
        {
            return photoDao.Insert(photoEntity);
        }

        public List<PhotoEntity> GetAllImage(int id)
        {
            List<PhotoEntity> cc = photoDao.GetAllImage(id);
            return cc;
        }

        public List<PhotoEntity> GetAllImages(int id)
        {
            List<PhotoEntity> cc = photoDao.GetAllImages(id);
            return cc;
        }



        public List<PhotoEntity> GetImageBySlug(string slug)
        {
            List<PhotoEntity> cc = photoDao.GetImageBySlug(slug);
            return cc;
        }


        public List<PhotoEntity> GetArchieveImages(int id)
        {
            List<PhotoEntity> cc = photoDao.GetArchieveImages(id);
            return cc;
        }

        public DataTable GetImage(int id)
        {
            DataTable dt = photoDao.GetImage(id);
            return dt;
        }

        public bool Delete(int id)
        {
            return photoDao.Delete(id);
        }

        public bool UnArchieve(int id)
        {
            return photoDao.UnArchieve(id);
        }

        public bool Remove(int id)
        {
            return photoDao.Remove(id);
        }

        public bool ReportPhotosRemove(int id)
        {
            return photoDao.ReportPhotosRemove(id);
        }

        public bool ReportAllPhotosRemove(int id)
        {
            return photoDao.ReportAllPhotosRemove(id);
        }

        public bool DeleteAritcle(int id)
        {
            return photoDao.DeleteAritcle(id);
        }

    }
}
