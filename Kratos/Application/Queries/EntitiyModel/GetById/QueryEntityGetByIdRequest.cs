using MediatR;

namespace Application.Queries.Entitie.GetById
{
    public class QueryEntityGetByIdRequest : IRequest<QueryEntityGetByIdResponse>
    {
        public QueryEntityGetByIdRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
