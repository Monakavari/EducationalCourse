﻿using EducationalCourse.Common.Dtos;
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
        Task<ApiResult<CourseSinglePageDto>> GetCourseSinglePageInfo(CancellationToken cancellationToken);
        Task<ApiResult<List<FilterCourseDto>>> GetLastCourses(CancellationToken cancellationToken);
        Task<ApiResult<List<FilterCourseDto>>> GetPopularCourses(CancellationToken cancellationToken);
        Task<ApiResult> CreateCourse(AddCourseDto request, CancellationToken cancellationToken);
        Task<ApiResult> EditCourse(EditCourseDto request, CancellationToken cancellationToken);
        Task<ApiResult> Delete(int id, CancellationToken cancellationToken);
        Task<DataGridResult<FilterCourseResponseDto>> GetFilterCourses(FilterCourseRequestDto request, CancellationToken cancellationTokenationToken);
        Task<DataGridResult<FilterArchivedCoursesResponseDto>> GetFilterArchivedCourses(FilterArchivedCoursesRequestDto request, CancellationToken cancellationTokenationToken);
    }
}
