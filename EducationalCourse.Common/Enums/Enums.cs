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
}
