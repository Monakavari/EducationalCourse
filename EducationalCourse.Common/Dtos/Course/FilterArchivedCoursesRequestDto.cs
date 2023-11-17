using EducationalCourse.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.Common.Dtos.Course
{
    public class FilterArchivedCoursesRequestDto: BasePagingDto
    {
        public FilterArchivedCoursesRequestDto()
        {
            CourseGroupIds = new List<int>();
            SubCourseGroupIds = new List<int>();
        }
        public List<int> CourseGroupIds { get; set; }
        public List<int> SubCourseGroupIds { get; set; }
        public string CourseTitle { get; set; }
        public int? StartPrice { get; set; }
        public int? EndPrice { get; set; }
        public bool IsFreeCost { get; set; }
        public OrderByEnum OrderByType { get; set; }
    }
}
