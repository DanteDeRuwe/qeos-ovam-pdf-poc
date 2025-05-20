using OvamPdfPoc.Models.Interfaces;
using OvamPdfPoc.Models.Models;
using OvamPdfPoc.Templates.Converters;
using OvamPdfPoc.Templates.Templates;
using WkHtmlToPdfDotNet;

namespace OvamPdfPoc.Templates;

internal class OvamPdfGenerator(RazorToHtmlConverter razorToHtmlConverter, HtmlToPdfConverter htmlToPdfConverter) : IOvamPdfGenerator
{
    public async Task<byte[]> Generate(OvamPdfModel model)
    {
        var html = await razorToHtmlConverter.RenderAsync<Ovam>(b => b.WithParameter(x => x.Model, model));
        return htmlToPdfConverter.RenderAsync(html, Orientation.Portrait);
    }
}