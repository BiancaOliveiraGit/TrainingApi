using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingApi.ErrorMiddleware;

public static class ErrorExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomExceptionMiddleware>();
    }
}
