using MAV.Cms.Business.Exceptions;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Extensions;
using MAV.Cms.Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAV.Cms.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                Dictionary<string, string> errors = new Dictionary<string, string>();
                foreach (var item in ex.Errors)
                {
                    errors.Add(item.Key, string.Join(",", item.Value));
                }

                var response = new ApiException(StatusCodes.Status422UnprocessableEntity, LangManager.Translate("Error.ValidationError"), errors: errors);
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                ApiException response = _env.IsDevelopment()
                    ? new ApiException(StatusCodes.Status500InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiException(StatusCodes.Status500InternalServerError);

                switch (ex.Message)
                {
                    case "400":
                        response = new ApiException(StatusCodes.Status400BadRequest, ex.InnerException != null && ex.InnerException.Message.HasValue() ? ex.InnerException.Message : null);
                        break;
                    case "401":
                        response = new ApiException(StatusCodes.Status401Unauthorized, ex.InnerException != null && ex.InnerException.Message.HasValue() ? ex.InnerException.Message : null);
                        break;
                    case "403":
                        response = new ApiException(StatusCodes.Status403Forbidden, ex.InnerException != null && ex.InnerException.Message.HasValue() ? ex.InnerException.Message : null);
                        break;
                    case "404":
                        response = new ApiException(StatusCodes.Status404NotFound, ex.InnerException != null && ex.InnerException.Message.HasValue() ? ex.InnerException.Message : null);
                        break;
                    default:
                        response = new ApiException(StatusCodes.Status400BadRequest, ex.InnerException != null && ex.InnerException.Message.HasValue() ? ex.InnerException.Message : null);
                        break;
                }

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
