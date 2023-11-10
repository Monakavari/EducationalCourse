using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.Domain.Dtos.Course
{
    public class EditCourseEpisodeDto
    {
        public int CourseId { get; set; }
        public int CourseEpisodeId { get; set; }
        public string EpisodeTime { get; set; }
        public string EpisodeFileName { get; set; }
        public string EpisodeFileTitle { get; set; }
        public bool IsFree { get; set; }
    }
}
