using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.Domain.Dtos.Course
{
    public class AddChildCourseGroupDto
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string CourseGroupTitle { get; set; }
    }
}
