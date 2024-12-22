using MediatR;

namespace Application.Commands.Entity.Create
{
    public class CreateEntityCommandRequest : IRequest<bool>
    {
        public string Name { get; set; } = null!;

        public Core.Entities.Entity ToEntity(CreateEntityCommandRequest request)
        {
            return new Core.Entities.Entity(Name = request.Name);
        }
    }
}