using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.Common.Dtos.Course
{
    public class EditCourseDto
    {
        public int Id { get; set; }
        public int CourseGroupId { get; set; }
        public int? SubCourseGroupId { get; set; }
        public int CourseLevelId { get; set; }
        public int CourseStetusId { get; set; }
        public int TeacherId { get; set; }
        public string CourseTitle { get; set; }
        public int CoursePrice { get; set; }
        public bool IsFreeCost { get; set; }
        public string DirectoryName { get; set; }
        public IFormFile FormFile { get; set; }
        public IFormFile DemoFile { get; set; }
    }
}
