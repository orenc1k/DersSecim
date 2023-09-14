using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODTUDersSecim.Models
{
    public class Subjects : ISubjects
    {

        [Key]
        public int SubjectCode { get; set; }

        [StringLength(100)]
        public string? SubjectName { get; set; }

        public float? EctsCredit { get; set; }

        public float? SubjectCredit { get; set; }

        public string? SubjectLevel { get; set; }

        public string? SubjectType { get; set; }

    }
}
