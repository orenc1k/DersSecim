
using ODTUDersSecim.Models;

namespace ODTUDersSecim.DTOs
{
    public class ElectiveCoursesDTO
    {

        public Electives.ElectiveTypes? ElectiveType { get; set; }

        public int? SubjectCode { get; set; }

        public int? DeptCode { get; set; }
    }
}

