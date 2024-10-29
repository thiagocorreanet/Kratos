using MediatR;

namespace Application.Commands.Entity.Update
{
    public class UpdateEntityCommandRequest : IRequest<bool>
    {
        public int Id {get; init;}
        public string Name { get; init; } = null!;
    }
}