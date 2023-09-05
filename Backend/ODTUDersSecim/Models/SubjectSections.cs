using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODTUDersSecim.Models
{
    public class SubjectSections
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SectionId { get; set; }

        public int SectionCode { get; set; }

        public string? GivenDept { get; set; }

        public string? StartChar { get; set; }

        public string? EndChar { get; set; }

        public float? MinCumGpa { get; set; }

        public float? MaxCumGpa { get; set; }

        public int? MinYear { get; set; }

        public int? MaxYear { get; set; }

        public string? StartGrade { get; set; }

        public string? EndGrade { get; set; }

        [ForeignKey("SubjectCode")]
        public int SubjectCode { get; set; }
        public Subjects? Subjects { get; set; }

        public ICollection<SectionDays>? SectionDays { get; set; }

    }
}
