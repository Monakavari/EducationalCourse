using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Repository;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class UserCourseRepository:BaseRepository<UserCourse>, IUserCourseRepository
    {
        #region Constructor

        private readonly EducationalCourseContext _context;

        public UserCourseRepository(EducationalCourseContext context):base(context) 
        {
            _context = context;
        }

        #endregion Constructor
    }
}
