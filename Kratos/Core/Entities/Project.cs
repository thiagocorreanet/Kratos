namespace Core.Entities;

public class Project : BaseEntity
{

    protected Project()
    { }

    public Project(string name)
    {
        Name = name;
    }

    public Project(int id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; private set; }
}