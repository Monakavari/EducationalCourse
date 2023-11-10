using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Domain.Dtos;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class PrepareForViewService : IPrepareForViewService
    {
        #region Constructor

        private readonly ICourseGroupRepository _courseGroupRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICourseLevelRepository _courseLevelRepository;
        private readonly ICourseStatusRepository _courseStatusRepository;
        public PrepareForViewService(ICourseEpisodeRepository episodeRepository,
                             ICourseGroupRepository courseGroupRepository,

                             IUserRepository userRepository,
                             ICourseLevelRepository courseLevelRepository,
                             ICourseStatusRepository courseStatusRepository)
        {

            _userRepository = userRepository;
            _courseLevelRepository = courseLevelRepository;
            _courseStatusRepository = courseStatusRepository;
            _courseGroupRepository = courseGroupRepository;
        }

        #endregion Constructor

        public async Task<ApiResult<PrepareForViewDto>> GetCourseInfoToFillCombos(CancellationToken cancellationToken)
        {
            PrepareForViewDto result = new PrepareForViewDto();

            result.User = await GetAllUsers(cancellationToken);
            result.CourseLevel = await GetAllCourseLevels(cancellationToken);
            result.CourseStatus = await GetAllCourseStatus(cancellationToken);
            result.CourseGroup = await GetAllCourseGroups(cancellationToken);

            return new ApiResult<PrepareForViewDto>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");
        }

        #region Private methods

        private async Task<List<UserDto>> GetAllUsers(CancellationToken cancellationToken)
        {
            return await _userRepository.FetchIQueryableEntity()
                                        .Select(x => new UserDto
                                        {
                                            Id = x.Id,
                                            FirstName = x.FirstName,
                                            LastName = x.LastName

                                        }).ToListAsync(cancellationToken);
        }

        private async Task<List<CourseLevelDto>> GetAllCourseLevels(CancellationToken cancellationToken)
        {
            return await _courseLevelRepository.FetchIQueryableEntity()
                                               .Select(x => new CourseLevelDto
                                               {
                                                   Id = x.Id,
                                                   Title = x.Title

                                               }).ToListAsync(cancellationToken);
        }

        private async Task<List<CourseStatusDto>> GetAllCourseStatus(CancellationToken cancellationToken)
        {
            return await _courseStatusRepository.FetchIQueryableEntity()
                                                .Select(x => new CourseStatusDto
                                                {
                                                    Id = x.Id,
                                                    Title = x.Title

                                                }).ToListAsync(cancellationToken);
        }

        private async Task<List<CourseGroupDto>> GetAllCourseGroups(CancellationToken cancellationToken)
        {
            var courseGroupList = await _courseGroupRepository.FetchIQueryableEntity()
                                                              .Where(x => x.IsActive)
                                                              .ToListAsync(cancellationToken);
            var result = new List<CourseGroupDto>();

            if (courseGroupList is null)
                throw new Exception("گروهی یافت نشد ");

            result = LoadCourseGroupParentChild(result, courseGroupList);

            return result;

        }

        private List<CourseGroupDto> LoadChildren(List<CourseGroup> courseGroupList, int parentId)
        {
            var children = new List<CourseGroupDto>();

            foreach (var courseGroup in courseGroupList.Where(x => x.ParentId == parentId))
            {
                children.Add(new CourseGroupDto
                {
                    Id = courseGroup.Id,
                    Title = courseGroup.CourseGroupTitle,
                    ParentId = courseGroup.ParentId,
                    Children = LoadChildren(courseGroupList, courseGroup.Id)
                });
            }
            return children;
        }

        private List<CourseGroupDto> LoadCourseGroupParentChild(List<CourseGroupDto> request, List<CourseGroup> courseGroupList)
        {
            foreach (var courseGroup in courseGroupList.Where(x => x.ParentId is null))
            {
                request.Add(new CourseGroupDto
                {
                    Id = courseGroup.Id,
                    Title = courseGroup.CourseGroupTitle,
                    ParentId = courseGroup.ParentId,
                    Children = LoadChildren(courseGroupList, courseGroup.Id)
                });
            }
            return request;
        }

        #endregion Private methods
    }
}
