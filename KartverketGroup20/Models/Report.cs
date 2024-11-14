using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KartverketGroup20.Models
{
    public class Report
    {
        
        public int Id { get; set; }
        //[ForeignKey("AppUser")]
        //public int UserId { get; set; }
        public string? GeoJson { get; set; }
        public string? Description { get; set; }
        //public string? MapType { get; set; }
        //public string? Status { get; set; }
        [DataType(DataType.Time)]
        public DateTime? ReportTime { get; set; }
        

    }
}