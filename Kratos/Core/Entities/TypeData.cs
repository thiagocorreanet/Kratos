namespace Core.Entities;

public class TypeData : BaseEntity
{
    public TypeData()
    {}
    
    public TypeData(string name)
    {
        Name = name;
    }

    public TypeData(int id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; set; }
    
    // Relation with property entity
    public List<EntityProperty> PropertiesRel { get; set; } = new List<EntityProperty>();
}