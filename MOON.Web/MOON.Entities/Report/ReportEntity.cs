using System;


namespace MOON.Entities.Report
{
    public class ReportEntity
    {
        public int ReportId { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        public ReportEntity()
        {
            InitializedObjectValue();
        }

        internal void InitializedObjectValue()
        {
            this.ReportId = 0;
            this.Message = string.Empty;
            this.CreatedAt = DateTime.Now;
        }



    }
}
