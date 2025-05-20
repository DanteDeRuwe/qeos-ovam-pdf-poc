using System.Globalization;
using Bogus;
using OvamPdfPoc.Models.Models;

namespace OvamPdfPoc.API.Fakers;

public static class FakeOvamIdentificationFormFactory
{
    public static OvamIdentificationForm Generate()
    {
        var faker = new Faker();
        var addressFaker = new Faker<IdentificationFormAddress>()
            .RuleFor(a => a.Street, f => f.Address.StreetAddress())
            .RuleFor(a => a.PostalCode, f => f.Address.ZipCode())
            .RuleFor(a => a.Municipality, f => f.Address.City())
            .RuleFor(a => a.HouseNumber, f => f.Address.BuildingNumber())
            .RuleFor(a => a.MailboxNumber, f => f.Random.Bool() ? f.Address.SecondaryAddress() : null)
            .RuleFor(a => a.Country, f => f.Address.Country());

        var companyIdNumbersFaker = new Faker<IdentificationNumbers>()
            .RuleFor(a => a.EnterpriseNumber, f => f.Random.Int(100000000, 999999999).ToString())
            .RuleFor(a => a.VatNumber, f => f.Random.Int(100000000, 999999999).ToString())
            .RuleFor(a => a.EstablishmentUnitNumber, f => f.Random.Int(100000000, 999999999).ToString())
            .RuleFor(a => a.SeaShipIMONumber, f => f.Random.Int(100000000, 999999999).ToString())
            .RuleFor(a => a.SeaShipMMSINumber, f => f.Random.Int(100000000, 999999999).ToString())
            .RuleFor(a => a.Citizen, _ => null);

        var carrierFaker = new Faker<FormCarrier>()
            .RuleFor(a => a.Name, f => f.Company.CompanyName())
            .RuleFor(a => a.Address, addressFaker.Generate())
            .RuleFor(a => a.IdentificationNumber, companyIdNumbersFaker.Generate());

        var wasteFaker = new Faker<FormArticle>()
            .RuleFor(a => a.Description, f => f.Commerce.Product())
            .RuleFor(a => a.ChemicalComposition, f => f.Lorem.Sentence(1, 3))
            .RuleFor(a => a.PhysicalProperties, f => f.Lorem.Sentence(1, 3))
            .RuleFor(a => a.PackingMaterial, f => f.Lorem.Sentence(1, 3))
            .RuleFor(a => a.EuralCode, f => f.Random.AlphaNumeric(10))
            .RuleFor(a => a.TreatmentType, f => f.Lorem.Sentence(1, 3))
            .RuleFor(a => a.TreatmentCode, f => Enumerable.Range(f.Random.Int(1, 5), f.Random.Int(1, 5)).Select(i => $"{f.PickRandom("R", "D")}{i}").ToArray())
            .RuleFor(a => a.PackingCount, f => f.Random.Number(1, 500))
            .RuleFor(a => a.Weight, f => new FormArticleWeight { Quantity = f.Random.Int(0, 999), Unit = "kg" })
            .RuleFor(a => a.WasteId, f => f.Random.Guid().ToString());

        var signatureFaker = new Faker<FormSignature>()
            .RuleFor(a => a.Name, f => f.Name.FullName())
            .RuleFor(a => a.Date, f => f.Date.PastOffset())
            .RuleFor(a => a.Name, f => f.Name.FullName())
            .RuleFor(a => a.SignedAtLocation,
                f => new FormLocation
                {
                    Coordinates =
                    [
                        f.Address.Latitude().ToString(CultureInfo.InvariantCulture), f.Address.Longitude().ToString(CultureInfo.InvariantCulture)
                    ]
                });


        return new OvamIdentificationForm
        {
            Id = $"SEOS-{faker.Random.Number(1, 100)}",
            WasteProducer = new FormWasteProducer
            {
                Name = faker.Company.CompanyName(),
                Address = addressFaker.Generate(),
                IdentificationNumber = faker.Random.Bool()
                    ? new IdentificationNumbers
                    {
                        Citizen = faker.Name.FullName()
                    }
                    : companyIdNumbersFaker.Generate(),
                PickupLocation = new FormWasteProducerPickupLocation
                {
                    Address = addressFaker.Generate(),
                    IdentificationNumber = companyIdNumbersFaker.Generate(),
                }
            },
            TreatmentFacility = new FormTreatmentFacility
            {
                Name = faker.Company.CompanyName(),
                Address = addressFaker.Generate(),
                IdentificationNumber = companyIdNumbersFaker.Generate()
            },
            CollectorTraderBroker = new FormCollectorTraderBroker
            {
                Name = faker.Company.CompanyName(),
                Address = addressFaker.Generate(),
                IdentificationNumber = companyIdNumbersFaker.Generate()
            },
            Carriers =
                carrierFaker.Generate(faker.Random.Int(1, 3))
                    .Select((x, i) => i == 0 ? x with { ExecutiveCarrier = true } : x) // Set first carrier as executive
                    .ToArray(),

            TransportDate = faker.Date.PastOffset(),
            Waste = wasteFaker.Generate(faker.Random.Int(1, 3)).ToArray(),
            SignatureReceiver = signatureFaker.Generate(),
            SignatureResponsible = signatureFaker.Generate(),
        };
    }
}