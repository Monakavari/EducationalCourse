namespace EducationalCourse.Common.DTOs.Configurations
{
    public class TokenSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpiresInHours { get; set; }
    }
}
