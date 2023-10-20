using Microsoft.AspNetCore.Http;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface IFileManagerService
    {
        Task SaveImage(IFormFile formFile, string imagePath);
    }
}
