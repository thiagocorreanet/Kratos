using MediatR;
namespace Application.Commands.Project.Create;

public class CreateProjectCommandRequest : IRequest<bool>
{
    public string Name { get; set; } = null!;

    public Core.Entities.Project ToEntity(CreateProjectCommandRequest request)
    {
        var toEntity = new Core.Entities.Project(Name = request.Name);

        return toEntity;
    }
}