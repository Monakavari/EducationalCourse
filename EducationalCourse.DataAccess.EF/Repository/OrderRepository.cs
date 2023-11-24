using Azure.Core;
using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        #region Constructor

        private readonly EducationalCourseContext _context;
        public OrderRepository(EducationalCourseContext context) : base(context)
        {
            _context = context;
        }

        #endregion Constructor

        //**********************************GetCoureseForUserOrder**********************************
        public async Task<Course> GetCoureseForUserOrder(int courseId, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.Where(x => x.Id == courseId && x.IsDelete == false)
                                                .FirstOrDefaultAsync(cancellationToken);

            return course;
        }

        //**********************************GetUserOpenOrder*****************************************
        public async Task<Order> GetUserOpenOrder(int userId, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                    .Where(x => x.UserId == userId)
                    .Where(x => !x.IsFinally)
                    .Include(x => x.OrderDetails)
                    .FirstOrDefaultAsync(cancellationToken);

            return order;
        }

        //**********************************GetOrderForUserPannel************************************
        public async Task<Order> GetOrderForUserPannel(int userId, int orderId, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                             .Where(x => x.Id == orderId && x.UserId == userId)
                             .Include(x => x.OrderDetails)
                             .FirstOrDefaultAsync(cancellationToken);
            return order;
        }
    }
}

