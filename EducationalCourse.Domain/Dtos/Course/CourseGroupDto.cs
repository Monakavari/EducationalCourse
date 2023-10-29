using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.Domain.Dtos.Course
{
    public class CourseGroupDto
    {
        public int id { get; set; }
        public string Title { get; set; }
        public IEnumerable<CourseGroupDto> Children { get; set; }
    }
}
