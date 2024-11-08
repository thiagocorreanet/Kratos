using MediatR;

namespace Application.Commands.EntityProperty.Create;

public class CreateEntityPropertyRequest : IRequest<bool>
{
    public List<CreateEntityPropertyRequestItem> Items { get; init; } = new List<CreateEntityPropertyRequestItem>();
}

public class CreateEntityPropertyRequestItem
{
    public string Name { get; init; } = null!;
    public string Type { get; init; } = null!;
    public bool IsRequired { get; init; }
    public int QuantityCaracter { get; init; }
    public int EntityId { get; init; } 
}