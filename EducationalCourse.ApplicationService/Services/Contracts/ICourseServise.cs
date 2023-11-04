using EducationalCourse.Common.Dtos;
using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.Domain.Dtos;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Framework;
using EducationalCourse.Framework.BasePaging.Dtos;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface ICourseServise
    {
        Task<ApiResult<List<FilterCourseDto>>> SearchCourseByTitle(string courseTitle, CancellationToken cancellationToken);
        Task<ApiResult<List<FilterCourseDto>>> GetLastCourses(CancellationToken cancellationToken);
        Task<ApiResult<List<FilterCourseDto>>> GetPopularCourses(CancellationToken cancellationToken);
        Task<ApiResult> CreateCourse(AddCourseDto request,CancellationToken cancellationToken);
        Task<ApiResult> EditCourse(AddCourseDto request,CancellationToken cancellationToken);
        Task<ApiResult> AddVideoFileToCourse(List<AddVideoFileCourseDto> request,CancellationToken cancellationToken);
        Task<DataGridResult<FilterCourseResponseDto>> GetFilterCourses(FilterCourseRequestDto request, CancellationToken cancellationTokenationToken);
    }
}
