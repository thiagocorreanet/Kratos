using MediatR;

namespace Application.Commands.EntityProperty.Create;

public class CreateEntityPropertyRequest : IRequest<bool>
{
    public string Name { get; set; } = null!;
    public int TypeDataId { get;  set; } 
    public bool IsRequired { get;  set; }
    public int QuantityCaracter { get;  set; }
    public bool IsRequiredRel { get;  set; }
    public int EntityId { get;  set; }
    public string TypeRel { get;  set; } = null!;
    public int? EntityIdRel { get;  set; }

    public Core.Entities.EntityProperty ToEntity(CreateEntityPropertyRequest request)
    {
        return new Core.Entities.EntityProperty(
               Name = request.Name, 
               TypeDataId = request.TypeDataId,
               IsRequired = request.IsRequired,
               QuantityCaracter = request.QuantityCaracter,
               IsRequiredRel = request.IsRequiredRel,
               EntityId = request.EntityId,
               TypeRel = request.TypeRel,
               EntityIdRel = request.EntityIdRel
            );
    }
}

