using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EducationalCourse.Framework
{
    public class ApiResult<TData> : ApiResult
    {
        public TData Data { get; set; }

        public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, TData data, string message = null)
            : base(isSuccess, statusCode, message)
        {
            Data = data;
        }
    }
}
