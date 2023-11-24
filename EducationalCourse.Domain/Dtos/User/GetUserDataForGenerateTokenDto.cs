namespace EducationalCourse.Domain.DTOs.User
{
    public class GetUserDataForGenerateTokenDto
    {
        public GetUserDataForGenerateTokenDto()
        {
            RoleIds = new List<int>();
            PermisionIds = new List<int>();
        }

        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<int> RoleIds { get; set; }
        public List<int> PermisionIds { get; set; }
    }
}
