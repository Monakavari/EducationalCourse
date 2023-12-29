using EducationalCourse.Common.Dtos.User;
using EducationalCourse.Framework.CustomException;
using System.Text.RegularExpressions;

namespace EducationalCourse.ApplicationService.Validations
{
    public static class AccountValidator
    {
        public static void SetValidators(SignUpDto request)
        {
            var validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            var validateMobilRegex = new Regex("^(?:-(?:[1-9](?:\\d{0,2}(?:,\\d{3})+|\\d*))|(?:0|(?:[1-9](?:\\d{0,2}(?:,\\d{3})+|\\d*))))(?:.\\d+|)$");

            if (string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.RePassword) ||
                string.IsNullOrWhiteSpace(request.Mobile))

                throw new AppException("پر کردن فیلدهای مربوطه اجباریست.");

            if (!validateEmailRegex.IsMatch(request.Email))
                throw new AppException("فرمت ایمیل نامعتبر است.");

            if (validateMobilRegex.IsMatch(request.Mobile))
            {
                if (request.Mobile.Length != 11)
                    throw new AppException("تعداد مجاز ارقام یازده رقم می باشد.");
            }
            else if (!validateMobilRegex.IsMatch(request.Mobile))
                throw new AppException("فرمت شماره موبایل معتبر نمیباشد.");

            if (!request.RePassword.Equals(request.Password))
                throw new AppException("رمزعبور و تکرارآن یکسان نمی باشند.");

        }
        public static void SetValidators(LoginRequestDto request)
        {
            var validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            var validateMobilRegex = new Regex("^(?:-(?:[1-9](?:\\d{0,2}(?:,\\d{3})+|\\d*))|(?:0|(?:[1-9](?:\\d{0,2}(?:,\\d{3})+|\\d*))))(?:.\\d+|)$");

            if (string.IsNullOrWhiteSpace(request.UserName) ||
                 string.IsNullOrWhiteSpace(request.Password))

                throw new AppException("پر کردن فیلدهای مربوطه اجباریست.");

            if (validateMobilRegex.IsMatch(request.UserName))
            {
                if (request.UserName.Length != 11)
                    throw new AppException("تعداد مجاز ارقام یازده رقم می باشد.");
            }
            else if (!validateEmailRegex.IsMatch(request.UserName))
                throw new AppException("فرمت ایمیل نامعتبر است.");
        }

        public static void SetValidators(RePasswordRequestDto request)
        {
            if (request.UserId <= default(int))
                throw new AppException("شناسه کاربری نمی تواند خالی باشد.");

            if (string.IsNullOrWhiteSpace(request.Password) ||
                    string.IsNullOrWhiteSpace(request.RePassword))

                throw new AppException("پر کردن فیلدهای مربوطه اجباریست.");

            if (!request.Password.Equals(request.RePassword))
                throw new AppException("رمزعبور و تکرارآن یکسان نمی باشند.");

        }

    }
}
