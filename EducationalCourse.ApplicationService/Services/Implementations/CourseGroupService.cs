using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.CustomException;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class CourseGroupService : ICourseGroupService
    {
        #region Constructor

        private readonly ICourseGroupRepository _courseGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CourseGroupService(ICourseGroupRepository courseGroupRepository, IUnitOfWork unitOfWork)
        {
            _courseGroupRepository = courseGroupRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        //************************************* LoadCourseGroupParentChaild *************************
        public async Task<ApiResult<List<CourseGroupDto>>> GetAllCourseGroups(CancellationToken cancellationToken)
        {
            var courseGroupList = await _courseGroupRepository.FetchIQueryableEntity().Where(x => x.IsActive).ToListAsync(cancellationToken);
            var result = new List<CourseGroupDto>();

            if (courseGroupList is null)
                throw new Exception("گروهی یافت نشد ");

            result = LoadCourseGroupParentChild(result, courseGroupList);

            return new ApiResult<List<CourseGroupDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");
        }

        //************************************* LoadCourseGroupParentChaild *************************
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

        //************************************* LoadChildren ****************************************
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

        //************************************* AddParent *******************************************
        public async Task<ApiResult> AddParent(AddParentCourseGroupDto request, CancellationToken cancellationToken)
        {
            if (request.CourseGroupTitle is null)
                throw new AppException("عنوان نمی تواند خالی باشد");

            if (await _courseGroupRepository.ExistCourseGroupName(request.CourseGroupTitle, cancellationToken))
            {
                throw new AppException("عنوان نمی تواند تکراری باشد");

            }
            CourseGroup courseGroup = new CourseGroup
            {
                CourseGroupTitle = request.CourseGroupTitle,
                ParentId = null,
            };

            await _courseGroupRepository.AddAsync(courseGroup, cancellationToken);
            var result = (await _unitOfWork.SaveChangesAsync(cancellationToken))> default(int);

            if(!result)
                throw new AppException("عملیات ثبت خبر در دیتابیس با خطا مواجه شد");


            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //************************************* AddChild ********************************************
        public async Task<ApiResult> AddChild(AddChildCourseGroupDto request, CancellationToken cancellationToken)
        {
            if (request.CourseGroupTitle is null)
                throw new AppException("عنوان نمی تواند خالی باشد");

            if (await _courseGroupRepository.ExistCourseGroupName(request.CourseGroupTitle, cancellationToken))
            {
                throw new AppException("عنوان نمی تواند خالی باشد");

            }

            CourseGroup courseGroup = new CourseGroup
            {
                CourseGroupTitle = request.CourseGroupTitle,
                Id = request.ParentId,
            };

            await _courseGroupRepository.AddAsync(courseGroup, cancellationToken);
            var result = (await _unitOfWork.SaveChangesAsync(cancellationToken)) > default(int);

            if (!result)
                throw new AppException("عملیات ثبت خبر در دیتابیس با خطا مواجه شد");

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }
    }

}
