using Application.Commands.Entity.Create;
using Application.Commands.Entity.Update;
using Application.Commands.EntityProperty.Create;
using Application.Commands.EntityProperty.Update;
using Application.Queries;
using Application.Queries.EntitiyModel.GetAll;
using Application.Queries.EntityProperty.GetAll;
using Application.Queries.EntityProperty.GetById;
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
        }
    }
}