using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Enums;
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

        //************************ SaveImage ***********************
        public SaveImageDto SaveImage(FileTypeEnum fileType, IFormFile formFile, string directoryName, string? oldImageName = null)
        {
            string fileName = string.Empty;
            string base64String = string.Empty;

            if (formFile.Length > 0)
            {
                string path = GeneratePath(fileType, directoryName);
                fileName = UploadToServer(formFile, path, oldImageName);
                base64String = ToBase64String(formFile);
            }

            return new SaveImageDto()
            {
                AvatarBase64 = base64String,
                AvatarName = fileName,
            };
        }

        //************************ SaveFile ***********************
        public string SaveFile(FileTypeEnum fileType, IFormFile formFile, string directoryName, string? oldFileName = null)
        {
            string fileName = string.Empty;

            if (formFile.Length > 0)
            {
                string path = GeneratePath(fileType, directoryName);
                fileName = UploadToServer(formFile, path, oldFileName);
            }

            return fileName;
        }

        //************************ DeleteFile **********************
        public void DeleteFile(FileTypeEnum fileType, string fileName, string directoryName)
        {
            var filePath = Path.Combine(GeneratePath(fileType, directoryName), fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        #region PrivateMethod

        //********************* UploadToServer *********************
        private string UploadToServer(IFormFile formFile, string path, string oldFileName)
        {
            DeleteOldFile(oldFileName, path);
            string fileName = UploadNewFile(formFile, path);

            return fileName;
        }

        //********************** DeleteOldFile ********************
        private void DeleteOldFile(string oldFileName, string path)
        {
            if (!string.IsNullOrWhiteSpace(oldFileName))
            {
                var oldImagePath = Path.Combine(path, oldFileName);

                if (File.Exists(oldImagePath))
                    File.Delete(oldImagePath);
            }
        }

        //**********************UploadNewImage*********************
        private string UploadNewFile(IFormFile formFile, string path)
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

        //**********************ToBase64String*********************
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

        #endregion PrivateMethod

        #region GeneratePath

        private string GeneratePath(FileTypeEnum fileType, string directoryName)
        {
            string path = string.Empty;

            switch (fileType)
            {
                case FileTypeEnum.CourseImage:
                    path = $"{env.WebRootPath}/Images/Courses/{directoryName}";
                    break;

                case FileTypeEnum.UserImage:
                    path = $"{env.WebRootPath}/Images/UserProfile/{directoryName}";
                    break;

                case FileTypeEnum.DemoCourseVideo:
                    path = $"{env.WebRootPath}/Files/Courses/DemoVideos/{directoryName}";
                    break;

                case FileTypeEnum.CourseVideo:
                    path = $"{env.WebRootPath}/Files/Courses/CourseVideos/{directoryName}";
                    break;
            }

            return path;
        }

        #endregion GeneratePath
    }
}
