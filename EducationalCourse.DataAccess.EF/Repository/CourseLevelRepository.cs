using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Repository;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class CourseLevelRepository:BaseRepository<CourseLevel>, ICourseLevelRepository
    {
        private readonly EducationalCourseContext _context; 
        public CourseLevelRepository(EducationalCourseContext context) :base(context)
        {
            _context = context;
        }
    }
}
