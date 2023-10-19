using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Models.User
{
    public class User : BaseEntity
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AvatarName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ActiveCode { get; set; }

        #endregion  Properties
    }
}
