using System.ComponentModel;

namespace Core.Entities;

public class EntityProperty : BaseEntity
{
    protected EntityProperty()
    {}
    
    public EntityProperty(string name, string type, bool isRequired, int entityId)
    {
        Name = name;
        Type = type;
        IsRequired = isRequired;
        EntityId = entityId;
    }
    
    public string Name { get; private set; } 
    public string Type { get; private set; } 
    public bool IsRequired { get; private set; }

    // Relation property
    public int EntityId { get; private set; }
    public Entity EntityRel { get; private set; } = null!;
}