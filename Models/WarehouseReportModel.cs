using System.ComponentModel.DataAnnotations;
using WarehousePro.API.Models.Enums;

namespace WarehouseProject.Models
{
    public class WarehouseReportModel
    {
        [Key]
        public int ReportID { get; set; }
        public ReportScope Scope { get; set; }
        public string Metrics { get; set; }
        public DateTime GeneratedDate { get; set; }
    }
}
