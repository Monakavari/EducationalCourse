using EducationalCourse.ApplicationService.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class FileManagerService : IFileManagerService
    {
        #region Constractor

        private readonly IWebHostEnvironment env;

        public FileManagerService(IWebHostEnvironment env)
        {
            this.env = env;
        }

        #endregion Constractor

        public Task SaveImage(IFormFile formFile, string imagePath)
        {
            throw new NotImplementedException();
        }
    }
}
