using EducationalCourse.Common.Enums;

namespace EducationalCourse.Common.Dtos.Wallet
{
    public class ChargeWalletDto
    {
        public ChargeWalletDto()
        {
            this.IsPay = false;
        }
        public int Amount { get; set; }
        public WalletTypeEnum WalletType { get; set; }
        public string Description { get; set; }
        public bool IsPay { get; set; } // Port or Admin
    }
}
