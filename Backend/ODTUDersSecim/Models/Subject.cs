using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODTUDersSecim.Models
{
    public class Subject
    {
        public int SubjectCode { get; set; }
        public int[] Sections { get; set; }
    }
}
