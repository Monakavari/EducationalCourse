﻿using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.CustomException;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class CourseService : ICourseServise
    {
        #region Constructor

        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        #endregion Constructor

        //********************************SearchCourseByTitle************************************
        public async Task<ApiResult<List<CourseDto>>> SearchCourseByTitle(string courseTitle, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(courseTitle) && courseTitle.Length < 4)
                throw new AppException("تعداد کاراکتر وارد شده باید بیشتر باشد");

            var result = await _courseRepository.GetAllCourse(courseTitle, cancellationToken);

            return new ApiResult<List<CourseDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد");
        }

        //********************************GetLastCourses************************************
        public async Task<ApiResult<List<CourseDto>>> GetLastCourses(CancellationToken cancellationToken)
        {
            var result = await _courseRepository.GetLastCourses(cancellationToken);

            if (result is null)
                throw new AppException("دوره ای یافت نشد");

            return new ApiResult<List<CourseDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");
        }

        //********************************GetPopularCourses************************************
        public async Task<ApiResult<List<CourseDto>>> GetPopularCourses(CancellationToken cancellationToken)
        {

            var result = await _courseRepository.GetPopularCourses(cancellationToken);

            if (result is null)
                throw new AppException("دوره ای یافت نشد");


            return new ApiResult<List<CourseDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");
        }
    }
}
