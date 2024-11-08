using MediatR;

namespace Application.Commands.EntityProperty.Update;

public class UpdateEntityPropertyRequest : IRequest<bool>
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Type { get; init; } = null!;
    public bool IsRequired { get; init; }
    public int QuantityCaracter { get; init; }
    public int EntityId { get; init; } 
}