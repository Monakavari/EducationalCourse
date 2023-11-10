using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Repository;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class CourseStatusRepository : BaseRepository<CourseStatus>, ICourseStatusRepository
    {
        #region Constructor

        private readonly EducationalCourseContext _context;

        public CourseStatusRepository(EducationalCourseContext context) : base(context)
        {
            _context = context;
        }
        #endregion Constructor
    }
}
