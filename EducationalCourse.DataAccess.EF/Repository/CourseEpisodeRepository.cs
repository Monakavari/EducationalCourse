using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class CourseEpisodeRepository: BaseRepository<CourseEpisode>, ICourseEpisodeRepository
    {
        private readonly EducationalCourseContext _context;
        public CourseEpisodeRepository(EducationalCourseContext context):base(context) 
        {
            _context = context;
        }
    }
}
