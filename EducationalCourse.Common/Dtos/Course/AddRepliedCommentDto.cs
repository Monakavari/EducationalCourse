using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.Common.Dtos.Course
{
    public class AddRepliedCommentDto
    {
        public string Text { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int CommentId { get; set; }
    }
}
