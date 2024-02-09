using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class CourseVoteRepository : BaseRepository<CourseVote>, ICourseVoteRepository
    {
        #region Consteructor

        private readonly EducationalCourseContext _context;
        public CourseVoteRepository(EducationalCourseContext context) : base(context)
        {
            _context = context;
        }

        #endregion Consteructor

        //*********************************** CheckUserVote ***********************************
        public async Task<CourseVote> GetUserVote(int userId,int courseId, CancellationToken cancellationToken)
        {
            return await _context.CourseVotes
                .Where(x => x.UserId == userId)
                .Where(x => x.CourseId == courseId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
