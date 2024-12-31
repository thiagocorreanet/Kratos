using MediatR;

namespace Application.Commands.TypeData.Create;

public class CreateTypeDataCommandRequest : IRequest<bool>
{
    public string Name { get; set; } = null!;

    public Core.Entities.TypeData ToEntity(CreateTypeDataCommandRequest request)
    {
        return new Core.Entities.TypeData(Name = request.Name);
    }
}