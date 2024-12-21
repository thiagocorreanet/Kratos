using MediatR;
namespace Application.Commands.Project.Create;

public class CreateProjectCommandRequest : IRequest<bool>
{
    public string Name { get; set; } = null!;
}