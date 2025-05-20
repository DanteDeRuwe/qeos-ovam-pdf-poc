namespace OvamPdfPoc.Models.Models;

public record OvamIdentificationForm
{
    public string? Id { get; init; }
    public Guid Uuid { get; init; }
    public DateTimeOffset TransportDate { get; init; }

    public FormWasteProducer WasteProducer { get; init; }
    public FormCollectorTraderBroker? CollectorTraderBroker { get; init; }
    public FormCarrier[] Carriers { get; init; }
    public FormTreatmentFacility TreatmentFacility { get; init; }
    public FormArticle[] Waste { get; init; }
    public FormLocation CollectionLocation { get; init; }
    public FormLocation DeliveryLocation { get; init; }
    public FormSignature SignatureResponsible { get; init; }
    public FormSignature SignatureReceiver { get; init; }
    public string? TransportInstructions { get; init; }
    public string? ReasonDiscontinuation { get; init; }
    public string Status { get; init; }
    public DateTimeOffset TmestampLastModification { get; init; }
    public FormMetadata Metadata { get; init; }
    public string SchemaVersion = "1.0";
    public FormProprietaryProperties? ProprietaryProperties { get; init; }
}

public record IdentificationFormRelation
{
    public string Name { get; set; }
    public IdentificationFormAddress Address { get; set; }
    public IdentificationNumbers IdentificationNumber { get; set; }
}

public record IdentificationNumbers
{
    public string? EnterpriseNumber { get; set; }
    public string? VatNumber { get; set; }
    public string? EstablishmentUnitNumber { get; set; }
    public string? SeaShipIMONumber { get; set; }
    public string? SeaShipMMSINumber { get; set; }
    public string? Citizen { get; set; }
}

public record IdentificationFormAddress
{
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string? MailboxNumber { get; set; }
    public string PostalCode { get; set; }
    public string Municipality { get; set; }
    public string Country { get; set; }
}

public record IdentificationFormLocation
{
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string MailboxNumber { get; set; }
    public string PostalCode { get; set; }
    public string Municipality { get; set; }
    public string Country { get; set; }
}

public record FormWasteProducer : IdentificationFormRelation
{
    public FormWasteProducerPickupLocation PickupLocation { get; set; }
}

public record FormWasteProducerPickupLocation
{
    public IdentificationFormAddress Address { get; set; }
    public string AddressExtension { get; set; }
    public IdentificationNumbers IdentificationNumber { get; set; }
}

public record FormCollectorTraderBroker : IdentificationFormRelation;

public record FormCarrier : IdentificationFormRelation
{
    public bool ExecutiveCarrier { get; set; }
}

public record FormTreatmentFacility : IdentificationFormRelation;

public record FormArticle
{
    public string WasteId { get; set; }
    public string Description { get; set; }
    public FormArticleWeight Weight { get; set; }
    public string EuralCode { get; set; }
    public string? PhysicalProperties { get; set; }
    public string? ChemicalComposition { get; set; }
    public string? PackingMaterial { get; set; }
    public int? PackingCount { get; set; }
    public string[] TreatmentCode { get; set; }
    public string? TreatmentType { get; set; }
}

public record FormArticleWeight
{
    public double Quantity { get; set; }
    public string Unit { get; set; }
}

public record FormLocation
{
    public string[] Coordinates { get; set; }
    public string Type { get; init; } = "Point";
}

public record FormSignature
{
    public DateTimeOffset Date { get; init; }
    public string Name { get; init; }
    public string SignatureType { get; init; }
    public FormLocation? SignedAtLocation { get; init; }
    public string SystemIdentification { get; init; }
    public string? LinkAuditTrail { get; init; }
}

public record FormMetadata
{
    public int FormVersion { get; init; }
    public DateTimeOffset TimestampFormVersionIncrease { get; init; }
    public DateTimeOffset? IopTimestamp { get; init; }
    public int? QueueOffset { get; init; }
    public string[]? RemovedParties { get; init; }
}

public record IdentificationFormProprietaryProperties
{
    public string Comment { get; init; }
    public string Extras { get; init; }
    public string TransportReference { get; init; }
}

public record FormProprietaryProperties
{
    public IdentificationFormProprietaryProperties? Carrier { get; init; }
    public IdentificationFormProprietaryProperties? CollectorTraderBroker { get; init; }
    public string? Extras { get; init; }
    public IdentificationFormProprietaryProperties? TreatmentFacility { get; init; }
    public IdentificationFormProprietaryProperties? WasteProducer { get; init; }
}