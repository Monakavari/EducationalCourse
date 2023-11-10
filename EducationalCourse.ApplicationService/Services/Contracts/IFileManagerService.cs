using EducationalCourse.Common.Enums;
using EducationalCourse.Domain.Dtos.FileManager;
using Microsoft.AspNetCore.Http;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface IFileManagerService
    {
        SaveImageDto SaveImage(FileTypeEnum fileType, IFormFile formFile, string directoryName, string? oldImageName = null);

        string SaveFile(FileTypeEnum fileType, IFormFile file, string directoryName,string? oldFileName = null);

        void DeleteFile(FileTypeEnum fileType, string fileName, string directoryName);
    }
}
