using EducationalCourse.Common.Enums;
using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities
{
    public class WalletTransaction : BaseEntity
    {
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public WalletTypeEnum WalletType { get; set; }
    }
}
