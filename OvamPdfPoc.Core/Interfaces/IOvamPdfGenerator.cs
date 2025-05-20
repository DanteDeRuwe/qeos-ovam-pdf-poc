using OvamPdfPoc.Models.Models;

namespace OvamPdfPoc.Models.Interfaces;

public interface IOvamPdfGenerator
{
    Task<byte[]> Generate(OvamPdfModel model);
}