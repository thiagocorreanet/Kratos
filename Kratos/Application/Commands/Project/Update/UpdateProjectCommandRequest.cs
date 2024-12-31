using MediatR;

namespace Application.Commands.Project.Update;

public class UpdateProjectCommandRequest : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Core.Entities.Project ToEntity(UpdateProjectCommandRequest request)
    {
        return new Core.Entities.Project(Id = request.Id, Name = request.Name);
    }
}