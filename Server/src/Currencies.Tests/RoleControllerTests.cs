﻿using Currencies.Contracts.Helpers;
using Currencies.Contracts.Interfaces;
using Currencies.DataAccess.Services;
using Currencies.Models;
using Xunit;
using AutoMapper;
using Currencies.DataAccess.Mappings;
using Currencies.Contracts.Helpers.Forms;
using Currencies.Contracts.ModelDtos.Role;
using Currencies.Api.Functions.Role.Queries.GetAll;
using Currencies.Api.Functions.Role.Queries.GetSingle;
using Currencies.Api.Functions.Role.Commands.Create;
using Currencies.Api.Functions.Role.Commands.Update;
using Currencies.Api.Functions.Role.Commands.Delete;
using Currencies.Api.Functions.Role.Queries.GetEditForm;

namespace Currencies.Tests;

public class RoleControllerTests : IClassFixture<BaseTestFixture>
{
    private readonly TableContext _dbContext;
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public RoleControllerTests(BaseTestFixture fixture)
    {
        _dbContext = fixture._dbContext;
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AutoMapperProfile());
        });
        _mapper = mappingConfig.CreateMapper();
        _roleService = new RoleService(_dbContext, _mapper);
    }

    [Fact]
    public async Task GetAll_Roles_ReturnPageResult()
    {
        // arrange
        FilterRoleDto filter = new()
        {
            PageNumber = 1,
            PageSize = 10
        };

        GetRolesListQuery query = new(filter);
        GetRolesListQueryHandler handler = new(_roleService);

        // act
        var result = await handler.Handle(query, new CancellationToken());

        // assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<PageResult<RoleDto>>(result);
    }

    [Fact]
    public async Task GetById_Roles_ReturnRole()
    {
        // arrange
        var id = 1;

        GetSingleRoleQuery query = new(id);
        GetSinglRoleQueryHandler handler = new(_roleService, _mapper);

        // act
        var result = await handler.Handle(query, new CancellationToken());

        // assert
        Assert.NotNull(result);
        Assert.Equal(id, result!.Id);
    }

    [Fact]
    public async Task Create_Role_ReturnNewRole()
    {
        // arrange
        BaseRoleDto dto = new()
        {
            Name = "Name"
        };

        CreateRoleCommand command = new(dto);
        CreateRoleCommandHandler handler = new(_roleService);

        // act
        var result = await handler.Handle(command, new CancellationToken());

        // assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<RoleDto>(result);
    }

    [Fact]
    public async Task Update_Role_ReturnUpdatedRole()
    {
        // arrange
        var id = 1;
        BaseRoleDto dto = new()
        {
            Name = "Update name",
            IsActive = true
        };

        UpdateRoleCommand command = new(id, dto);
        UpdateRoleCommandHandler handler = new(_roleService);

        // act
        var result = await handler.Handle(command, new CancellationToken());

        // assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<RoleDto>(result);
    }

    [Fact]
    public async Task Delete_Role_ReturnTrue()
    {
        // arrange
        var id = 1;

        DeleteRoleCommand command = new(id);
        DeleteRoleCommandHandler handler = new(_roleService);

        // act
        var result = await handler.Handle(command, new CancellationToken());

        // assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetEditForm_Role_ReturnRoleEmptyEditForm()
    {
        // arrange
        var id = 0;

        GetRoleEditFormQuery query = new(id);
        GetRoleEditFormQueryHandler handler = new(_roleService, _mapper, _dbContext);

        // act
        var result = await handler.Handle(query, new CancellationToken());

        // assert
        Assert.NotNull(result);
        Assert.Equal(string.Empty, result.Name.Value);
        Assert.True(result.IsActive.Value);
        Assert.IsAssignableFrom<RoleEditForm>(result);
    }
}
