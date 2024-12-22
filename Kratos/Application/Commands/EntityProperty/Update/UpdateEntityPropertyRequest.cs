using MediatR;

namespace Application.Commands.EntityProperty.Update;

public class UpdateEntityPropertyRequest : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public bool IsRequired { get; set; }
    public int QuantityCaracter { get; set; }
    public int EntityId { get; set; }

    public Core.Entities.EntityProperty ToEntity(UpdateEntityPropertyRequest request)
    {
        return new Core.Entities.EntityProperty
            (
                Id = request.Id,
                Name = request.Name,
                Type = request.Type,
                IsRequired = request.IsRequired,
                EntityId = request.EntityId,
                QuantityCaracter = request.QuantityCaracter
            );
    }
}