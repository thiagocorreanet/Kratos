using MediatR;

namespace Application.Commands.EntityProperty.Delete;

public class DeleteEntityPropertyRequest : IRequest<bool>
{
    public DeleteEntityPropertyRequest(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}