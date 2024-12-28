using MediatR;

namespace Application.Commands.TypeData.Delete;

public class DeleteTypeDataCommandRequest : IRequest<bool>
{
    public DeleteTypeDataCommandRequest(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}