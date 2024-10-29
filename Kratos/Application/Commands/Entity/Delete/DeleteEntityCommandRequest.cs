using MediatR;

namespace Application.Commands.Entitie.Delete
{
    public class DeleteEntityCommandRequest : IRequest<bool>
    {
        public DeleteEntityCommandRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
