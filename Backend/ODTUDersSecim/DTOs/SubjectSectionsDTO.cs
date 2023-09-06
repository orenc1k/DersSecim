


namespace ODTUDersSecim.DTOs
{

    public class SubjectSectionsDTO
    {
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
        public int? SubjectCode { get; set; }

    }

}