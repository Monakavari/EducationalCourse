using EducationalCourse.Domain.Models.Base;
using EducationalCourse.Framework.BasePaging.Dtos;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.Framework.Extensions
{
    public static class IQueryableApplyPagingExtension
    {
        public static async Task<DataGridResult<T>> ApplyPaging<T>(this IQueryable<T> expression, GridState request) where T : BaseEntity, new()
        {
            var result = new DataGridResult<T>();

            if (request is null)
                request = new GridState
                {
                    Take = 10,
                    Skip = 0,
                    Dir = "desc"
                };

            request.Skip = request.Skip - 1 * request.Take;

            result.Total = expression.Count();
            result.Data = await expression
                    .ApplyOrdering(request)
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .ToListAsync();

            return result;
        }

        private static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> expression, GridState request) where T : BaseEntity, new()
        {
            if (request.Dir.Contains("asc"))
                expression.OrderBy(c => c.CreateDate);
            else
                expression.OrderByDescending(c => c.CreateDate);

            return expression;
        }
    }
}
