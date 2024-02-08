using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities.Account;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class UserCourseRepository : BaseRepository<UserCourse>, IUserCourseRepository
    {
        #region Constructor

        private readonly EducationalCourseContext _context;

        public UserCourseRepository(EducationalCourseContext context) : base(context)
        {
            _context = context;
        }

        #endregion Constructor

        //***************************** IsUserInCourse ******************************
        public async Task<bool> IsUserInCourse(int userId, int courseId, CancellationToken cancellationToken)
        {
            return await _context.UserCourses
                            .Where(x => x.UserId == userId && x.CourseId == courseId)
                            .AnyAsync(cancellationToken);
        }

    }
}
