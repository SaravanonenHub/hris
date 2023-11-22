using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;

namespace API.Extensions
{
    public static class MYJPIF
    {
        public static NewtonsoftJsonPatchInputFormatter GetJsonInputFormatter()
        {
            var builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                    .GetRequiredService<IOptions<MvcOptions>>()
                    .Value
                    .InputFormatters
                    .OfType<NewtonsoftJsonPatchInputFormatter>()
                    .First();
        }
    }
}
