using System.ComponentModel.DataAnnotations;

namespace WarehouseProject.Models
{
    public class WarehouseReportModel
    {
        [Key]
        public int ReportID { get; set; }
        public string Scope { get; set; }
        public string Metrics { get; set; }
        public DateTime GeneratedDate { get; set; }
    }
}
