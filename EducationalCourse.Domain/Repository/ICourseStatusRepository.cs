using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.Domain.Repository
{
    public interface ICourseStatusRepository:IBaseRepository<CourseStatus>
    {
    }
}
