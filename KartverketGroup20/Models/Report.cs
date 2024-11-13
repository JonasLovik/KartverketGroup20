using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KartverketGroup20.Models
{
    public class Report
    {
        
        public int Id { get; set; }
        public string? Title { get; set; } = String.Empty;
        public string? MapType { get; set; }
        public string? Description { get; set; } = String.Empty;
        public string? GeoJson { get; set; }
        public DateTime? ReportTime { get; set; } = DateTime.Now;
        public int UserId { get; set; }

    }
}