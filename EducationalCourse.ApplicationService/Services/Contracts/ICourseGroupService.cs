using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Models.Course;
using EducationalCourse.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface ICourseGroupService 
    {
        //Task<ApiResult<List<CourseGroup>>> GetAllParents(CancellationToken cancellationToken);
        //Task<ApiResult<IEnumerable<CourseGroupDto>>> GetAllChildren(CancellationToken cancellationToken);
        Task<ApiResult<List<CourseGroupDto>>> GetAllCourseGroups(CancellationToken cancellationToken);
        Task<ApiResult> AddCourseGroup();
    }
}
