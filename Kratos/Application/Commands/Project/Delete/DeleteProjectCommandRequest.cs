using MediatR;

namespace Application.Commands.Entitie.Delete;

public class DeleteProjectCommandRequest : IRequest<bool>
{
    public DeleteProjectCommandRequest(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}