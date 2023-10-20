namespace EducationalCourse.Domain.Models.Account;

public class LoginResponseDto
{
    public LoginResponseDto(User user, string token)
    {
        Id = user.Id;
        Email = user.Email;
        Token = token;
        Mobile = user.Mobile;
        FirstName = user.FirstName;
        LastName = user.LastName;

    }
    public int Id { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string Mobile { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

}
