using System.ComponentModel.DataAnnotations.Schema;

namespace AnewareApp.Models
{
    public class ReportModel
    {
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("user")]
        public string User { get; set; }

        [Column("severity")]
        public string Severity { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("image")]
        public string? ImageUrl { get; set; }

        [Column("created_at")]
        public DateTime Create_At { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime Update_At { get; set; }
    }
}
