using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace OvamPdfPoc.Templates;

public class PdfRenderService(IConverter pdfConverter)
{
    public byte[] Convert(string html, Orientation orientation, string headerPath = "", string footerPath = "", double? marginBottom = null)
    {
        var pdfDocument = new HtmlToPdfDocument
        {
            GlobalSettings = new GlobalSettings
            {
                Margins = new MarginSettings
                {
                    Bottom = marginBottom,
                    Unit = Unit.Centimeters
                },
                ColorMode = ColorMode.Color,
                Orientation = orientation,
                PaperSize = PaperKind.A4,
            },
            Objects =
            {
                new ObjectSettings
                {
                    HtmlContent = html,
                    PagesCount = true,
                    WebSettings = { DefaultEncoding = "utf-8" },

                    HeaderSettings =
                    {
                        FontSize = 6,
                        HtmlUrl = headerPath,
                        Line = false,
                        Spacing = 0.0
                    },

                    FooterSettings =
                    {
                        FontSize = 6,
                        Center = "[page]/[toPage]",
                        HtmlUrl = footerPath,
                        Line = false,
                        Spacing = 0.0
                    }
                }
            }
        };

        return pdfConverter.Convert(pdfDocument);
    }
}