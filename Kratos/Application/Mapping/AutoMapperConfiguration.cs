using Application.Commands.Entity.Create;
using Application.Commands.Entity.Update;
using Application.Commands.EntityProperty.Create;
using Application.Commands.EntityProperty.Update;
using Application.Commands.Project.Create;
using Application.Commands.Project.Update;
using Application.Queries;
using Application.Queries.EntitiyModel.GetAll;
using Application.Queries.EntityProperty.GetAll;
using Application.Queries.EntityProperty.GetById;
using Application.Queries.Project.GetAll;
using Application.Queries.Project.GetById;
using AutoMapper;
using Core.Entities;

namespace Application.Mapping
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            #region Entity
            CreateMap<Entity, CreateEntityCommandRequest>().ReverseMap();
            CreateMap<Entity, UpdateEntityCommandRequest>().ReverseMap();
            
            CreateMap<Entity, QueryEntityGetAllResponse>().ReverseMap();
            CreateMap<Entity, QueryEntityGetByIdResponse>().ReverseMap();
            #endregion

            #region Entity Property
            CreateMap<EntityProperty, CreateEntityPropertyRequestItem>().ReverseMap();
            CreateMap<EntityProperty, UpdateEntityPropertyRequest>().ReverseMap();
            
            CreateMap<EntityProperty, QueryEntityPropertyGetAllResponse>().ReverseMap();
            CreateMap<EntityProperty, QueryEntityPropertyGetByIdResponse>().ReverseMap();
            #endregion

            #region Projec
            CreateMap<Project, CreateProjectCommandRequest>().ReverseMap();
            CreateMap<Project, UpdateProjectCommandRequest>().ReverseMap();

            CreateMap<Project, QueryProjectGetAllResponse>().ReverseMap();
            CreateMap<Project, QueryProjectGetByIdResponse>().ReverseMap();
            CreateMap<Project, QueryProjectGetByIdRequest>().ReverseMap();
            #endregion
        }
    }
}