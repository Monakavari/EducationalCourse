using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.Repository;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class DiscountRepository:BaseRepository<Discount>, IDiscountRepository
    {
        #region Constructor

        private readonly EducationalCourseContext context;

        public DiscountRepository(EducationalCourseContext context):base(context) 
        {
            this.context = context;
        }

        #endregion Constructor
    }
}
