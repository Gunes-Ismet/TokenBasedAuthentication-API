using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SharedLibrary.DTO_s;
using SharedLibrary.Exceptions;

namespace SharedLibrary.Extensions
{
    public static class CustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (errorFeature != null)
                    {
                        var ex = errorFeature.Error;
                        ErrorDTO errorDTO = null;
                        if (ex is CustomException)
                        {
                            errorDTO = new ErrorDTO(ex.Message, true);
                        }
                        else
                        {
                            errorDTO = new ErrorDTO(ex.Message, false);
                        }
                        var response = Response<NoDataDTO>.Fail(errorDTO, 500);
                        await context.Response.WriteAsJsonAsync(response);
                    }
                });
            });
        }
    }
}
