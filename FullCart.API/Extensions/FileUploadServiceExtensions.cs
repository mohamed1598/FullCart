using Microsoft.AspNetCore.Http.Features;
using OfficeOpenXml;

namespace FullCart.API.Extensions
{
    public static class FileUploadServiceExtensions
    {
        public static IServiceCollection ConfigureFileUpload(this IServiceCollection services)
        {
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            return services;
        }

        
    }
}
