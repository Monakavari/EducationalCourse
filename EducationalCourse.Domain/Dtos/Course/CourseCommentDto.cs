using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.Domain.Dtos.Course
{
    public class CourseCommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public bool IsAdminRead { get; set; }
        public int? ParentId { get; set; }
        public List<CourseComment> Children { get; set; }
    }
}
