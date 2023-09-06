using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODTUDersSecim.Models
{
	public class SectionDays
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SectionDaysId { get; set; }

        public string? Day1 { get; set; }
        public string? Time1 { get; set; }

        public string? Day2 { get; set; }
        public string? Time2 { get; set; }

        public string? Day3 { get; set; }
        public string? Time3 { get; set; }


        [ForeignKey("SectionId")]
        public int? SectionId { get; set; }
        public virtual SubjectSections? SubjectSections { get; set; }

        [ForeignKey("SubjectCode")]
        public int? SubjectCode { get; set; }
        public virtual Subjects? Subjects { get; set; }
    }
}

