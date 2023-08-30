using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODTUDersSecim.Models
{
    public class AEESubjects : ISubjects
    {
        [Key]
        public int SubjectCode { get; set; }

        public string? SubjectName { get; set; }

        public int? SubjectCredit { get; set; }

        public string? SubjectLevel { get; set; }

        public string? SubjectType { get; set; }
    }
}
