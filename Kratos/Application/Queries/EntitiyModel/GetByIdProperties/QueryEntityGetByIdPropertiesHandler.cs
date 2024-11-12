
using System.Text;

using Application.Notification;

using AutoMapper;

using Core.Entities;
using Core.Repositories;

using Humanizer;

using MediatR;

namespace Application.Queries.EntitiyModel.GetByIdProperties;

public class QueryEntityGetByIdPropertiesHandler : BaseCQRS, IRequestHandler<QueryEntityGetByIdPropertiesRequest, string>
{
    private readonly IEntityRepository _repository;

    public QueryEntityGetByIdPropertiesHandler(INotificationError notificationError, IMapper iMapper, IEntityRepository repository) : base(notificationError, iMapper)
    {
        _repository = repository;
    }

    public async Task<string> Handle(QueryEntityGetByIdPropertiesRequest request, CancellationToken cancellationToken)
    {
        var getEntities = await _repository.GetAllEntityByIdAllPropertityAsync(request.Id);
        var convertClassForSingle = getEntities.Name.Singularize(true);

        var generateEntity = GenerateEntity(getEntities, convertClassForSingle);
        var generateInterfaceRepository = GenerateInterfaceRepository(getEntities, convertClassForSingle);
        var generateInfrastructureConfiguration = GenerateInfrastructureConfiguration(getEntities, convertClassForSingle);
        var generateInfrastructurePersistenceAndDI = GenerateInfrastructureDbContextAndDI(getEntities, convertClassForSingle);
        var generateInfrastructurePersistenceRepository = GenerateInfrastructureRepository(getEntities, convertClassForSingle);
        var generateApplicationCommandsCreate = GenerateApplicationCommandCreate(getEntities, convertClassForSingle);
        var generateApplicationCommandUpdateCode = GenerateApplicationCommandUpdate(getEntities, convertClassForSingle);
        var generateApplicationCommandDeleteCode = GenerateApplicationCommandDelete(getEntities, convertClassForSingle);
        var generateApplicationMappings = GenerateApplicationMapping(getEntities, convertClassForSingle);
        var generateApplicationQueriesEntityGetAllResponse = GenerateApplicationQueryGetAllResponse(getEntities, convertClassForSingle);
        var generateApplicationQueriesEntityGetAllRequest = GenerateApplicationQueryGetAllRequest(getEntities, convertClassForSingle);
        var generateApplicationQueriesEntityGetAllHandler = GenerateApplicationQueryGetAllHandler(getEntities, convertClassForSingle);
        var generateApplicationQueriesEntityGetByIdResponse = GenerateApplicationQueryGetByIdResponse(getEntities, convertClassForSingle);
        var generateApplicationValitorsCreate = GenerateApplicationValidatorCreate(getEntities, convertClassForSingle);
        var generateApplicationValitorsUpdate = GenerateApplicationValidatorUpdate(getEntities, convertClassForSingle);

        return $"{generateEntity} \n \n {generateInterfaceRepository} \n \n {generateInfrastructureConfiguration} \n \n {generateInfrastructurePersistenceAndDI}, " +
            $"\n \n {generateInfrastructurePersistenceRepository},  \n \n {generateApplicationCommandsCreate},  \n \n {generateApplicationCommandUpdateCode}," +
            $"\n \n {generateApplicationCommandDeleteCode}, \n \n {generateApplicationMappings}, \n \n {generateApplicationQueriesEntityGetAllResponse}, \n \n {generateApplicationQueriesEntityGetAllRequest}, " +
            $"\n \n {generateApplicationQueriesEntityGetAllHandler}, \n \n {generateApplicationQueriesEntityGetByIdResponse}, \n \n {generateApplicationValitorsCreate}" +
            $", \n \n {generateApplicationValitorsUpdate}";

    }

    private static string GenerateEntity(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderEntity = new StringBuilder();

        stringBuilderEntity.AppendLine("////// Camada Core");
        stringBuilderEntity.AppendLine("////// Entities");
        stringBuilderEntity.AppendLine("namespace Core.Entities;");
        stringBuilderEntity.AppendLine();
        stringBuilderEntity.AppendLine($"public class {convertClassForSingle} : BaseEntity");
        stringBuilderEntity.AppendLine("{");
        stringBuilderEntity.AppendLine();

        stringBuilderEntity.AppendLine($"protected {convertClassForSingle}()");
        stringBuilderEntity.AppendLine("{ }");
        stringBuilderEntity.AppendLine();

        stringBuilderEntity.Append($"public {convertClassForSingle}(");
        stringBuilderEntity.Append(string.Join(", ", getEntities.PropertyRel.Select(p => $"{p.Type} {p.Name.ToLower()}")));
        stringBuilderEntity.AppendLine(")");
        stringBuilderEntity.AppendLine("{");
        foreach (var item in getEntities.PropertyRel)
        {
            stringBuilderEntity.AppendLine($"    {item.Name} = {item.Name.ToLower()};");
        }
        stringBuilderEntity.AppendLine("}");
        stringBuilderEntity.AppendLine();

        foreach (var item in getEntities.PropertyRel)
        {
            stringBuilderEntity.AppendLine($"public {item.Type} {item.Name} {{ get; private set; }}");
        }

        stringBuilderEntity.AppendLine("}");

        return stringBuilderEntity.ToString();
    }

    private static string GenerateInterfaceRepository(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderInterfaceRepository = new StringBuilder();

        stringBuilderInterfaceRepository.AppendLine("////// Repositories");
        stringBuilderInterfaceRepository.AppendLine();
        stringBuilderInterfaceRepository.AppendLine();
        stringBuilderInterfaceRepository.AppendLine("using Core.Entities;");
        stringBuilderInterfaceRepository.AppendLine();

        stringBuilderInterfaceRepository.AppendLine("namespace Core.Repositories;");
        stringBuilderInterfaceRepository.AppendLine();

        stringBuilderInterfaceRepository.AppendLine($"public interface I{convertClassForSingle}Repository : IBaseRepository<{convertClassForSingle}>");
        stringBuilderInterfaceRepository.AppendLine("{}");

        return stringBuilderInterfaceRepository.ToString();
    }

    private static string GenerateInfrastructureConfiguration(Entity getEntities, string convertClassForSingle)
    {
        string typeCustom = string.Empty;
        int counter = 2;
        var stringBuilderConfiguration = new StringBuilder();

        var mappingOfTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "string", "VARCHAR" },
            { "DateTime", "DATETIME2" },
            { "bool", "BIT" }
        };


        stringBuilderConfiguration.AppendLine("////// Camada Infraestrutura");
        stringBuilderConfiguration.AppendLine("////// Persistence > Configuration");
        stringBuilderConfiguration.AppendLine();
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine("using Core.Entities;");
        stringBuilderConfiguration.AppendLine("using Microsoft.EntityFrameworkCore;");
        stringBuilderConfiguration.AppendLine("using Microsoft.EntityFrameworkCore.Metadata;");
        stringBuilderConfiguration.AppendLine("using Microsoft.EntityFrameworkCore.Metadata.Builders;");
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine("namespace Infrastructure.Persistence.Configuration;");
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine($"public class Configuration{convertClassForSingle} : IEntityTypeConfiguration<{convertClassForSingle}>");
        stringBuilderConfiguration.AppendLine("{");
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine($"public void Configure(EntityTypeBuilder<{convertClassForSingle}> builder)");
        stringBuilderConfiguration.AppendLine("{");

        stringBuilderConfiguration.AppendLine($"builder.ToTable(\"{getEntities.Name}\");");
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine("builder.HasKey(x => x.Id)");
        stringBuilderConfiguration.AppendLine($"    .HasName(\"PK_{getEntities.Name.ToUpper()}\");");
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine(" builder.Property(c => c.Id)");
        stringBuilderConfiguration.AppendLine($"    .HasColumnName(\"{convertClassForSingle}Id\")");
        stringBuilderConfiguration.AppendLine($"    .HasColumnOrder(1)");
        stringBuilderConfiguration.AppendLine($"    .ValueGeneratedOnAdd();");
        stringBuilderConfiguration.AppendLine();

        foreach (var property in getEntities.PropertyRel)
        {
            typeCustom = mappingOfTypes.TryGetValue(property.Type, out var mappedType)
                         ? mappedType + (property.Type.Equals("string", StringComparison.OrdinalIgnoreCase) && property.QuantityCaracter > 0 ? $"({property.QuantityCaracter})" : "")
                         : "VARCHAR(MAX)";

            stringBuilderConfiguration.AppendLine($"builder.Property(c => c.{property.Name})");
            stringBuilderConfiguration.AppendLine($"    .HasColumnName(\"{property.Name}\")");
            stringBuilderConfiguration.AppendLine($"    .HasColumnOrder({counter})");
            stringBuilderConfiguration.AppendLine($"    .IsRequired({(property.IsRequired ? true : false)})");
            stringBuilderConfiguration.AppendLine($"    .HasColumnType({typeCustom}");
            stringBuilderConfiguration.AppendLine();

            counter++;
        }

        stringBuilderConfiguration.AppendLine("builder.Property(x => x.CreatedAt)");
        stringBuilderConfiguration.AppendLine($"    .HasColumnOrder({counter})");
        stringBuilderConfiguration.AppendLine($"    .IsRequired(true)");
        stringBuilderConfiguration.AppendLine($"    .HasColumnType(\"DATETIME2\")");
        stringBuilderConfiguration.AppendLine($"    .HasDefaultValueSql(\"GETDATE()\")");
        stringBuilderConfiguration.AppendLine($"    .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);");
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine("builder.Property(x => x.AlteredAt)");
        stringBuilderConfiguration.AppendLine($"    .HasColumnOrder({counter + 1})");
        stringBuilderConfiguration.AppendLine($"    .IsRequired(true)");
        stringBuilderConfiguration.AppendLine($"    .HasColumnType(\"DATETIME2\")");
        stringBuilderConfiguration.AppendLine($"    .HasDefaultValueSql(\"GETDATE()\")");
        stringBuilderConfiguration.AppendLine($"    .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Save);");
        stringBuilderConfiguration.AppendLine();

        stringBuilderConfiguration.AppendLine();
        stringBuilderConfiguration.AppendLine(" builder.HasIndex(c => c.Id)");
        stringBuilderConfiguration.AppendLine($"     .HasDatabaseName(\"IX_{convertClassForSingle.ToUpper()}_ID\")");
        stringBuilderConfiguration.AppendLine($"     .IsUnique();");
        stringBuilderConfiguration.AppendLine("}");

        return stringBuilderConfiguration.ToString();
    }

    private static string GenerateInfrastructureRepository(Entity getEntities, string convertClassForSingle)
    {
        var stringBuildeDbContextAndDI = new StringBuilder();


        stringBuildeDbContextAndDI.AppendLine("////// Camada Infraestrutura");
        stringBuildeDbContextAndDI.AppendLine("////// Persistence > Repository ");
        stringBuildeDbContextAndDI.AppendLine("using Core.Entities;");
        stringBuildeDbContextAndDI.AppendLine("using Core.Repositories;");
        stringBuildeDbContextAndDI.AppendLine();

        stringBuildeDbContextAndDI.AppendLine("namespace Infrastructure.Persistence.Repositories;");
        stringBuildeDbContextAndDI.AppendLine();

        stringBuildeDbContextAndDI.AppendLine($"public class {convertClassForSingle}Repository : BaseRepository<{convertClassForSingle}>, I{convertClassForSingle}Repository");
        stringBuildeDbContextAndDI.AppendLine("{");
        stringBuildeDbContextAndDI.AppendLine();

        stringBuildeDbContextAndDI.AppendLine($" public {convertClassForSingle}Repository(DbContextProject context) : base(context) {{}}");
        stringBuildeDbContextAndDI.AppendLine("}");



        return stringBuildeDbContextAndDI.ToString();
    }

    private static string GenerateInfrastructureDbContextAndDI(Entity getEntities, string convertClassForSingle)
    {
        var stringBuildeDbContextAndDI = new StringBuilder();


        stringBuildeDbContextAndDI.AppendLine("////// Camada Infraestrutura");
        stringBuildeDbContextAndDI.AppendLine("////// Persistence > DbContextProject.cs ");
        stringBuildeDbContextAndDI.AppendLine($"public DbSet<{convertClassForSingle}> {getEntities.Name} => Set<{convertClassForSingle}>();");
        stringBuildeDbContextAndDI.AppendLine();

        stringBuildeDbContextAndDI.AppendLine("////// DependencyInjectionInfrastructure.cs : Dentro do método AddRepositories()");
        stringBuildeDbContextAndDI.AppendLine($"services.AddScoped<I{convertClassForSingle}Repository, {convertClassForSingle}Repository>();");
        stringBuildeDbContextAndDI.AppendLine();

        return stringBuildeDbContextAndDI.ToString();
    }

    private static string GenerateApplicationCommandCreate(Entity getEntities, string convertClassForSingle)
    {
        var stringBuildeCommand = new StringBuilder();


        stringBuildeCommand.AppendLine("////// Camda de application > Command Creteate");
        stringBuildeCommand.AppendLine("////// Command > Create Classe da request");
        stringBuildeCommand.AppendLine("using MediatR;");
        stringBuildeCommand.AppendLine("namespace Application.Commands.Entity.Create;");
        stringBuildeCommand.AppendLine();
        stringBuildeCommand.AppendLine($"public class Create{convertClassForSingle}CommandRequest : IRequest<bool>");
        stringBuildeCommand.AppendLine("{");

        foreach (var property in getEntities.PropertyRel)
        {
            stringBuildeCommand.AppendLine($" public {property.Type} {property.Name} {{get; set; }} = null!;");
        }

        stringBuildeCommand.AppendLine("}");

        stringBuildeCommand.AppendLine("////// Command > Create Classe da Handler");
        stringBuildeCommand.AppendLine("using Application.Notification;");
        stringBuildeCommand.AppendLine("using AutoMapper;");
        stringBuildeCommand.AppendLine("using Core.Repositories;");
        stringBuildeCommand.AppendLine("using MediatR;");
        stringBuildeCommand.AppendLine("using Microsoft.Extensions.Logging;");
        stringBuildeCommand.AppendLine();
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine("namespace Application.Commands.Entity.Create;");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine($"public class Create{convertClassForSingle}CommandHandler(INotificationError notificationError, I{convertClassForSingle}Repository repository, ILogger<Create{convertClassForSingle}CommandHandler> logger, IMapper iMapper) : BaseCQRS(notificationError, iMapper), IRequestHandler<Create{convertClassForSingle}CommandRequest, bool>");
        stringBuildeCommand.AppendLine("{");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine($" public async Task<bool> Handle(Create{convertClassForSingle}CommandRequest request, CancellationToken cancellationToken)");
        stringBuildeCommand.AppendLine("{");
        stringBuildeCommand.AppendLine("{");
        stringBuildeCommand.AppendLine("const bool transactionStared = true;");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine("try");
        stringBuildeCommand.AppendLine("{");
        stringBuildeCommand.AppendLine(" await repository.StartTransactionAsync();");
        stringBuildeCommand.AppendLine($" repository.Add(await SimpleMapping<Core.Entities.{convertClassForSingle}>(request));");
        stringBuildeCommand.AppendLine($" var result = await repository.SaveChangesAsync();");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine(" if(!result)");
        stringBuildeCommand.AppendLine(" {");
        stringBuildeCommand.AppendLine("  Notify(message: \"Oops! We couldn't save your record. Please try again.\");");
        stringBuildeCommand.AppendLine("  await repository.RollbackTransactionAsync();");
        stringBuildeCommand.AppendLine("  return false;");
        stringBuildeCommand.AppendLine("  }");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine("await repository.CommitTransactionAsync();");
        stringBuildeCommand.AppendLine("}");
        stringBuildeCommand.AppendLine(" catch (Exception ex)");
        stringBuildeCommand.AppendLine(" {");
        stringBuildeCommand.AppendLine("  if (transactionStared) await repository.RollbackTransactionAsync();");
        stringBuildeCommand.AppendLine();

        stringBuildeCommand.AppendLine(" logger.LogCritical( \"Ops! We were unable to process your request. Details error: {ErrorMessage}\",ex.Message);");
        stringBuildeCommand.AppendLine(" Notify(message: \"Oops! We were unable to process your request.\");");
        stringBuildeCommand.AppendLine(" }");
        stringBuildeCommand.AppendLine();
        stringBuildeCommand.AppendLine("return true;");
        stringBuildeCommand.AppendLine("}");
        stringBuildeCommand.AppendLine("}");


        return stringBuildeCommand.ToString();
    }

    private static string GenerateApplicationCommandUpdate(Entity getEntities, string convertClassForSingle)
    {
        var stringBuildeCommandUpdate = new StringBuilder();
        string isRequired = string.Empty;


        stringBuildeCommandUpdate.AppendLine("////// Camda de application > Command > Update");
        stringBuildeCommandUpdate.AppendLine("////// Classe de request");
        stringBuildeCommandUpdate.AppendLine("using MediatR;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("namespace Application.Commands.Entity.Update;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($" public class Update{convertClassForSingle}CommandRequest : IRequest<bool>");
        stringBuildeCommandUpdate.AppendLine(" {");
        stringBuildeCommandUpdate.AppendLine(" public int Id {get; set;}");

        foreach (var property in getEntities.PropertyRel)
        {
            if (!property.IsRequired)
                isRequired = "?";
            else
                isRequired = "";

            stringBuildeCommandUpdate.AppendLine($" public {property.Type}{isRequired} {property.Name} {{get; set;}}");
        }

        stringBuildeCommandUpdate.AppendLine(" }");
        stringBuildeCommandUpdate.AppendLine();
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("////// Classe de Handler");
        stringBuildeCommandUpdate.AppendLine("using Application.Notification;");
        stringBuildeCommandUpdate.AppendLine("using Application.AutoMapper;");
        stringBuildeCommandUpdate.AppendLine("using Core.Repositories;");
        stringBuildeCommandUpdate.AppendLine("using MediatR;");
        stringBuildeCommandUpdate.AppendLine("using Microsoft.Extensions.Logging;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("namespace Application.Commands.Entity.Update;");
        stringBuildeCommandUpdate.AppendLine();
        stringBuildeCommandUpdate.AppendLine($" public class Update{convertClassForSingle}CommandHandler : BaseCQRS, IRequestHandler<Update{convertClassForSingle}CommandRequest, bool>");
        stringBuildeCommandUpdate.AppendLine(" {");
        stringBuildeCommandUpdate.AppendLine($" private readonly I{convertClassForSingle}Repository _repository;");
        stringBuildeCommandUpdate.AppendLine(" private readonly ILogger<UpdateEntityCommandHandler> _logger;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($"public Update{convertClassForSingle}CommandHandler(INotificationError notificationError, I{convertClassForSingle}Repository repository, ILogger<Update{convertClassForSingle}CommandHandler> looger, IMapper mapper) : base(notificationError, mapper)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine("_repository = repository;");
        stringBuildeCommandUpdate.AppendLine("_logger = looger;");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($"public async Task<bool> Handle(Update{convertClassForSingle}CommandRequest request, CancellationToken cancellationToken)");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine("var transactionStared = true;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("try");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine($"var getById{convertClassForSingle} = await _repository.GetByIdAsync(request.Id);");
        stringBuildeCommandUpdate.AppendLine();
        stringBuildeCommandUpdate.AppendLine("if (entitie is null)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine("Notify(message: \"Unable to locate the record.\");");
        stringBuildeCommandUpdate.AppendLine("return false;");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("await _repository.StartTransactionAsync();");
        stringBuildeCommandUpdate.AppendLine($" _repository.Update(await SimpleMapping<Core.Entities.{convertClassForSingle}>(request));");
        stringBuildeCommandUpdate.AppendLine(" var result = await _repository.SaveChangesAsync();");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine(" if(!result)");
        stringBuildeCommandUpdate.AppendLine(" {");
        stringBuildeCommandUpdate.AppendLine("  Notify(\"Oops! We couldn't save your record. Please try again.\");");
        stringBuildeCommandUpdate.AppendLine("  await _repository.RollbackTransactionAsync();");
        stringBuildeCommandUpdate.AppendLine("  return false;");
        stringBuildeCommandUpdate.AppendLine(" }");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("await _repository.CommitTransactionAsync();");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("catch (Exception ex)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine("if (transactionStared) await _repository.RollbackTransactionAsync();");
        stringBuildeCommandUpdate.AppendLine("_logger.LogCritical($\"ops! We were unable to process your request. Details error: {ex.Message}\");");
        stringBuildeCommandUpdate.AppendLine("Notify(message: \"Oops! We were unable to process your request.\");");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("return true;");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine("}");




        return stringBuildeCommandUpdate.ToString();
    }

    private static string GenerateApplicationCommandDelete(Entity getEntities, string convertClassForSingle)
    {
        var stringBuildeCommandUpdate = new StringBuilder();


        stringBuildeCommandUpdate.AppendLine("////// Camda de application > Command > Delete Request");
        stringBuildeCommandUpdate.AppendLine("using MediatR;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("namespace Application.Commands.Entitie.Delete;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($"public class Delete{convertClassForSingle}CommandRequest : IRequest<bool>");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine($"public Delete{convertClassForSingle}CommandRequest(int id)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine(" Id = id;");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("public int Id { get; set; }");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("////// Camda de application > Command > Delete Handler");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("using Application.Commands.Entitie.Delete;");
        stringBuildeCommandUpdate.AppendLine("using Application.Notification;");
        stringBuildeCommandUpdate.AppendLine("using AutoMapper;");
        stringBuildeCommandUpdate.AppendLine("using Core.Repositories;");
        stringBuildeCommandUpdate.AppendLine("using MediatR;");
        stringBuildeCommandUpdate.AppendLine("using Microsoft.Extensions.Logging;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("namespace Application.Commands.Entity.Delete;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($" public class Delete{convertClassForSingle}CommandHandler : BaseCQRS, IRequestHandler<Delete{convertClassForSingle}CommandRequest, bool>");
        stringBuildeCommandUpdate.AppendLine(" {");
        stringBuildeCommandUpdate.AppendLine($"  private readonly I{convertClassForSingle}Repository _repository;");
        stringBuildeCommandUpdate.AppendLine($"  private readonly ILogger<Delete{convertClassForSingle}CommandHandler> _logger;");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($"public Delete{convertClassForSingle}CommandHandler(INotificationError notificationError, IMapper iMapper, I{convertClassForSingle}Repository repository, ILogger<Update{convertClassForSingle}CommandHandler> logger) : base(notificationError, iMapper)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine(" _repository = repository;");
        stringBuildeCommandUpdate.AppendLine(" _logger = logger;");
        stringBuildeCommandUpdate.AppendLine(" }");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine($"public async Task<bool> Handle(Delete{convertClassForSingle}CommandRequest request, CancellationToken cancellationToken)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine("bool transactionStared = true;");
        stringBuildeCommandUpdate.AppendLine($"var get{convertClassForSingle}ById = await _repository.GetByIdAsync(request.Id);");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("if(entitie is null)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine("Notify(\"Unable to locate the record.\");");
        stringBuildeCommandUpdate.AppendLine("return false;");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("try");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine("await _repository.StartTransactionAsync()");
        stringBuildeCommandUpdate.AppendLine("await _repository.RollbackTransactionAsync();");
        stringBuildeCommandUpdate.AppendLine("return false;");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("await _repository.CommitTransactionAsync();");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine("catch (Exception ex)");
        stringBuildeCommandUpdate.AppendLine("{");
        stringBuildeCommandUpdate.AppendLine("if (transactionStared) await _repository.RollbackTransactionAsync();");
        stringBuildeCommandUpdate.AppendLine(" _logger.LogCritical($\"ops! We were unable to process your request. Details error: {ex.Message}\");");
        stringBuildeCommandUpdate.AppendLine("Notify(message: \"Oops! We were unable to process your request.\");");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine();

        stringBuildeCommandUpdate.AppendLine("return true;");
        stringBuildeCommandUpdate.AppendLine("}");
        stringBuildeCommandUpdate.AppendLine("}");

        return stringBuildeCommandUpdate.ToString();

    }

    private static string GenerateApplicationMapping(Entity getEntities, string convertClassForSingle)
    {
        var stringBuildeMapping = new StringBuilder();


        stringBuildeMapping.AppendLine("////// Camda de application > dentro da classe AutoMapperConfiguration");
        stringBuildeMapping.AppendLine();

        stringBuildeMapping.AppendLine($"using Application.Queries.{convertClassForSingle}.GetAll;");
        stringBuildeMapping.AppendLine($"using Application.Queries.{convertClassForSingle}.GetById;");
        stringBuildeMapping.AppendLine();

        stringBuildeMapping.AppendLine($" #region {convertClassForSingle}");
        stringBuildeMapping.AppendLine($"  CreateMap<{convertClassForSingle}, Create{convertClassForSingle}CommandRequest>().ReverseMap();");
        stringBuildeMapping.AppendLine($"  UpdateMap<{convertClassForSingle}, Update{convertClassForSingle}CommandRequest>().ReverseMap();");
        stringBuildeMapping.AppendLine();

        stringBuildeMapping.AppendLine($"CreateMap<{convertClassForSingle}, Query{convertClassForSingle}GetAllResponse>().ReverseMap();");
        stringBuildeMapping.AppendLine($"CreateMap<{convertClassForSingle}, Query{convertClassForSingle}GetByIdResponse>().ReverseMap();");
        stringBuildeMapping.AppendLine($"#endregion");

        return stringBuildeMapping.ToString();
    }

    private static string GenerateApplicationQueryGetAllResponse(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderQueryGetAll = new StringBuilder();
        stringBuilderQueryGetAll.AppendLine("////// Camda de application > Queries > GetAll > Response");
        stringBuilderQueryGetAll.AppendLine("////// Criação da classe Response");
        stringBuilderQueryGetAll.AppendLine($"namespace Application.Queries.{convertClassForSingle}.GetAll;");
        stringBuilderQueryGetAll.AppendLine();

        stringBuilderQueryGetAll.AppendLine($"public class Query{convertClassForSingle}GetAllResponse");
        stringBuilderQueryGetAll.AppendLine("{");
        stringBuilderQueryGetAll.AppendLine(" public int Id { get; set; }");

        foreach (var property in getEntities.PropertyRel)
        {
            stringBuilderQueryGetAll.AppendLine($" public {property.Type} {property.Name} {{ get; set; }}");
        }

        stringBuilderQueryGetAll.AppendLine("}");

        return stringBuilderQueryGetAll.ToString();
    }

    private static string GenerateApplicationQueryGetAllRequest(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderQueryGetAllRequest = new StringBuilder();

        stringBuilderQueryGetAllRequest.AppendLine("////// Camda de application > Queries > GetAll > Request");
        stringBuilderQueryGetAllRequest.AppendLine("////// Criação da classe de request");
        stringBuilderQueryGetAllRequest.AppendLine("using MediatR;");
        stringBuilderQueryGetAllRequest.AppendLine();

        stringBuilderQueryGetAllRequest.AppendLine($"namespace Application.Queries.{convertClassForSingle}.GetAll;");
        stringBuilderQueryGetAllRequest.AppendLine();

        stringBuilderQueryGetAllRequest.AppendLine($"public class Query{convertClassForSingle}GetAllRequest : IRequest<IEnumerable<Query{convertClassForSingle}GetAllResponse>> {{}}");

        return stringBuilderQueryGetAllRequest.ToString();

    }

    private static string GenerateApplicationQueryGetAllHandler(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderQueryGetAllHandler = new StringBuilder();

        stringBuilderQueryGetAllHandler.AppendLine("////// Camda de application > Queries > GetAll > Handler");
        stringBuilderQueryGetAllHandler.AppendLine("////// Criação da classe do handler");
        stringBuilderQueryGetAllHandler.AppendLine("using Application.Notification;");
        stringBuilderQueryGetAllHandler.AppendLine("using AutoMapper;");
        stringBuilderQueryGetAllHandler.AppendLine("using Core.Repositories;");
        stringBuilderQueryGetAllHandler.AppendLine("using MediatR;");
        stringBuilderQueryGetAllHandler.AppendLine();

        stringBuilderQueryGetAllHandler.AppendLine($"namespace Application.Queries.{convertClassForSingle}.GetAll;");
        stringBuilderQueryGetAllHandler.AppendLine();

        stringBuilderQueryGetAllHandler.AppendLine($"public class Query{convertClassForSingle}GetAllHandler : BaseCQRS, IRequestHandler<Query{convertClassForSingle}GetAllRequest, IEnumerable<Query{convertClassForSingle}GetAllResponse>>");
        stringBuilderQueryGetAllHandler.AppendLine("{");
        stringBuilderQueryGetAllHandler.AppendLine($"private readonly I{convertClassForSingle}Repository _repository;");
        stringBuilderQueryGetAllHandler.AppendLine();

        stringBuilderQueryGetAllHandler.AppendLine($"public Query{convertClassForSingle}GetAllHandler(INotificationError notificationError, IMapper iMapper, I{convertClassForSingle}Repository repository) : base(notificationError, iMapper)");
        stringBuilderQueryGetAllHandler.AppendLine("{");
        stringBuilderQueryGetAllHandler.AppendLine(" _repository = repository;");
        stringBuilderQueryGetAllHandler.AppendLine("}");
        stringBuilderQueryGetAllHandler.AppendLine();

        stringBuilderQueryGetAllHandler.AppendLine($"public async Task<IEnumerable<Query{convertClassForSingle}GetAllResponse>> Handle(Query{convertClassForSingle}GetAllRequest request, CancellationToken cancellationToken)");
        stringBuilderQueryGetAllHandler.AppendLine("{");
        stringBuilderQueryGetAllHandler.AppendLine($"var get{getEntities.Name} = await _repository.GetAllAsync();");
        stringBuilderQueryGetAllHandler.AppendLine($"return await MappingList<Query{convertClassForSingle}GetAllResponse>(get{getEntities.Name});");
        stringBuilderQueryGetAllHandler.AppendLine("}");
        stringBuilderQueryGetAllHandler.AppendLine("}");

        return stringBuilderQueryGetAllHandler.ToString();
    }

    private static string GenerateApplicationQueryGetByIdResponse(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderQueryGetByIdResponse = new StringBuilder();

        stringBuilderQueryGetByIdResponse.AppendLine("////// Camda de application > Queries > GetById > Request");
        stringBuilderQueryGetByIdResponse.AppendLine();

        stringBuilderQueryGetByIdResponse.AppendLine($"namespace Application.Queries.{convertClassForSingle}.GetById;");
        stringBuilderQueryGetByIdResponse.AppendLine();

        stringBuilderQueryGetByIdResponse.AppendLine($"public class Query{convertClassForSingle}GetByIdRequest : IRequest<Query{convertClassForSingle}GetByIdResponse>");
        stringBuilderQueryGetByIdResponse.AppendLine("{");
        stringBuilderQueryGetByIdResponse.AppendLine($"public Query{convertClassForSingle}GetByIdRequest(int id)");
        stringBuilderQueryGetByIdResponse.AppendLine("{");
        stringBuilderQueryGetByIdResponse.AppendLine("Id = id");
        stringBuilderQueryGetByIdResponse.AppendLine("}");
        stringBuilderQueryGetByIdResponse.AppendLine();

        stringBuilderQueryGetByIdResponse.AppendLine(" public int Id { get; set; }");
        stringBuilderQueryGetByIdResponse.AppendLine(" }");

        return stringBuilderQueryGetByIdResponse.ToString();
    }

    private static string GenerateApplicationValidatorCreate(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderCreateValidator = new StringBuilder();
        stringBuilderCreateValidator.AppendLine("////// Camada de application > Validators");
        stringBuilderCreateValidator.AppendLine();

        stringBuilderCreateValidator.AppendLine($"using Application.Commands.{convertClassForSingle}.Create;");
        stringBuilderCreateValidator.AppendLine("using FluentValidation;");
        stringBuilderCreateValidator.AppendLine();

        stringBuilderCreateValidator.AppendLine($"namespace Application.Validators.{convertClassForSingle};");
        stringBuilderCreateValidator.AppendLine();

        stringBuilderCreateValidator.AppendLine($"public class Create{convertClassForSingle}CommandItemValidator : AbstractValidator<Create{convertClassForSingle}Request>");
        stringBuilderCreateValidator.AppendLine("{");
        stringBuilderCreateValidator.AppendLine();

        stringBuilderCreateValidator.AppendLine($" public Create{convertClassForSingle}CommandItemValidator()");
        stringBuilderCreateValidator.AppendLine("{");
        stringBuilderCreateValidator.AppendLine();

        foreach (var property in getEntities.PropertyRel.Where(x => x.IsRequired == true && x.Type.Equals("string")))
        {
            stringBuilderCreateValidator.AppendLine($"RuleFor(x => x.{property.Name})");
            stringBuilderCreateValidator.AppendLine(".NotEmpty().WithMessage(\"Field is empty, the email is required.\")");
            stringBuilderCreateValidator.AppendLine(".NotNull().WithMessage(\"The email is null, please enter a valid email.\")");
            stringBuilderCreateValidator.AppendLine($".MaximumLength(maximumLength: {property.QuantityCaracter}).WithMessage(errorMessage: \"The field accepts a maximum of {property.QuantityCaracter} characters.\");");
            stringBuilderCreateValidator.AppendLine();
        }

        stringBuilderCreateValidator.AppendLine("}");
        stringBuilderCreateValidator.AppendLine("}");

        return stringBuilderCreateValidator.ToString();
    }

    private static string GenerateApplicationValidatorUpdate(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderUpdateValidator = new StringBuilder();

        stringBuilderUpdateValidator.AppendLine("////// Camada de application > Validators");
        stringBuilderUpdateValidator.AppendLine();

        stringBuilderUpdateValidator.AppendLine($"using Application.Commands.{convertClassForSingle}.Create;");
        stringBuilderUpdateValidator.AppendLine("using FluentValidation;");
        stringBuilderUpdateValidator.AppendLine();

        stringBuilderUpdateValidator.AppendLine($"namespace Application.Validators.{convertClassForSingle};");
        stringBuilderUpdateValidator.AppendLine();

        stringBuilderUpdateValidator.AppendLine($"public class Create{convertClassForSingle}CommandItemValidator : AbstractValidator<Create{convertClassForSingle}Request>");
        stringBuilderUpdateValidator.AppendLine("{");
        stringBuilderUpdateValidator.AppendLine();

        stringBuilderUpdateValidator.AppendLine($" public Create{convertClassForSingle}CommandItemValidator()");
        stringBuilderUpdateValidator.AppendLine("{");
        stringBuilderUpdateValidator.AppendLine();

        foreach (var property in getEntities.PropertyRel.Where(x => x.IsRequired == true && x.Type.Equals("string")))
        {
            stringBuilderUpdateValidator.AppendLine($"RuleFor(x => x.{property.Name})");
            stringBuilderUpdateValidator.AppendLine(".NotEmpty().WithMessage(\"Field is empty, the email is required.\")");
            stringBuilderUpdateValidator.AppendLine(".NotNull().WithMessage(\"The email is null, please enter a valid email.\")");
            stringBuilderUpdateValidator.AppendLine($".MaximumLength(maximumLength: {property.QuantityCaracter}).WithMessage(errorMessage: \"The field accepts a maximum of {property.QuantityCaracter} characters.\");");
            stringBuilderUpdateValidator.AppendLine();
        }

        stringBuilderUpdateValidator.AppendLine("}");
        stringBuilderUpdateValidator.AppendLine("}");

        return stringBuilderUpdateValidator.ToString();
    }

    private static string GenerateAPIEndpoint(Entity getEntities, string convertClassForSingle)
    {
        var stringBuilderApiEndpoint = new StringBuilder();
        stringBuilderApiEndpoint.AppendLine("////// Camada da API > Controllers");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine($"using Application.Commands.{convertClassForSingle}.Delete;");
        stringBuilderApiEndpoint.AppendLine($"using Application.Commands.{convertClassForSingle}.Create;");
        stringBuilderApiEndpoint.AppendLine($"using Application.Commands.{convertClassForSingle}.Update;");
        stringBuilderApiEndpoint.AppendLine("using Application.Notification;");
        stringBuilderApiEndpoint.AppendLine("using Application.Queries;");
        stringBuilderApiEndpoint.AppendLine($"using Application.Queries.{convertClassForSingle}.GetById;");
        stringBuilderApiEndpoint.AppendLine($"using Application.Queries.{convertClassForSingle}.GetAll;");
        stringBuilderApiEndpoint.AppendLine("using MediatR;");
        stringBuilderApiEndpoint.AppendLine("using Microsoft.AspNetCore.Mvc;");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("namespace API.Controllers;");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine($"[Route(\"api/{ConvertToKebabCase(convertClassForSingle)}\")]");
        stringBuilderApiEndpoint.AppendLine($"public class {getEntities.Name}Controller : MainController");
        stringBuilderApiEndpoint.AppendLine("{");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("private readonly IMediator _mediator;");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine($"public {getEntities.Name}Controller(INotificationError notificationError, IMediator mediator) : base(notificationError)");
        stringBuilderApiEndpoint.AppendLine("{");
        stringBuilderApiEndpoint.AppendLine("_mediator = mediator;");
        stringBuilderApiEndpoint.AppendLine("}");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("[HttpGet(\"all\")]");
        stringBuilderApiEndpoint.AppendLine($"public async Task<ActionResult<IEnumerable<Query{convertClassForSingle}GetAllResponse>>> GetAll()");
        stringBuilderApiEndpoint.AppendLine("{");
        stringBuilderApiEndpoint.AppendLine($"return Ok(await _mediator.Send(new Query{convertClassForSingle}GetAllRequest()));");
        stringBuilderApiEndpoint.AppendLine("}");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("[HttpGet(\"{id:int}\")]");
        stringBuilderApiEndpoint.AppendLine($"public async Task<ActionResult<Query{convertClassForSingle}GetByIdResponse>> GetById(int id)");
        stringBuilderApiEndpoint.AppendLine("{");
        stringBuilderApiEndpoint.AppendLine($"var getById{convertClassForSingle} = await _mediator.Send(new Query{convertClassForSingle}GetByIdRequest(id));");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine($"if(getById{convertClassForSingle} is null)");
        stringBuilderApiEndpoint.AppendLine($" return NotFound();");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine($" return Ok(getById{convertClassForSingle});");
        stringBuilderApiEndpoint.AppendLine(" }");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("[HttpPost]");
        stringBuilderApiEndpoint.AppendLine($" public async Task<ActionResult> Post([FromBody] Create{convertClassForSingle}Request request)");
        stringBuilderApiEndpoint.AppendLine(" {");
        stringBuilderApiEndpoint.AppendLine(" if (!ModelState.IsValid) return CustomResponse(ModelState);");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("await _mediator.Send(request);");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("if (!ValidOperation()) return CustomResponse();");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("return Created();");
        stringBuilderApiEndpoint.AppendLine("}");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("[HttpPut(\"{id:int}\")]");
        stringBuilderApiEndpoint.AppendLine($"public async Task<ActionResult> Put(int id, Update{convertClassForSingle}Request request)");
        stringBuilderApiEndpoint.AppendLine("{");
        stringBuilderApiEndpoint.AppendLine("if (id != request.Id)");
        stringBuilderApiEndpoint.AppendLine("{");
        stringBuilderApiEndpoint.AppendLine("NotifyError(\"Oops! We cannot process your request due to ID integrity errors.\");");
        stringBuilderApiEndpoint.AppendLine("return CustomResponse();");
        stringBuilderApiEndpoint.AppendLine("}");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("if (!ModelState.IsValid) return CustomResponse(ModelState);");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("return Ok();");
        stringBuilderApiEndpoint.AppendLine("}");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("[HttpDelete(\"{id:int}\")]");
        stringBuilderApiEndpoint.AppendLine("public async Task<ActionResult> Delete(int id)");
        stringBuilderApiEndpoint.AppendLine("{");
        stringBuilderApiEndpoint.AppendLine($"await _mediator.Send(new Delete{convertClassForSingle}Request(id));");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("if (!ValidOperation()) return CustomResponse();");
        stringBuilderApiEndpoint.AppendLine();

        stringBuilderApiEndpoint.AppendLine("return NoContent();");
        stringBuilderApiEndpoint.AppendLine("}");
        stringBuilderApiEndpoint.AppendLine("}");


        return stringBuilderApiEndpoint.ToString();

    }

    private static string ConvertToKebabCase(string input)
    {
        return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x : x.ToString())).ToLower();
    }
}
