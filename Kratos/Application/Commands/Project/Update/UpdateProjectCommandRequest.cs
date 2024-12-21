using MediatR;

namespace Application.Commands.Project.Update;

public class UpdateProjectCommandRequest : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; }
}