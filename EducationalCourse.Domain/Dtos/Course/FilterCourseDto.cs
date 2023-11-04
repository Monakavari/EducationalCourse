
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.Domain.Dtos.Course
{
    public class FilterCourseDto
    {
        public string CourseTitle { get; set; }
        public bool IsFreeCost { get; set; }
        public int CoursePrice { get; set; }
        public string CourseImageBase64 { get; set; }
        public string CourseImageName { get; set; }

    }
}
