using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace EducationalCourse.Framework
{
    public class ApiResult
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, string message = null)
        {
            IsSuccess = isSuccess;
            StatusCode = (int)statusCode;
            Message = message ?? statusCode.ToDisplay();
        }

        public ApiResult(bool isSuccess, HttpStatusCode statusCode, string message = null)
        {
            IsSuccess = isSuccess;
            StatusCode = (int)statusCode;
            Message = message ?? statusCode.ToDisplay();
        }

        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(isSuccess: true, ApiResultStatusCode.Success);
        }

        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(isSuccess: false, ApiResultStatusCode.BadRequest);
        }

        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            Dictionary<string, string[]> source = (Dictionary<string, string[]>)result.Value!.GetType().GetProperty("Errors")!.GetValue(result.Value, null);
            string message = result.ToString();
            IEnumerable<string> enumerable = source.SelectMany((KeyValuePair<string, string[]> p) => p.Value).Distinct();
            if (enumerable.Count() > 0)
            {
                message = string.Join(" | ", enumerable);
            }

            return new ApiResult(isSuccess: false, ApiResultStatusCode.BadRequest, message);
        }

        public static implicit operator ApiResult(ContentResult result)
        {
            return new ApiResult(isSuccess: true, ApiResultStatusCode.Success, result.Content);
        }

        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(isSuccess: false, ApiResultStatusCode.NotFound);
        }
    }

}
