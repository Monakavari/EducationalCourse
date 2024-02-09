using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;

namespace EducationalCourse.Domain.Repository
{
    public interface ICourseVoteRepository : IBaseRepository<CourseVote>
    {
        Task<CourseVote> GetUserVote(int userId, int courseId, CancellationToken cancellationToken);
    }
}
