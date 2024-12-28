using MediatR;

namespace Application.Commands.TypeData.Update;

public class UpdateTypeDataCommandRequest : IRequest<bool>
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public Core.Entities.TypeData ToEntity(UpdateTypeDataCommandRequest request)
    {
        return new Core.Entities.TypeData(Id = request.Id, request.Name);
    }
}