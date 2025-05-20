using Microsoft.Extensions.DependencyInjection;
using OvamPdfPoc.Models.Interfaces;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace OvamPdfPoc.Templates;

public static class Bootstrap
{
    public static IServiceCollection AddPdfRenderingServices(this IServiceCollection services)
    {
        services.AddSingleton<ITools, PdfTools>();
        services.AddScoped<IConverter, BasicConverter>();
        services.AddScoped<PdfRenderService>();
        services.AddScoped<IOvamPdfGenerator, OvamPdfGenerator>();
        return services;
    }
}