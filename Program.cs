using CMS.MiddleWare;
using Microsoft.Extensions.Hosting;
var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(builder =>
{
    builder.UseMiddleware<ExceptionHandlingMiddleware>();
})
    .ConfigureServices(services =>
    {
        //Add services here
        // services.AddScoped
        //services.AddTransient
        //services.AddSingleton

    })
    .Build();