using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.ICommandRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.Domain.Repository
{
    public interface IOrderDetailRepository:IBaseRepository<OrderDetail>
    {
        Task<bool> IsExistOrderDetail(int orderDetailId, int OrderId,CancellationToken cancellationToken);
    }
}
