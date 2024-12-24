using MediatR;

namespace Application.Commands.Entity.Update
{
    public class UpdateEntityCommandRequest : IRequest<bool>
    {
        public int Id {get; set;}
        public string Name { get; set; } = null!;
        public int ProjectId {get; set;}

        public Core.Entities.Entity ToEntity(UpdateEntityCommandRequest request)
        {
            return new Core.Entities.Entity(Id = request.Id, Name = request.Name, ProjectId = request.ProjectId);
        }
    }
}