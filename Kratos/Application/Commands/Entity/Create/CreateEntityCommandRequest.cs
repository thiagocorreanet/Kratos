using MediatR;

namespace Application.Commands.Entity.Create
{
    public class CreateEntityCommandRequest : IRequest<bool>
    {
        public string Name { get; set; } = null!;
    }
}