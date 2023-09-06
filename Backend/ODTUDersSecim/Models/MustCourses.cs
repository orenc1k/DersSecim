using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODTUDersSecim.Models
{
	public class MustCourses
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MustCourseId {get; set;}

		public int Semester { get; set; }

		public string CourseName { get; set; }

        [ForeignKey("DeptCode")]
        public int DeptCode { get; set; }
        public virtual Departments? Departments { get; set; }

        [ForeignKey("SubjectCode")]
        public int SubjectCode { get; set; }
        public virtual Subjects? Subjects { get; set; }

    }
}

