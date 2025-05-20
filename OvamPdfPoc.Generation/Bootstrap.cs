using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using OvamPdfPoc.Models.Interfaces;
using OvamPdfPoc.Templates.Converters;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace OvamPdfPoc.Templates;

public static class Bootstrap
{
    public static IServiceCollection AddPdfRenderingServices(this IServiceCollection services)
    {
        services.AddSingleton<ITools, PdfTools>();
        services.AddScoped<IConverter, BasicConverter>();
        services.AddScoped<HtmlToPdfConverter>();

        services.AddScoped<HtmlRenderer>(sp => new HtmlRenderer(sp, sp.GetService<ILoggerFactory>() ?? new NullLoggerFactory()));
        services.AddScoped<RazorToHtmlConverter>();

        services.AddScoped<IOvamPdfGenerator, OvamPdfGenerator>();
        return services;
    }
}