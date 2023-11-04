using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;

namespace EducationalCourse.Domain.Repository
{
    public interface ICourseGroupRepository : IBaseRepository<CourseGroup>
    {
        Task<bool> ExistCourseGroupName(string courseGroupName, CancellationToken cancellationToken);
        Task<bool> ExistCourseGroupNameForAnotherCourseGroup(string courseGroupName, int courseGroupId, CancellationToken cancellationToken);
        Task<bool> HasRelatedCourse(int courseId, CancellationToken cancellationToken);




       // Task IncrementParentDirectChildCount(int courseGroupId, CancellationToken cancellationToken);
        //Task DecrementParentDirectChildCount(int courseGroupId, CancellationToken cancellationToken);
        //Task<bool> HasChildCourseGroup(int courseGroupId, CancellationToken cancellationToken);
       // Task<string> GenerateLinage(int courseGroupId, CancellationToken cancellationToken);
       // Task SetLinage(string linage, int courseGroupId, CancellationToken cancellationToken);
        //Task<List<CourseGroup>> GetAllChildren(int courseGroupId, CancellationToken cancellationToken);
        //Task<bool> GenerateAndSetLineageOnUpdateParentIdForNodeAndItsChildren(int courseGroupId, int? newParentID, CancellationToken cancellationToken);
    }
}
