using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODTUDersSecim.Models
{
    public class ElectiveCourses
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ElectiveCoursesId { get; set; }

        public Electives.ElectiveTypes? ElectiveType { get; set; }

        [ForeignKey("SubjectCode")]
        public int? SubjectCode { get; set; }
        public virtual AvailableCourses? AvailableCourses { get; set; }

        [ForeignKey("DeptCode")]
        public int? DeptCode { get; set; }
        public virtual Departments? Departments { get; set; }

    }
}
