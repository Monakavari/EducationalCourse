namespace EducationalCourse.Common.Enums
{
    public enum FileTypeEnum
    {
        UserImage,
        CourseImage,
        CourseVideo,
        DemoCourseVideo,
    }

    public enum OrderByEnum : int
    {
        Non = 0,
        MaxPrice = 1,
        MinPrice = 2,
        PopularCourse = 3,
        LastCourse = 4,
        CourseTime = 5,
        PublishTime = 6
    }

    public enum WalletTypeEnum : int
    {
        Credit = 2, //واریز به حساب
        Deposit = 3,//برداشت از حساب
    }
    public enum DiscountUseType : int
    {
        Success = 1,
        NotFound = 2,
        Finished = 3,
        Expired = 4,
        NotStarted= 5,
        Used=6
    }
    public enum PermissionCodeEnum : int
    {
        AddRole = 1
    }
}
