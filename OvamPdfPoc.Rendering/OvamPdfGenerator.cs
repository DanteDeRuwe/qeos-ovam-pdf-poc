using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging.Abstractions;
using OvamPdfPoc.Models.Interfaces;
using OvamPdfPoc.Models.Models;
using WkHtmlToPdfDotNet;

namespace OvamPdfPoc.Templates;

public class OvamPdfGenerator(IServiceProvider sp, PdfRenderService pdfRenderService) : IOvamPdfGenerator
{
    public async Task<byte[]> Generate(OvamPdfModel model)
    {
        var htmlRenderer = new HtmlRenderer(sp, new NullLoggerFactory());

        var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var componentParameters = ParameterView.FromDictionary(new Dictionary<string, object?> { { nameof(Ovam.Model), model } });
            var htmlRoot = await htmlRenderer.RenderComponentAsync<Ovam>(componentParameters);
            return htmlRoot.ToHtmlString();
        });

        return pdfRenderService.Convert(html, Orientation.Portrait);
    }
}