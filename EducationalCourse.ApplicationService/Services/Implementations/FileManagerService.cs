using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Utilities.Generator;
using EducationalCourse.Domain.Dtos.FileManager;
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

        //************************ SaveImageUserProfile ***********************
        public SaveImageDto SaveImageCourse(IFormFile formFile, string courseName, string? oldImageCourseName = null)
        {
            string fileName = string.Empty;
            string base64String = string.Empty;

            if (formFile.Length > 0)
            {
                string path = $"{env.WebRootPath}/Image/Courses/{courseName}";
                fileName = UploadToServer(formFile, path, oldImageCourseName);
                base64String = ToBase64String(formFile);
            }

            return new SaveImageDto()
            {
                AvatarBase64 = base64String,
                AvatarName = fileName,
            };
        }

        //************************ SaveImageUserProfile ***********************
        public SaveImageDto SaveImageUserProfile(IFormFile formFile, string userName, string? oldImageName = null)
        {
            string fileName = string.Empty;
            string base64String = string.Empty;

            if (formFile.Length > 0)
            {
                string path = $"{env.WebRootPath}/Image/UserProfile/{userName}";
                fileName = UploadToServer(formFile, path, oldImageName);
                base64String = ToBase64String(formFile);
            }

            return new SaveImageDto()
            {
                AvatarBase64 = base64String,
                AvatarName = fileName,
            };
        }

        //***********************************************
        private string UploadToServer(IFormFile formFile, string path, string oldImageName)
        {
            DeleteOldImage(oldImageName, path);
            string fileName = UploadNewImage(formFile, path);

            return fileName;
        }

        //***********************************************
        private void DeleteOldImage(string oldImageName, string path)
        {
            if (!string.IsNullOrWhiteSpace(oldImageName))
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), path, oldImageName);

                if (File.Exists(oldImagePath))
                    File.Delete(oldImagePath);
            }
        }
        //***********************************************

        private string UploadNewImage(IFormFile formFile, string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(formFile.FileName);
            var filePath = ($"{path}/{fileName}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            return fileName;
        }

        //***********************************************
        private string ToBase64String(IFormFile formFile)
        {
            string base64String = string.Empty;

            if (formFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    formFile.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    base64String = Convert.ToBase64String(fileBytes);
                }
            }

            return base64String;
        }

        //************************ SaveFile ***********************
        public string SaveFile(IFormFile file, string directoryName)
        {
            string path = $"{env.WebRootPath}/Files/Courses/{directoryName}";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName);
            var filePath = ($"{path}/{fileName}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }

        //************************ SaveFiles ***********************
        public List<string> SaveFiles(List<IFormFile> files, string directoryName)
        {
            var fileNames = new List<string>();
            string path = $"{env.WebRootPath}/Files/Courses/{directoryName}";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            foreach (var file in files)
            {
                var fileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName);
                var filePath = ($"{path}/{fileName}");
                fileNames.Add(fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            return fileNames;
        }
    }
}
