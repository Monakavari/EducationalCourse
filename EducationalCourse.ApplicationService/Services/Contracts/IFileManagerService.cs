using EducationalCourse.Domain.Dtos.FileManager;
using Microsoft.AspNetCore.Http;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface IFileManagerService
    {
        SaveImageDto SaveImageCourse(IFormFile formFile, string courseName, string? oldImageCourseName = null);

        SaveImageDto SaveImageUserProfile(IFormFile formFile, string userName, string? oldImageName = null);

        string SaveFile(IFormFile file, string directoryName);

        List<string> SaveFiles(List<IFormFile> files, string directoryName);
    }
}
