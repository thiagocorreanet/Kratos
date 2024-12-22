
namespace Core.Entities;
public class Entity : BaseEntity
{
    protected Entity()
    {}
    
    public Entity(string name)
    {
        Name = name;
    }

    public Entity(int id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; private set; } = null!;
    
    // Relation entities
    public List<EntityProperty> PropertyRel { get; private set; } = new List<EntityProperty>();

}