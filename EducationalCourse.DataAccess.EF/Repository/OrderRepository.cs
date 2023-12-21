using Azure.Core;
using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.Models.Account;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        //********************************** GetCoureseForUserOrder **************************
        public async Task<Course> GetCoureseForUserOrder(int courseId, CancellationToken cancellationToken)
        {
            return await _context.Courses.Where(x => x.Id == courseId && x.IsDelete == false)
                                                .FirstOrDefaultAsync(cancellationToken);

        }

        //********************************** GetUserOpenOrder *******************************
        public async Task<Order> GetUserOpenOrder(int userId, CancellationToken cancellationToken)
        {
            return await _context.Orders
                                 .Where(x => x.UserId == userId)
                                 .Where(x => !x.IsFinally)
                                 .Include(x => x.OrderDetails)
                                 .FirstOrDefaultAsync(cancellationToken);
        }

        //********************************** GetOrderForUserPannel **************************
        public async Task<Order> GetOrderForUserPannel(int userId, int orderId, CancellationToken cancellationToken)
        {
            return await _context.Orders
                             .Where(x => x.Id == orderId && x.UserId == userId)
                             .Include(x => x.OrderDetails)
                             .FirstOrDefaultAsync(cancellationToken);

        }

        //********************************** GetUserOrder ***********************************
        public async Task<Order> GetUserOrder(int userId, int orderId, CancellationToken cancellationToken)
        {
            return await _context.Orders
                              .Include(x => x.OrderDetails)
                              .ThenInclude(x => x.Course)
                              .Where(x => x.UserId == userId)
                              .Where(x => x.Id == orderId)
                              .FirstOrDefaultAsync(cancellationToken);

        }

        //********************************** GetOrderById ***********************************
        public async Task<Order> GetOrderById(int orderId, CancellationToken cancellationToken)
        {
            return await _context.Orders
                              .Where(x => x.Id == orderId)
                              .Where(x => x.IsActive)
                              .Where(x => !x.IsFinally)
                              .Include(x => x.OrderDetails)
                              .SingleOrDefaultAsync(cancellationToken);

        }

        //********************************** GetUserOrders ***********************************
        public async Task<List<Order>> GetUserOrders(int userId, CancellationToken cancellationToken)
        {
            return await _context.Orders
                                 .Where(x => x.UserId == userId)
                                 .Where(x => x.IsActive)
                                 .Where(x => x.IsFinally)
                                 .Include(x => x.OrderDetails)
                                 .ToListAsync(cancellationToken);
        }
    }
}

