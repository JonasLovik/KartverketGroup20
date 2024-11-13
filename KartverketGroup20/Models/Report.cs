using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KartverketGroup20.Models
{
    public class Report
    {

        [Required]
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string? MapSpecification { get; set; }
        public string Content { get; set; } = String.Empty;
        public string? GeoJson { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public int UserId { get; set; }

    }
