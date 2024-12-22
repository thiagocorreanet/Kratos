using Application.Extension.GenerateAPI;
using Application.Extension.GenerateApplication;
using Application.Extension.GenerateCore;
using Application.Extension.GenerateInfrastructure;
using Application.Notification;
using Core.Repositories;

using Humanizer;

using MediatR;

namespace Application.Queries.EntitiyModel.GetByIdProperties;

public class QueryEntityGetByIdPropertiesHandler : BaseCQRS, IRequestHandler<QueryEntityGetByIdPropertiesRequest, string>
{
    private readonly IEntityRepository _repository;

    public QueryEntityGetByIdPropertiesHandler(INotificationError notificationError, IEntityRepository repository) : base(notificationError)
    {
        _repository = repository;
    }

    public async Task<string> Handle(QueryEntityGetByIdPropertiesRequest request, CancellationToken cancellationToken)
    {
        var getEntities = await _repository.GetAllEntityByIdAllPropertityAsync(request.Id);
        var convertClassForSingle = getEntities.Name.Singularize(true);

        var generateEntity = GenerateEntity.GenerateCodeEntity(getEntities, convertClassForSingle);
        var generateInterfaceRepository = GenerateInterfaceRepository.GenerateCodeInterfaceRepository(getEntities, convertClassForSingle);
        var generateInfrastructureConfiguration = GenerateInfrastructureConfiguration.GenerateCodeInfrastructureConfiguration(getEntities, convertClassForSingle);
        var generateInfrastructurePersistenceRepository = GenerateInfrastructureRepository.GenerateCodeInfrastructureRepository(convertClassForSingle);
        var generateInfrastructurePersistenceAndDI = GenerateInfrastructureDbContextAndDI.GenerateCodeInfrastructureDbContextAndDI(getEntities, convertClassForSingle);
        var generateApplicationCommandsCreate = GenerateApplicationCommandCreate.GenerateCodeApplicationCommandCreate(getEntities, convertClassForSingle);
        var generateApplicationCommandUpdateCode = GenerateApplicationCommandUpdate.GenerateCodeApplicationCommandUpdate(getEntities, convertClassForSingle);
        var generateApplicationCommandDeleteCode = GenerateApplicationCommandDelete.GenerateCodeApplicationCommandDelete(convertClassForSingle);
        var generateApplicationQueriesEntityGetAllResponse = GenerateApplicationQueryGetAllResponse.GenerateCodeApplicationQueryGetAllResponse(getEntities, convertClassForSingle);
        var generateApplicationQueriesEntityGetAllRequest = GenerateApplicationQueryGetAllRequest.GenerateCodeApplicationQueryGetAllRequest(getEntities, convertClassForSingle);
        var generateApplicationQueriesEntityGetAllHandler = GenerateApplicationQueryGetAllHandler.GenerateCodeApplicationQueryGetAllHandler(getEntities, convertClassForSingle);
        var generateApplicationQueriesEntityGetByIdRequest = GenerateApplicationQueryGetByIdRequest.GenerateCodeApplicationQueryGetByIdRequest(getEntities, convertClassForSingle);
        var generateApplicationQueriesEntityGetByIdResponse = GenerateApplicationQueryGetByIdResponse.GenerateCodeApplicationQueryGetByIdResponse(getEntities, convertClassForSingle);
        var generateApplicationQueriesEntityGetByIdHandler = GenerateApplicationQueryGetByIdHandler.GenerateCodeApplicationQueryGetByIdHandler(getEntities, convertClassForSingle);
        var generateApplicationValitorsCreate = GenerateApplicationValidatorCreate.GenerateCodeApplicationValidatorCreate(getEntities, convertClassForSingle);
        var generateApplicationValitorsUpdate = GenerateApplicationValidatorUpdate.GenerateCodeApplicationValidatorUpdate(getEntities, convertClassForSingle);
        var generateApiControllers = GenerateAPIEndpoint.GenerateCodeAPIEndpoint(getEntities, convertClassForSingle);

        return $"{generateEntity} \n {generateInterfaceRepository} \n {generateInfrastructureConfiguration} \n {generateInfrastructurePersistenceRepository} \n {generateInfrastructurePersistenceAndDI}" +
            $"\n {generateApplicationCommandsCreate} \n {generateApplicationCommandUpdateCode} \n {generateApplicationCommandDeleteCode} \n {generateApplicationQueriesEntityGetAllResponse} \n {generateApplicationQueriesEntityGetAllRequest} \n {generateApplicationQueriesEntityGetAllHandler}" +
            $"\n {generateApplicationQueriesEntityGetByIdRequest} \n {generateApplicationQueriesEntityGetByIdResponse} \n {generateApplicationQueriesEntityGetByIdHandler} \n {generateApplicationValitorsCreate} \n {generateApplicationValitorsUpdate} \n {generateApiControllers}";

    }






















}
