using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EducationalCourse.Framework
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new NotSupportedException();
            }

            return Enum.GetValues(input.GetType()).Cast<T>();
        }

        public static IEnumerable<T> GetEnumFlags<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new NotSupportedException();
            }

            foreach (object value in Enum.GetValues(input.GetType()))
            {
                if ((input as Enum).HasFlag(value as Enum))
                {
                    yield return (T)value;
                }
            }
        }

        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Argument value is NULL");
            }

            DisplayAttribute displayAttribute = value.GetType().GetField(value.ToString()).GetCustomAttributes<DisplayAttribute>(inherit: false)
                .FirstOrDefault();
            if (displayAttribute == null)
            {
                return value.ToString();
            }

            return displayAttribute.GetType().GetProperty(property.ToString())!.GetValue(displayAttribute, null)!.ToString();
        }

        public static Dictionary<int, string> ToDictionary(this Enum value)
        {
            return Enum.GetValues(value.GetType()).Cast<Enum>().ToDictionary((Enum p) => Convert.ToInt32(p), (Enum q) => q.ToDisplay());
        }
    }

}
