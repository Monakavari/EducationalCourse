using Microsoft.VisualBasic;

namespace EducationalCourse.Common.Dtos.Comment
{
    public class GetAllCommentsDto
    {
        public int CourseId { get; set; }
        public BasePagingDto BasePaging { get; set; }
    }
}
