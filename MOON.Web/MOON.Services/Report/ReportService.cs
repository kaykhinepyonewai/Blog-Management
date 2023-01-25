using MOON.DAO.Report;
using MOON.Entities.Report;
using System.Data;

namespace MOON.Services.Report
{
    public class ReportService
    {
        private ReportDao reportDao = new ReportDao();

        public DataTable GetAll()
        {
            DataTable dt = reportDao.GetAll();
            return dt;
        }

        public DataTable Get(int id)
        {
            DataTable dt = reportDao.Get(id);
            return dt;
        }

        public bool Insert(ReportEntity reportEntity)
        {
            return reportDao.Insert(reportEntity);
        }

        public bool Update(ReportEntity reportEntity)
        {
            return reportDao.Update(reportEntity);
        }

        public bool Delete(int id)
        {
            return reportDao.Delete(id);
        }
        public int Exist(ReportEntity reportEntity)
        {
            return reportDao.Exist(reportEntity);
        }
    }
}
