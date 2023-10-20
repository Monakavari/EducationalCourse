using Microsoft.AspNetCore.Http;

namespace EducationalCourse.Common.Dtos.User
{
    public class EditAccountUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LasttName { get; set; }
        public string AvatarName { get; set; }
    }
}
