using System.Drawing;
using System.Drawing.Imaging;

namespace EducationalCourse.Common.Utilities.Extensions.FileExtensions
{
    public static class ImageUploaderExtension
    {
        public static void AddImageToServer(this Image image, string fileName, string originPath, string deleteFileName = null)
        {
            if (image != null)
            {
                if (!Directory.Exists(originPath))
                    Directory.CreateDirectory(originPath);

                if (deleteFileName != null)
                    File.Delete(originPath + deleteFileName);

                string imageName = originPath + fileName;

                using (var stream = new FileStream(imageName, FileMode.Create))
                {
                    if (!Directory.Exists(imageName))
                        image.Save(stream, ImageFormat.Jpeg);
                }
            }
        }

        public static byte[] DecodeUrlBase64(string base64ToByte)
        {
            return Convert.FromBase64String(base64ToByte.Substring(base64ToByte.LastIndexOf(',') + 1));
        }

        public static Image Base64ToImage(string base64ToImage)
        {
            var stringTobyte = DecodeUrlBase64(base64ToImage);
            MemoryStream stream = new MemoryStream(stringTobyte, 0, stringTobyte.Length);
            stream.Write(stringTobyte, 0, stringTobyte.Length);
            Image image = Image.FromStream(stream, true);

            return image;
        }
    }
}
