using EducationalCourse.Common.Enums;
using EducationalCourse.Domain.Models.Account;
using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public Wallet()
        {
            WalletTransactions = new List<WalletTransaction>();
        }
        public int Amount { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<WalletTransaction> WalletTransactions { get; set; }

    }
}
