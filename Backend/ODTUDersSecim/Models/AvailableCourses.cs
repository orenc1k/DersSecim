using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODTUDersSecim.Models
{
    public class AvailableCourses 
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AvailableCoursesId { get; set; }


        [ForeignKey("SubjectCode")]
        public int? SubjectCode { get; set; }
        public virtual Subjects? Subjects { get; set; }

    }
}
