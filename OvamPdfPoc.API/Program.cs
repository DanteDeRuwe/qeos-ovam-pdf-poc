using Microsoft.AspNetCore.Mvc;
using OvamPdfPoc.API.Fakers;
using OvamPdfPoc.Models.Interfaces;
using OvamPdfPoc.Models.Models;
using OvamPdfPoc.Templates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPdfRenderingServices();

var app = builder.Build();

app.MapGet("pdf/test", async ([FromServices] IOvamPdfGenerator generator) =>
{
    var form = FakeOvamIdentificationFormFactory.Generate();
    var model = new OvamPdfModel(form);

    var bytes = await generator.Generate(model);

    var now = DateTimeOffset.UtcNow;
    const string language = "nl";
    var filename = $"{now:yyyyMMdd}__{form.Id}_{language}.pdf";

    return Results.File(bytes, "application/pdf", filename, false, now);
});

app.Run();