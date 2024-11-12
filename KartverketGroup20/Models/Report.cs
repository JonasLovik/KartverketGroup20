using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KartverketGroup20.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("AppUser")]
        public int AppUserId { get; set; }
        public string GeoJson { get; set; }
        public string ReportTime { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string MapType { get; set; }

    }
}
