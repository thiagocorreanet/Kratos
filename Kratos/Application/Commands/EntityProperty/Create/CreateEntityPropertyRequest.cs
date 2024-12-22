using MediatR;

namespace Application.Commands.EntityProperty.Create;

public class CreateEntityPropertyRequest : IRequest<bool>
{
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public bool IsRequired { get; set; }
    public int QuantityCaracter { get; set; }
    public int EntityId { get; set; }

    public Core.Entities.EntityProperty ToEntitie(CreateEntityPropertyRequest request)
    {
        return new Core.Entities.EntityProperty(
                Name = request.Name,
                Type = request.Type,
                IsRequired = request.IsRequired,
                EntityId = request.EntityId,
                QuantityCaracter = request.QuantityCaracter
            );
    }
}

