namespace EducationalCourse.Common.Dtos
{
    public class BasePagingDto
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public string Dir { get; set; }
    }
}
