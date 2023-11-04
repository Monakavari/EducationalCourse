using Azure.Core;
using EducationalCourse.Common.Dtos.User;
using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Models.Account;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly EducationalCourseContext _dbContext;
        public UserRepository(EducationalCourseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //************************************GetUserByActiveCode*******************************
        public async Task<User> GetUserByActiveCode(string activeCode, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Users
                         .Where(x => x.ActiveCode.Equals(activeCode))
                         .SingleOrDefaultAsync(cancellationToken);

            return result;
        }

        //************************************GetUserByEmailOrMobile****************************
        public async Task<User> GetUserByEmailOrMobile(LoginRequestDto request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Users
                               .Where(x => x.IsActive)
                               .Where(x => x.Email.Equals(request.UserName) ||
                                x.Mobile.Equals(request.UserName))
                               .SingleOrDefaultAsync(cancellationToken);

            return result;
        }

        //************************************GetUserByUserName*********************************
        public async Task<User> GetUserByUserName(string userName, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Users
                               .Where(x => x.Email.Equals(userName) ||
                                x.Mobile.Equals(userName))
                               .SingleOrDefaultAsync(cancellationToken);

            return result;
        }

        //************************************IsExistUser***************************************
        public async Task<bool> IsExistUser(SignUpDto request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Users
                               .Where(x => x.Email.Equals(request.Email) ||
                               x.Mobile.Equals(request.Mobile))
                               .AnyAsync(cancellationToken);

            return result;
        }
    }
}
