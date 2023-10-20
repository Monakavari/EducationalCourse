using EducationalCourse.Domain.Models.Base;


namespace EducationalCourse.Domain.Models.Account
{
    public class User : BaseEntity
    {
        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string AvatarName { get; set; }
        public string AvatarBase64 { get; set; }
        public string ActiveCode { get; set; }

        #endregion  Properties
    }
}
