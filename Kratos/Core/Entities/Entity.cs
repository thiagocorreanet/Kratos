
namespace Core.Entities;
public class Entity : BaseEntity
{
    protected Entity()
    {}
    
    public Entity(string name, int projectId)
    {
        Name = name;
        ProjectId = projectId;
    }

    public Entity(int id, string name, int projectId) : base(id)
    {
        Name = name;
        ProjectId = projectId;
    }

    public string Name { get; private set; } = null!;
    public int ProjectId { get; private set; }
    
    // Relation entities
    public List<EntityProperty> PropertyRel { get; private set; } = new List<EntityProperty>();
    public Project ProjectRel { get; private set; } = null!;

}