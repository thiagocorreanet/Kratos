﻿using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{

    public ProjectRepository(DbContextProject context) : base(context) { }
}