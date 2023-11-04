namespace EducationalCourse.Framework.BasePaging.Dtos
{
    public class GridState
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public string Dir { get; set; }

        //[JsonIgnore]//برای اینکه فرانت نتواند این فیلد را ببیند
        //public DateTime CreateDate { get; set; }
    }
}
