using EducationalCourse.Domain.Entities.Account;
using EducationalCourse.Domain.ICommandRepositories.Base;

namespace EducationalCourse.Domain.Repository
{
    public interface IUserCourseRepository:IBaseRepository<UserCourse>
    {
        Task<bool> IsUserInCourse(int userId,int courseId,CancellationToken cancellationToken);
    }
}
