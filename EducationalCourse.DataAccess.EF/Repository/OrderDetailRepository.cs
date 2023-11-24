using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        #region Constructor

        private readonly EducationalCourseContext _context;
        public OrderDetailRepository(EducationalCourseContext context) : base(context)
        {
            _context = context;
        }

        #endregion Constructor

        public async Task<bool> IsExistOrderDetail(int OrderId,int courseId, CancellationToken cancellationToken)
        {
            var result = await _context.OrderDetails
                               .Where(x => x.OrderId == OrderId && x.CourseId == courseId)
                               .AnyAsync(cancellationToken);
            return result;

        }

    }
}
