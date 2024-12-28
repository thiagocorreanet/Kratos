using System.ComponentModel;

namespace Core.Entities;

public class EntityProperty : BaseEntity
{
    protected EntityProperty()
    {}

    public EntityProperty(string name, int typeDataId, bool isRequired, int propertyMaxLength, bool isRequiredRel, int? entityId, string typeRel)
    {
        Name = name;
        TypeDataId = typeDataId;
        IsRequired = isRequired;
        PropertyMaxLength = propertyMaxLength;
        IsRequiredRel = isRequiredRel;
        EntityId = entityId;
        TypeRel = typeRel;
    }

    public string Name { get; private set; } 
    public int TypeDataId { get; private set; } 
    public bool IsRequired { get; private set; }
    public int PropertyMaxLength { get; private set; }
    public bool IsRequiredRel { get; private set; }
    public int? EntityId { get; private set; }
    public string TypeRel { get; private set; }

    // Relation property
    public Entity EntityRel { get; private set; } = null!;
    public TypeData TypeDataRel { get; private set; } = null!;
}