using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Models.Course;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class CourseGroupService : ICourseGroupService
    {
        #region Constructor

        private readonly ICourseGroupRepository _courseGroupRepository;

        public CourseGroupService(ICourseGroupRepository courseGroupRepository)
        {
            _courseGroupRepository = courseGroupRepository;
        }

        public Task<ApiResult> AddCourseGroup()
        {
            throw new NotImplementedException();
        }

        #endregion Constructor

        //*************************************GetAllCourseGroups*****************************************
        public async Task<ApiResult<List<CourseGroupDto>>> GetAllCourseGroups(CancellationToken cancellationToken)

        {
            var result = new List<CourseGroupDto>();
            var entity = await _courseGroupRepository.GetCourseGroups();

            if (entity == null)
                throw new Exception("گروهی یافت نشد");

            GetAllParents(result, entity);
            //GetAllChildren(result, entity);

            return new ApiResult<List<CourseGroupDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");

        }

        private void GetAllParents(List<CourseGroupDto> result, List<CourseGroup> entity)
        {
            foreach (var item in entity.Where(x => x.ParentId == null))
            {
                result.Add(new CourseGroupDto
                {
                    id = item.Id,
                    Title = item.CourseGroupTitle,
                });
            }

        }

        //private void GetAllChildren(List<CourseGroupDto> result, List<CourseGroup> entity)
        //{
        //    var courseGroup = _courseGroupRepository.GetById();
        //    foreach (var item in entity.Where(x => x.Id= ))
        //}
    }
}
  
