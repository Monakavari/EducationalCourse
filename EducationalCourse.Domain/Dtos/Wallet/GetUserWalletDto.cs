using EducationalCourse.Common.Enums;

namespace EducationalCourse.Domain.Dtos.Wallet
{
    public class GetUserWalletDto
    {
        public int Amount { get; set; }
        public WalletTypeEnum WalletType { get; set; }
        public string Description { get; set; }
        public string CreatDateDisplay { get; set; }
    }
}
