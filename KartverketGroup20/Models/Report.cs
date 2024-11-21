using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using KartverketGroup20.Data.Enum;

namespace KartverketGroup20.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        [BindNever] 
        public string UserId { get; set; }

        [Required]
        public string GeoJson { get; set; }

        [Required]
        public string Description { get; set; }

        public string? MapType { get; set; }

        public Status Status { get; set; }

        [DataType(DataType.Time)]
        public DateTime? ReportTime { get; set; }

        [Display(Name = "Tilbakemelding")]
        public string? Feedback {  get; set; }
        

    }
}