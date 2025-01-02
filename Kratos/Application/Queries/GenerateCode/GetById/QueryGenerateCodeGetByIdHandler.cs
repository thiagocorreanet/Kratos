using System.Text;
using Antlr4.StringTemplate;
using Application.GenerateCode.Templates.Core.Abstract;
using Application.GenerateCode.Templates.Core.Entities;
using Application.Notification;
using Application.Queries.EntityProperty.GetAll;
using Core.Repositories;
using Humanizer;
using MediatR;

namespace Application.Queries.GenerateCode.GetById;

public class QueryGenerateCodeGetByIdHandler : BaseCQRS, IRequestHandler<QueryGenerateCodeGetByIdRequest, QueryGenerateCodeGetByIdResponse>
{
    private readonly IEntityPropertyRepository _propertyRepository;
    private readonly IEntityRepository _entityRepository;
    private readonly TemplateEntities _template;
    private readonly TemplateAbstract _templateAbstract;

    public QueryGenerateCodeGetByIdHandler(INotificationError notificationError, IEntityPropertyRepository propertyRepository, IEntityRepository entityRepository, TemplateEntities template, TemplateAbstract templateAbstract) : base(notificationError)
    {
        _propertyRepository = propertyRepository;
        _entityRepository = entityRepository;
        _template = template;
        _templateAbstract = templateAbstract;
    }

    public async Task<QueryGenerateCodeGetByIdResponse> Handle(QueryGenerateCodeGetByIdRequest request, CancellationToken cancellationToken)
    {
        var entity = await _entityRepository.GetByIdAsync(request.Id);
        if (entity == null)
        {
            Notify("Entity not found.");
            return null;
        }

        var properties = await _propertyRepository.GetAllEntitiesPropertiesByEntityIdAsync(request.Id);
        var responseProperties = request.ToResponse(properties.ToList());

        var className = entity.Name.Singularize(true);
        var codeTemplate = await _template.GenerateEntityTemplate(request, className, entity.Name, responseProperties);
        var codeTemplateAbstract = await _templateAbstract.GenerateAbstractTemplate(className);

        return new QueryGenerateCodeGetByIdResponse
        {
            Entities = codeTemplate.Render(),
            Abstract = codeTemplateAbstract.Render()
        };
    }

    
}


