using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using EducationalCourse.Framework.CustomException;
using System.Net;

namespace EducationalCourse.Framework.Infrastructure.Middelewares
{
    public class CustomExceptionHandlerMiddleware
    {
        #region Constructor

        private string message = null;
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
        ApiResultStatusCode apiStatusCode = ApiResultStatusCode.ServerError;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<CustomExceptionHandlerMiddleware> logger) : base()
        {
            Next = next;
            Env = env;
            Logger = logger;
        }

        protected RequestDelegate Next { get; }
        protected IWebHostEnvironment Env { get; }
        protected ILogger<CustomExceptionHandlerMiddleware> Logger { get; }

        #endregion Constructor

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (AppException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, AppException exception)
        {
            Logger.LogError(exception, exception.Message);
            httpStatusCode = exception.HttpStatusCode;
            apiStatusCode = exception.ApiStatusCode;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (Env.IsDevelopment())
            {
                dictionary.Add("Exception", exception.Message);
                dictionary.Add("StackTrace", exception.StackTrace);
                if (exception.InnerException != null)
                {
                    dictionary.Add("InnerException.Exception", exception.InnerException!.Message);
                    dictionary.Add("InnerException.StackTrace", exception.InnerException!.StackTrace);
                }

                if (exception.AdditionalData != null)
                {
                    dictionary.Add("AdditionalData", JsonConvert.SerializeObject(exception.AdditionalData));
                }

                message = JsonConvert.SerializeObject(dictionary.Values);
            }
            else
            {
                if (!string.IsNullOrEmpty(exception.AdditionalData?.ToString()))
                {
                    dictionary.Add("AdditionalData", exception.AdditionalData?.ToString());
                }

                dictionary.Add("Exception", exception.Message);
                message = string.Join(",", dictionary.Values);
            }

            await WriteToResponse(exception.HttpStatusCode, context);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Logger.LogError(exception, exception.Message);
            if (Env.IsDevelopment())
            {
                Dictionary<string, string> value = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    ["StackTrace"] = exception.StackTrace
                };
                message = JsonConvert.SerializeObject(value);
            }
            else
            {
                message = exception.Message;
            }

            await WriteToResponseAsync(context);
        }

        private async Task WriteToResponse(HttpStatusCode statusCode, HttpContext context)
        {
            if (context.Response.HasStarted)
            {
                throw new InvalidOperationException("The response has already started, the http status code middleware will not be executed.");
            }

            string text2 = JsonConvert.SerializeObject(new ApiResult(isSuccess: false, statusCode, message));
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(text2);
        }

        private async Task WriteToResponseAsync(HttpContext context)
        {
            if (context.Response.HasStarted)
            {
                throw new InvalidOperationException("The response has already started, the http status code middleware will not be executed.");
            }

            string text = JsonConvert.SerializeObject(new ApiResult(isSuccess: false, httpStatusCode, message));
            int statusCode2 = 500;
            if (apiStatusCode != 0)
            {
                statusCode2 = (int)apiStatusCode;
            }
            else if (httpStatusCode != 0)
            {
                statusCode2 = (int)httpStatusCode;
            }

            context.Response.StatusCode = statusCode2;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(text);
        }
    }
}
