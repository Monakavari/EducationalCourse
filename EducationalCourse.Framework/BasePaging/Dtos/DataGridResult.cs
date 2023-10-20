namespace EducationalCourse.Framework.BasePaging.Dtos
{
    public class DataGridResult<T>
    {
        public List<T> Data { get; set; }
        public int Total { get; set; }
    }
}
